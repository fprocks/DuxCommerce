namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue

type GetShopperCart = int64 -> Result<CartInfo, string>
type GetProduct =  int64 -> Result<ProductInfo, string>
type InsertCartItem = CartInfo -> Result<unit, string> 