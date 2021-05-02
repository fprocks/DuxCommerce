namespace DuxCommerce.Specifications.Forms
{
	public class ShippingRateForm
	{
		public int ShippingMethod { get; set; }
		public decimal Min { get; set; }
		public decimal Max { get; set; }
		public decimal Rate { get; set; }
	}
}
