using DuxCommerce.Catalogue;
using RepoDb;

namespace DuxCommerce.DatabaseMigrations
{
    public class ModelMapper
    {
        public static void Map()
        {
            FluentMapper.Entity<ProductInfo>()
                .Table("[dbo].[Product]");
        }
    }
}