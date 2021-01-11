using DuxCommerce.Settings.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DuxCommerce.Specifications.UseCases.Steps
{
    [Binding]
    public class PaymentMethodSteps
    {
        private readonly StepsContext _context;
        private readonly IApiClient _apiClient;

        public PaymentMethodSteps(StepsContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom enters the following payment method information:")]
        public void GivenTomEntersTheFollowingPaymentMethodInformation(Table table)
        {
        }

        [When(@"Tome saves the payment method")]
        public void WhenTomeSavesThePaymentMethod()
        {
        }
    }
}