using System.Collections.Generic;

namespace DuxCommerce.Specifications.Forms.Shipping
{
	public class ShippingMethodForm
	{
		public string Name { get; set; }
		public string MethodType { get; set; }
		public List<ShippingRateForm> Rates { get; set; }
	}
}