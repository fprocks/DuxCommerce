namespace DuxCommerce.ShoppingCarts.DomainTypes

open DuxCommerce.Catalogue
open DuxCommerce.Common
open DuxCommerce.ShoppingCarts

type ValidatedAddItemRequest = {
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

type AddCartItem = Cart -> Product -> ValidatedAddItemRequest -> Cart


module ShoppingCart =
    let internal update (itemRequest:ValidatedAddItemRequest) cartItem =
        let update item quantity = 
            let newQuantity = ItemQuantity.add item.Quantity quantity
            let newTotal = ItemTotal.calculate item.Price newQuantity
            { cartItem with Quantity = newQuantity; ItemTotal = newTotal }
        
        if cartItem.ProductId = itemRequest.ProductId
        then update cartItem itemRequest.Quantity
        else cartItem
        
    let internal calculate cart =
        cart.LineItems |> List.sumBy (fun l -> ItemTotal.value l.ItemTotal)
         
    let addCartItem (cart:Cart) (product:Product) (request:ValidatedAddItemRequest) =
        let f = fun l -> l.ProductId = request.ProductId
        let items = cart.LineItems |> List.filter f
        match items with
        | [] ->
            let newCartItem = {
                Id = CartItemId.create 0L
                CartId = cart.Id
                ProductId = product.Id
                ProductName = product.Name
                Price = product.Price
                Quantity = request.Quantity
                ItemTotal = ItemTotal.calculate product.Price request.Quantity     
            }
            let newTotal = CartTotal.addItemTotal cart.CartTotal newCartItem.ItemTotal
            let newItems = newCartItem :: cart.LineItems
            let newCart = { cart with LineItems = newItems; CartTotal = newTotal }
            newCart
        | _ ->            
            let newItems = cart.LineItems |> List.map (update request)
            let newCart = {cart with LineItems = newItems}
            let newTotal = CartTotal.create (calculate newCart)
            {newCart with CartTotal = newTotal}