using DuxCommerce.Catalogue;
using RepoDb;

namespace DuxCommerce.DatabaseMigrations
{
    public class ModelMapper
    {
        public static void Map()
        {
            FluentMapper.Entity<CreateProductRequest>()
                .Table("[dbo].[Product]")
                .Column(x => x.Name, "Name")
                .Column(x => x.Description, "Description")
                .Column(x => x.Price, "Price")
                .Column(x => x.Retail, "Retail")
                .Column(x => x.Cost, "Cost")
                .Column(x => x.Length, "Length")
                .Column(x => x.Width, "Width")
                .Column(x => x.Height, "Height")
                .Column(x => x.Weight, "Weight")
                .Column(x => x.ShippingType, "ShippingType")
                .Column(x => x.SKU, "SKU")
                .Column(x => x.Barcode, "Barcode")
                .Column(x => x.TrackInventory, "TrackInventory")
                .Column(x => x.OutOfStockRule, "OutOfStockRule");
        }
    }
}