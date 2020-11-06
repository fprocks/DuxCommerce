namespace DuxCommerce.ShoppingCarts.InternalTypes

open DuxCommerce.Catalogue.InternalTypes
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.Catalogue.SimpleTypes

type AddCartItemCommand = {
    ProductId : ProductId
    Quantity: ItemQuantity
}

type UpdateCartItemCommand = {
    ProductId : ProductId
    Quantity: ItemQuantity
}

type UpdateCartCommand = {
    UpdateItems: UpdateCartItemCommand seq
}

type DeleteCartItemCmd = {
    ProductId: ProductId
}

type CartItem = {
    Id: CartItemId
    CartId: CartId
    ProductId : ProductId
    ProductName: String255
    Price: SalePrice
    Quantity: ItemQuantity
    ItemTotal: ItemTotal
}

type Cart = {
    Id : CartId
    ShopperId : ShopperId
    LineItems: CartItem seq
    CartTotal: CartTotal
}

module CartItem =

    let internal addQty cartItem quantity = 
        let newQuantity = ItemQuantity.add cartItem.Quantity quantity
        let newTotal = ItemTotal.calculate cartItem.Price newQuantity
        { cartItem with Quantity = newQuantity; ItemTotal = newTotal }
        
    let internal addQtyIf (cmd:AddCartItemCommand) cartItem =        
        if cartItem.ProductId = cmd.ProductId
        then addQty cartItem cmd.Quantity
        else cartItem            
        
    let internal update cartItem quantity = 
        let newTotal = ItemTotal.calculate cartItem.Price quantity
        { cartItem with Quantity = quantity; ItemTotal = newTotal }
        
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
         
    let addCartItem cart product (cmd:AddCartItemCommand) =
        let checkId = fun item -> item.ProductId = cmd.ProductId
        let itemOption = cart.LineItems |> Seq.tryFind checkId
        let lineItems =
            match itemOption with
            | Some _ ->
                cart.LineItems |> Seq.map (CartItem.addQtyIf cmd)
            | None ->
                let newItem = CartItem.createItem cart.Id product cmd.Quantity
                Seq.append cart.LineItems [newItem]

        let updatedCart = { cart with LineItems = lineItems }
        let cartTotal = calculateTotal updatedCart
        { updatedCart with CartTotal = cartTotal }

    let updateCart cart (cmd:UpdateCartCommand) =

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
            
        let updatedCart = { cart with LineItems = lineItems }
        let newTotal = calculateTotal updatedCart
        { updatedCart with CartTotal = newTotal }

    let deleteCartItem cart (cmd:DeleteCartItemCmd) =

        let deleteIf (itemCmd:DeleteCartItemCmd) item =
            if itemCmd.ProductId = item.ProductId
            then Seq.empty
            else seq {item}

        let remainingItems = 
            cart.LineItems 
            |> Seq.map (deleteIf cmd)
            |> Seq.concat
            
        // Todo: how to avoid updating cart twice
        let updatedCart = { cart with LineItems = remainingItems }
        let cartTotal = calculateTotal updatedCart
        let deletedItems = Seq.except remainingItems cart.LineItems 
        { updatedCart with CartTotal = cartTotal }, deletedItems
