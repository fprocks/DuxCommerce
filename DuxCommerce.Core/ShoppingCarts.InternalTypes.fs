namespace DuxCommerce.ShoppingCarts.InternalTypes

open DuxCommerce.Catalogue.InternalTypes
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
        
module internal CartItem =
                
    let addQtyIf (cmd:AddCartItemCommand) cartItem =        
        let addQty cartItem quantity = 
            let newQuantity = ItemQuantity.add cartItem.Quantity quantity
            let newTotal = ItemTotal.calculate cartItem.Price newQuantity
            {cartItem with Quantity = newQuantity; ItemTotal = newTotal}

        if cartItem.ProductId = cmd.ProductId
        then addQty cartItem cmd.Quantity
        else cartItem            
    
    let update (cmds:UpdateCartItemCommand seq) cartItem =
        let updateQty cartItem quantity = 
            let newTotal = ItemTotal.calculate cartItem.Price quantity
            {cartItem with Quantity = quantity; ItemTotal = newTotal}

        let updateQtyIf cartItem (itemCmd:UpdateCartItemCommand) =
            if itemCmd.ProductId = cartItem.ProductId
            then seq {updateQty cartItem itemCmd.Quantity}
            else Seq.empty

        cmds
        |> Seq.map (updateQtyIf cartItem)
        |> Seq.concat
    
    let createItem cartId (product:Product) quantity :CartItem=
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
        
    let internal cartTotal cart =
        cart.LineItems 
        |> Seq.sumBy (fun l -> ItemTotal.value l.ItemTotal)
        |> CartTotal.create 

    let internal updateItems (cart:ShoppingCart) lineItems = 
        let updatedCart = {cart with LineItems = lineItems}            
        let cartTotal = cartTotal updatedCart
        {updatedCart with CartTotal = cartTotal}

    let internal addItem cart product (cmd:AddCartItemCommand) =        
        let lineItems = 
            let check cartItem = cartItem.ProductId = cmd.ProductId

            let itemFound = cart.LineItems |> Seq.tryFind check
            match itemFound with
            | Some _ ->
                cart.LineItems |> Seq.map (CartItem.addQtyIf cmd)
            | None ->
                let newItem = CartItem.createItem cart.Id product cmd.Quantity
                Seq.append cart.LineItems [newItem]

        updateItems cart lineItems 

    let internal update cart (cmd:UpdateCartCommand) =
        let lineItems =
            cart.LineItems
            |> Seq.map (CartItem.update cmd.UpdateItems)
            |> Seq.concat
    
        updateItems cart lineItems   
         
    let addCartItem cartDto productDto (request:AddCartItemRequest) =
        result {
            let! cmd = AddCartItemCommand.fromRequest request
            let cart = ShoppingCartDto.toDomain cartDto
            let! product = ProductDto.toDomain productDto

            let updatedCart = addItem cart product cmd                                
            return ShoppingCartDto.fromDomain updatedCart
        }

    let updateCart cartDto (request:UpdateCartRequest) =
        result {
            let! cmd = UpdateCartCommand.fromRequest request
            let cart = ShoppingCartDto.toDomain cartDto
            
            let updatedCart = update cart cmd
            return ShoppingCartDto.fromDomain updatedCart
        }

    let deleteCartItem cartDto (request:DeleteCartItemRequest) =
        result {
            let! cmd = DeleteCartItemCommand.fromRequest request
            let cart = ShoppingCartDto.toDomain cartDto

            let check cartItem = cartItem.ProductId <> cmd.ProductId               
            let remainingItems = Seq.filter check cart.LineItems 
            
            let updatedCart = 
                updateItems cart remainingItems  
                |> ShoppingCartDto.fromDomain
            
            let deletedItems = 
                Seq.except remainingItems cart.LineItems                
                |> Seq.map CartItemDto.fromDomain
                
            return updatedCart , deletedItems
        }