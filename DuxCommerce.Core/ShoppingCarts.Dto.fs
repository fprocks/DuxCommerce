namespace DuxCommerce.ShoppingCarts.Dto

open DuxCommerce.Catalogue
open DuxCommerce.ShoppingCarts

type ValidatedAddItemRequest = {
    ProductId : ProductId
    Quantity: ItemQuantity
}

module AddCartItemRequest =
    let validate (request:AddCartItemRequest) :Result<ValidatedAddItemRequest, string> =
        Ok {
            ProductId = ProductId.create request.ProductId
            Quantity = ItemQuantity.create request.Quantity
            }