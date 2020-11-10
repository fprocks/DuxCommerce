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

module CartItemDto = 

    let fromDomain (cartItem:CartItem) :CartItemDto =
        {
            Id = CartItemId.value cartItem.Id
            CartId = CartId.value cartItem.CartId
            ProductId = ProductId.value cartItem.ProductId
            ProductName = String255.value cartItem.ProductName
            Price = SalePrice.value cartItem.Price
            Quantity = ItemQuantity.value cartItem.Quantity
            ItemTotal = ItemTotal.value cartItem.ItemTotal
        }
            
    let toDomain (itemDto:CartItemDto) :CartItem =
        {
            Id = CartItemId.create itemDto.Id
            CartId = CartId.create itemDto.CartId
            ProductId = ProductId.create itemDto.ProductId
            ProductName = String255 itemDto.ProductName
            Price = SalePrice.create itemDto.Price
            Quantity = ItemQuantity.create itemDto.Quantity
            ItemTotal = ItemTotal.create itemDto.ItemTotal
        }

module ShoppingCartDto =

    let fromDomain (cart:ShoppingCart) :ShoppingCartDto =
        {
            Id = CartId.value cart.Id
            ShopperId = ShopperId.value cart.ShopperId
            LineItems = cart.LineItems |> Seq.map CartItemDto.fromDomain
            CartTotal = CartTotal.value cart.CartTotal
        }
    
    let toDomain (cartDto:ShoppingCartDto) :ShoppingCart =
        {
            Id = CartId.create cartDto.Id
            ShopperId = ShopperId.create cartDto.ShopperId
            LineItems = cartDto.LineItems |> Seq.map CartItemDto.toDomain
            CartTotal = CartTotal.create cartDto.CartTotal
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

         
    let addCartItem (cartDto: ShoppingCartDto) (productDto:ProductDto) (request:AddCartItemRequest) =
        result {
            let! cmd = AddCartItemCommand.fromRequest request
            let cart = ShoppingCartDto.toDomain cartDto
            let! product = ProductDto.toDomain productDto

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
            return ShoppingCartDto.fromDomain updatedCart
        } |> ConfigReader.retn

    let updateCart cartDto (request:UpdateCartRequest) =
        result {
            let! cmd = UpdateCartCommand.fromRequest request
            let cart = ShoppingCartDto.toDomain cartDto

            let updateQtyIf cartItem (itemCmd:UpdateCartItemCommand) =
                if itemCmd.ProductId = cartItem.ProductId
                then seq {CartItem.update cartItem itemCmd.Quantity}
                else Seq.empty
                
            let update (updateItemCommands:UpdateCartItemCommand seq) cartItem =
                updateItemCommands
                |> Seq.map (updateQtyIf cartItem)
                |> Seq.concat
                
            let lineItems =
                cart.LineItems
                |> Seq.map (update cmd.UpdateItems)
                |> Seq.concat
                
            let updatedCart = updateCartItems cart lineItems            
            return ShoppingCartDto.fromDomain updatedCart
        } |> ConfigReader.retn

    let deleteCartItem cartDto (request:DeleteCartItemRequest) =
        result {
            let! cmd = DeleteCartItemCommand.fromRequest request
            let cart = ShoppingCartDto.toDomain cartDto

            let deleteIf (deleteItemCommand:DeleteCartItemCommand) item =
                if deleteItemCommand.ProductId = item.ProductId
                then Seq.empty
                else seq {item}

            let remainingItems = 
                cart.LineItems 
                |> Seq.map (deleteIf cmd)
                |> Seq.concat
                
            let updatedCart = updateCartItems cart remainingItems                        
            let cartDto = ShoppingCartDto.fromDomain updatedCart
            
            let deletedItems = Seq.except remainingItems cart.LineItems
            let deletedItems' =
                deletedItems
                |> Seq.map CartItemDto.fromDomain
                
            return cartDto, deletedItems'
        } |> ConfigReader.retn
