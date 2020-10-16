using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DuxCommerce.Specifications.UseCases.Hooks
{
    public interface IApiClient
    {
        HttpResponseMessage Get(string url);
        HttpResponseMessage Post(string url, object payload);
    }

    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        private readonly MyScenarioContext context;

        public ApiClient(HttpClient httpClient, MyScenarioContext context)
        {
            this.httpClient = httpClient;
            this.context = context;
        }

        public HttpResponseMessage Get(string url)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Post(string url, object payload)
        {
            throw new NotImplementedException();
        }
    }
}
