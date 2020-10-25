namespace DuxCommerce.Catalogue

type ProductId = private ProductId of int64
module ProductId =
    let value (ProductId id) = id    
    let create id = ProductId id

type SalePrice = SalePrice of decimal
module SalePrice =
    let value (SalePrice price) = price
    let create price = SalePrice price
    
    let multiply qty (SalePrice p) =
        create (qty * p)
    
type RetailPrice = private RetailPrice of decimal
type ProductCost = private ProductCost of decimal

type Length = private Length of decimal
type Width = private Width of decimal
type Height = private Height of decimal
type Weight = private Weight of decimal