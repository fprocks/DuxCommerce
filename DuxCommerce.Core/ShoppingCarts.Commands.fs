namespace DuxCommerce.ShoppingCarts.Commands

open DuxCommerce.Common
open DuxCommerce.ShoppingCarts.PublicTypes
open DuxCommerce.ShoppingCarts.SimpleTypes
open DuxCommerce.Catalogue.SimpleTypes

type AddCartItemCommand = {
    ProductId : ProductId
    Quantity: ItemQuantity
}

module AddCartItemCommand =

    let fromRequest (request:AddCartItemRequest) :Result<AddCartItemCommand, CustomError> =
        Ok {
            ProductId = ProductId.create request.ProductId
            Quantity = ItemQuantity.create request.Quantity
        }

type UpdateCartItemCommand = {
    ProductId : ProductId
    Quantity: ItemQuantity
}

type UpdateCartCommand = {
    UpdateItems: UpdateCartItemCommand seq
}

module UpdateCartCommand =

    let fromRequest (request:UpdateCartRequest) :Result<UpdateCartCommand, CustomError> =
        let createItemCmd (request:UpdateCartItemRequest) :UpdateCartItemCommand =
            {
                ProductId = ProductId.create request.ProductId
                Quantity = ItemQuantity.create request.Quantity
            }
        Ok {
            UpdateItems = request.CartItems |> Seq.map createItemCmd
        }

type DeleteCartItemCommand = {
    ProductId: ProductId
}        

module DeleteCartItemCommand =

    let fromRequest (request:DeleteCartItemRequest) :Result<DeleteCartItemCommand, CustomError> =
        Ok {
            ProductId = ProductId.create request.ProductId
        }