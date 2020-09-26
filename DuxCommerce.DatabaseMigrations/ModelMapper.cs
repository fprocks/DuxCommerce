using DuxCommerce.Catalogue;
using RepoDb;

namespace DuxCommerce.DatabaseMigrations
{
    public class ModelMapper
    {
        public static void Map()
        {
            FluentMapper.Entity<Product>()
                .Table("[dbo].[Product]")
                .Primary(x => x.Id)
                .Identity(x => x.Id);
        }
    }
}