namespace DuxCommerce.ShoppingCarts.DomainTypes

open DuxCommerce.Catalogue
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts

type AddItemCmd = {
    ProductId : ProductId
    Quantity: ItemQuantity
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

type AddCartItem = Cart -> Product -> AddItemCmd -> Cart

module ShoppingCart =
    let internal updateItem (addItemCmd:AddItemCmd) cartItem =
        let update item quantity :CartItem= 
            let newQuantity = ItemQuantity.add item.Quantity quantity
            let newTotal = ItemTotal.calculate item.Price newQuantity
            { cartItem with Quantity = newQuantity; ItemTotal = newTotal }
        
        if cartItem.ProductId = addItemCmd.ProductId
        then update cartItem addItemCmd.Quantity
        else cartItem
        
    let internal calculate cart =
        cart.LineItems 
        |> List.sumBy (fun l -> ItemTotal.value l.ItemTotal)
        |> CartTotal.create

    let internal createItem cartId (product:Product) (quantity:ItemQuantity) :CartItem=
        {
            Id = CartItemId.create 0L
            CartId = cartId
            ProductId = product.Id
            ProductName = product.Name
            Price = product.Price
            Quantity = quantity
            ItemTotal = ItemTotal.calculate product.Price quantity     
        }
         
    let addCartItem (cart:Cart) (product:Product) (cmd:AddItemCmd) =
        let f = fun l -> l.ProductId = cmd.ProductId
        let items = cart.LineItems |> List.filter f
        match items with
        | [] ->
            let newItem = createItem cart.Id product cmd.Quantity
            let newTotal = CartTotal.addItemTotal cart.CartTotal newItem.ItemTotal
            let newItems = newItem :: cart.LineItems
            { cart with LineItems = newItems; CartTotal = newTotal }
        | _ ->            
            let newItems = cart.LineItems |> List.map (updateItem cmd)
            let newCart = { cart with LineItems = newItems }
            let newTotal = calculate newCart
            { newCart with CartTotal = newTotal }