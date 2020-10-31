namespace DuxCommerce.Catalogue.SimpleTypes

type ProductId = ProductId of int64
module ProductId =
    let value (ProductId id) = id    
    let create id = ProductId id

type SalePrice = SalePrice of decimal
module SalePrice =
    let value (SalePrice price) = price
    let create price = SalePrice price    
    let multiply qty (SalePrice p) =
        create (qty * p)
    
type RetailPrice = RetailPrice of decimal
module RetailPrice =    
    let value (RetailPrice price) = price
    let create price = RetailPrice price
    
type ProductCost = ProductCost of decimal
module ProductCost =
    let value (ProductCost cost) = cost
    let create cost = ProductCost cost

type Length = Length of decimal
module Length =
    let value (Length length) = length
    let create length = Length length
    
type Width = Width of decimal
module Width =
    let value (Width width) = width
    let create width = Width width
    
type Height = Height of decimal
module Height =
    let value (Height height) = height
    let create height = Height height

type Weight = Weight of decimal
module Weight =
    let value (Weight weight) = weight
    let create weight = Weight weight