using DuxCommerce.Catalogue;
using DuxCommerce.Catalogue.PublicTypes;
using DuxCommerce.ShoppingCarts;
using DuxCommerce.ShoppingCarts.PublicTypes;
using RepoDb;

namespace DuxCommerce.DatabaseMigrations
{
    public class ModelMapper
    {
        public static void Map()
        {
            FluentMapper.Entity<ProductInfo>()
                .Table("[dbo].[Product]");
            
            FluentMapper.Entity<CartInfo>()
                .Table("[dbo].[ShoppingCart]");
            
            FluentMapper.Entity<CartItemInfo>()
                .Table("[dbo].[ShoppingCartItem]");
        }
    }
}