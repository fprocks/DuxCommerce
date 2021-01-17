using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DuxCommerce.Specifications.Utilities
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, object payload);
        Task<HttpResponseMessage> PutAsync(string url, object payload);
        Task<HttpResponseMessage> DeleteAsync(string url, object payload);
    }

    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), url);
            return await SendRequestAsync(request);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object payload)
        {
            var method = "POST";
            return await SendRequest(url, method, payload);
        }

        public async Task<HttpResponseMessage> PutAsync(string url, object payload)
        {
            var method = "PUT";
            return await SendRequest(url, method, payload);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url, object payload)
        {
            var method = "DELETE";
            return await SendRequest(url, method, payload);
        }

        private async Task<HttpResponseMessage> SendRequest(string url, string method, object payload)
        {
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod(method), url) { Content = content };

            return await SendRequestAsync(request);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            // Todo: handle cross-cutting concerns here
            return await _httpClient.SendAsync(request);
        }
    }
}
