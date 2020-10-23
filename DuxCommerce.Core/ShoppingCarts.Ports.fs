namespace DuxCommerce.ShoppingCarts

open DuxCommerce.Catalogue
open DuxCommerce.Common


type GetShopperCart = Result<CartInfo, string>

type GetProduct =  int64 -> Result<ProductInfo, string>

type SaveCart = Cart -> Result<CartInfo, string> 