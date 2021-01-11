using DuxCommerce.Settings.PublicTypes;
using DuxCommerce.Specifications.UseCases.Hooks;
using DuxCommerce.Specifications.UseCases.Models;
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

        private PaymentMethod _methodRequest;
        private PaymentMethodDto _methodCreated;

        public PaymentMethodSteps(StepsContext context, IApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        [Given(@"Tom enters the following payment method information:")]
        public void GivenTomEntersTheFollowingPaymentMethodInformationAsync(Table table)
        {
            _methodRequest = table.CreateSet<PaymentMethod>().FirstOrDefault();
        }

        [When(@"Tome saves the payment method")]
        public async Task WhenTomeSavesThePaymentMethodAsync()
        {
            var apiResult = await _apiClient.PostAsync("api/paymentmethods", _methodRequest);
            _context.ApiResult = apiResult;
            _methodCreated = await GetCreatedMethod(apiResult);
        }

        [Then(@"payment method should be created as expected")]
        public void ThenPaymentMethodShouldBeCreatedAsExpected()
        {            
        }

        private async Task<PaymentMethodDto> GetCreatedMethod(HttpResponseMessage apiResult)
        {
            var resultString = await apiResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaymentMethodDto>(resultString);
        }
    }
}