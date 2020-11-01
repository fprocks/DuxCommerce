namespace DuxCommerce.Catalogue

open DuxCommerce.Catalogue.Dto
open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Common
open DuxCommerce.Catalogue.SimpleTypes

type CreateProductUseCase = ProductInfo -> Result<ProductInfo, string>
type GetProductUseCase = int64 -> Result<ProductInfo, string>
type UpdateProductUseCase = int64 -> ProductInfo -> Result<ProductInfo, string>

module UseCases =     
    let createProduct' createProduct getProduct :CreateProductUseCase =            
        fun info ->
            result {
                let! product = info |> ProductInfo.toDomain
                let! info = product |> createProduct
                let productId = ProductId.create info.Id
                return! getProduct productId 
            }
    
    let getProduct' getProduct :GetProductUseCase =
        fun id ->
            result {
                let productId = ProductId.create id
                
                // Todo: improve to handle null value
                return! getProduct productId
            }
        
    let updateProduct' updateProduct getProduct :UpdateProductUseCase =
        fun id info ->
            result {
                let! product = info |> ProductInfo.toDomain
                let productId = ProductId.create id
                do! updateProduct productId product
                return! getProduct productId
            }