using DuxCommerce.Catalogue.PublicTypes;
using DuxCommerce.ShoppingCarts.PublicTypes;
using RepoDb;

namespace DuxCommerce.DatabaseMigrations
{
    public class ModelMapper
    {
        public static void Map()
        {
            FluentMapper.Entity<ProductDto>()
                .Table("[dbo].[Product]");
            
            FluentMapper.Entity<ShoppingCartDto>()
                .Table("[dbo].[ShoppingCart]");
            
            FluentMapper.Entity<CartItemDto>()
                .Table("[dbo].[ShoppingCartItem]");
        }
    }
}