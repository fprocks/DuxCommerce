namespace DuxCommerce.Catalogue.Ports

open DuxCommerce.Catalogue.InternalTypes
open DuxCommerce.Catalogue.PublicTypes
open DuxCommerce.Catalogue.SimpleTypes
open DuxCommerce.Common

type CreateProduct = Product -> Result<ProductInfo, CustomError>
type GetProduct = ProductId -> Result<ProductInfo, CustomError>
type UpdateProduct = ProductId -> Product -> Result<unit, CustomError>  
