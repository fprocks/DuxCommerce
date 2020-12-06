using DuxCommerce.Catalogue.PublicTypes;
using DuxCommerce.Settings.PublicTypes;
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

            FluentMapper.Entity<StoreDetailsDto>()
                .Table("[dbo].[StoreDetails]");
            
            FluentMapper.Entity<AddressDto>()
                .Table("[dbo].[Address]");

            FluentMapper.Entity<LocationDto>()
                .Table("[dbo].[Location]");

            FluentMapper.Entity<ShippingCountryDto>()
                .Table("[dbo].[ShippingCountry]");

            FluentMapper.Entity<ShippingStateDto>()
                .Table("[dbo].[ShippingState]");

            FluentMapper.Entity<ShippingProfileDto>()
                .Table("[dbo].[ShippingProfile]");
            
            FluentMapper.Entity<ShippingZoneDto>()
                .Table("[dbo].[ShippingZone]");

            FluentMapper.Entity<ShippingLocationDto>()
                .Table("[dbo].[ShippingLocation]");
            
            FluentMapper.Entity<ShippingRatesDto>()
                .Table("[dbo].[ShippingRate]");
        }
    }
}