namespace DuxCommerce.ShoppingCarts.InternalTypes

open DuxCommerce.Catalogue.InternalTypes
open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.PublicTypes
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Catalogue.Dto
open DuxCommerce.ShoppingCarts.Commands

type CartItem = {
    Id: CartItemId
    CartId: CartId
    ProductId : ProductId
    ProductName: String255
    Price: SalePrice
    Quantity: ItemQuantity
    ItemTotal: ItemTotal
}

type ShoppingCart = {
    Id : CartId
    ShopperId : ShopperId
    LineItems: CartItem seq
    CartTotal: CartTotal
}

module CartItemInfo = 

    let fromDomain (cartItem:CartItem) :CartItemInfo =
        {
            Id = CartItemId.value cartItem.Id
            CartId = CartId.value cartItem.CartId
            ProductId = ProductId.value cartItem.ProductId
            ProductName = String255.value cartItem.ProductName
            Price = SalePrice.value cartItem.Price
            Quantity = ItemQuantity.value cartItem.Quantity
            ItemTotal = ItemTotal.value cartItem.ItemTotal
        }
            
    let toDomain (itemInfo:CartItemInfo) :CartItem =
        {
            Id = CartItemId.create itemInfo.Id
            CartId = CartId.create itemInfo.CartId
            ProductId = ProductId.create itemInfo.ProductId
            ProductName = String255 itemInfo.ProductName
            Price = SalePrice.create itemInfo.Price
            Quantity = ItemQuantity.create itemInfo.Quantity
            ItemTotal = ItemTotal.create itemInfo.ItemTotal
        }

module CartInfo =

    let fromDomain (cart:ShoppingCart) :CartInfo =
        {
            Id = CartId.value cart.Id
            ShopperId = ShopperId.value cart.ShopperId
            LineItems = cart.LineItems |> Seq.map CartItemInfo.fromDomain
            CartTotal = CartTotal.value cart.CartTotal
        }
    
    let toDomain (cartInfo:CartInfo) :ShoppingCart =
        {
            Id = CartId.create cartInfo.Id
            ShopperId = ShopperId.create cartInfo.ShopperId
            LineItems = cartInfo.LineItems |> Seq.map CartItemInfo.toDomain
            CartTotal = CartTotal.create cartInfo.CartTotal
        }
        
module CartItem =
        
    let internal addQty cartItem quantity = 
        let newQuantity = ItemQuantity.add cartItem.Quantity quantity
        let newTotal = ItemTotal.calculate cartItem.Price newQuantity
        {cartItem with Quantity = newQuantity; ItemTotal = newTotal}
        
    let internal addQtyIf (cmd:AddCartItemCommand) cartItem =        
        if cartItem.ProductId = cmd.ProductId
        then addQty cartItem cmd.Quantity
        else cartItem            
        
    let internal update cartItem quantity = 
        let newTotal = ItemTotal.calculate cartItem.Price quantity
        {cartItem with Quantity = quantity; ItemTotal = newTotal}
        
    let internal createItem cartId (product:Product) quantity :CartItem=
        {
            Id = CartItemId.create 0L
            CartId = cartId
            ProductId = product.Id
            ProductName = product.Name
            Price = product.Price
            Quantity = quantity
            ItemTotal = ItemTotal.calculate product.Price quantity     
        }

module ShoppingCart =  
        
    let internal calculateTotal cart =
        cart.LineItems 
        |> Seq.sumBy (fun l -> ItemTotal.value l.ItemTotal)
        |> CartTotal.create 

    let internal updateCartItems (cart:ShoppingCart) lineItems = 
        // Todo: how to avoid updating cart twice
        let updatedCart = {cart with LineItems = lineItems}            
        let cartTotal = calculateTotal updatedCart
        {updatedCart with CartTotal = cartTotal}

         
    let addCartItem (cartInfo: CartInfo) (productInfo:ProductInfo) (request:AddCartItemRequest) =
        result {
            let! cmd = AddCartItemCommand.fromRequest request
            let cart = CartInfo.toDomain cartInfo
            let! product = ProductInfo.toDomain productInfo

            let itemOption =
                cart.LineItems
                |> Seq.tryFind (fun item -> item.ProductId = cmd.ProductId)
                
            let lineItems =
                match itemOption with
                | Some _ ->
                    cart.LineItems |> Seq.map (CartItem.addQtyIf cmd)
                | None ->
                    let newItem = CartItem.createItem cart.Id product cmd.Quantity
                    Seq.append cart.LineItems [newItem]
                    
            let updatedCart = updateCartItems cart lineItems            
            return CartInfo.fromDomain updatedCart
        }

    let updateCart cartInfo (request:UpdateCartRequest) =
        result {
            let! cmd = UpdateCartCommand.fromRequest request
            let cart = CartInfo.toDomain cartInfo

            let updateQtyIf item (itemCmd:UpdateCartItemCommand) =
                if itemCmd.ProductId = item.ProductId
                then seq {CartItem.update item itemCmd.Quantity}
                else Seq.empty
                
            let update (itemCmds:UpdateCartItemCommand seq) cartItem =
                itemCmds
                |> Seq.map (updateQtyIf cartItem)
                |> Seq.concat
                
            let lineItems =
                cart.LineItems
                |> Seq.map (update cmd.UpdateItems)
                |> Seq.concat
                
            let updatedCart = updateCartItems cart lineItems            
            return CartInfo.fromDomain updatedCart
        }

    let deleteCartItem cartInfo (request:DeleteCartItemRequest) =
        result {
            let! cmd = DeleteCartItemCommand.fromRequest request
            let cart = CartInfo.toDomain cartInfo

            let deleteIf (itemCmd:DeleteCartItemCommand) item =
                if itemCmd.ProductId = item.ProductId
                then Seq.empty
                else seq {item}

            let remainingItems = 
                cart.LineItems 
                |> Seq.map (deleteIf cmd)
                |> Seq.concat
                
            let updatedCart = updateCartItems cart remainingItems                        
            let cartInfo = CartInfo.fromDomain updatedCart
            
            let deletedItems = Seq.except remainingItems cart.LineItems
            let deletedItems' =
                deletedItems
                |> Seq.map CartItemInfo.fromDomain
                
            return cartInfo, deletedItems'
        }
