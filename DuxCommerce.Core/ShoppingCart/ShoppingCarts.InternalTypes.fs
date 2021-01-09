namespace DuxCommerce.ShoppingCarts.InternalTypes

open DuxCommerce.Catalogue.InternalTypes
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.ShoppingCarts.Commands

type CartItem = {
    ProductId : ProductId
    ProductName: String255
    Price: SalePrice
    Quantity: ItemQuantity
    ItemTotal: ItemTotal
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

        cmds |> Seq.collect (updateQtyIf cartItem)
    
    let createItem cartId (product:Product) quantity :CartItem=
        {
            ProductId = product.ProductId
            ProductName = product.Name
            Price = product.Price
            Quantity = quantity
            ItemTotal = ItemTotal.calculate product.Price quantity     
        }

type ShoppingCart = {
    ShoppingCartId : ShoppingCartId
    ShopperId : ShopperId
    LineItems: CartItem seq
    CartTotal: CartTotal
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
         
    let addCartItem cart product (cmd:AddCartItemCommand) =
        let lineItems = 
            let check cartItem = cartItem.ProductId = cmd.ProductId
            let itemExists = cart.LineItems |> Seq.tryFind check
            match itemExists with
            | Some _ ->
                cart.LineItems |> Seq.map (CartItem.addQtyIf cmd)
            | None ->
                let newItem = CartItem.createItem cart.ShoppingCartId product cmd.Quantity
                Seq.append cart.LineItems [newItem]

        updateItems cart lineItems

    let updateCart cart (cmd:UpdateCartCommand) =
        let lineItems =
            cart.LineItems
            |> Seq.collect (CartItem.update cmd.UpdateItems)
    
        updateItems cart lineItems
            
    let deleteCartItem cart (cmd:DeleteCartItemCommand) =      
        let check cartItem = cartItem.ProductId <> cmd.ProductId               
        let remainingItems = Seq.filter check cart.LineItems 
        
        let updatedCart = updateItems cart remainingItems              
            
        updatedCart
        