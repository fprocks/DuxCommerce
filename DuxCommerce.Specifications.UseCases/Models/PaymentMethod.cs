namespace DuxCommerce.Specifications.UseCases.Models
{
    public class PaymentMethod
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string AdditionalDetails { get; set; }
        public string PaymentInstructions { get; set; }
    }
}
