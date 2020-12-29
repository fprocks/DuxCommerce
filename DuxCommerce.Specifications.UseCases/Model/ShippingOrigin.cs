namespace DuxCommerce.Specifications.UseCases.Model
{
    public class ShippingOrigin
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StateName { get; set; }
        public string CountryCode { get; set; }
    }
}
