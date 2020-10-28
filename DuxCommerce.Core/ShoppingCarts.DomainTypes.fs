namespace DuxCommerce.ShoppingCarts.DomainTypes

open DuxCommerce.Catalogue
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts

type AddCartItemCmd = {
    ProductId : ProductId
    Quantity: ItemQuantity
}

type UpdateCartItemCmd = {
    ProductId : ProductId
    Quantity: ItemQuantity
}

type UpdateCartCmd = {
    CartItemCmds: UpdateCartItemCmd seq
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
    LineItems: CartItem list
    CartTotal: CartTotal
}

module CartItem =
    let internal update cartItem quantity= 
        let newQuantity = ItemQuantity.add cartItem.Quantity quantity
        let newTotal = ItemTotal.calculate cartItem.Price newQuantity
        { cartItem with Quantity = newQuantity; ItemTotal = newTotal }
        
    let internal updateItem (cmd:AddCartItemCmd) cartItem =        
        if cartItem.ProductId = cmd.ProductId
        then update cartItem cmd.Quantity
        else cartItem            
        
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
    let internal calcTotal cart =
        cart.LineItems 
        |> List.sumBy (fun l -> ItemTotal.value l.ItemTotal)
        |> CartTotal.create
         
    let addCartItem cart product (cmd:AddCartItemCmd) =
        let f = fun item -> item.ProductId = cmd.ProductId
        let items = cart.LineItems |> List.filter f
        match items with
        | [] ->
            let newItem = CartItem.createItem cart.Id product cmd.Quantity
            let newTotal = CartTotal.addItemTotal cart.CartTotal newItem.ItemTotal
            let newItems = newItem :: cart.LineItems
            { cart with LineItems = newItems; CartTotal = newTotal }
        | _ ->            
            let newItems = cart.LineItems |> List.map (CartItem.updateItem cmd)
            let newCart = { cart with LineItems = newItems }
            let newTotal = calcTotal newCart
            { newCart with CartTotal = newTotal }

    let updateCart cart (cmd:UpdateCartCmd) =
        cart