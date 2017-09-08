using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Restful_API_Sample
{
    public class ApiClient
    {
        private readonly List<Uri> _serverUrls;
        private readonly IConfigurationRoot _configuration;
        private readonly HttpClient _apiClient;
        private RetryPolicy _serverRetryPolicy;
        private int _currentConfigIndex;
        
        public ApiClient(IConfigurationRoot configuration)
        {
            _configuration = configuration;

            _apiClient = new HttpClient();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes("zhulige:zhulige")));

            _serverUrls = new List<Uri>();
        }

        public async Task Initialize()
        {
            var consulClient = new ConsulClient(c =>
            {
                var uri = new Uri(_configuration["consulConfig:address"]);
                c.Address = uri;
            });

            //_logger.LogInformation("Discovering Services from Consul.");

            var services = await consulClient.Agent.Services();
            foreach (var service in services.Response)
            {
                var isSchoolApi = service.Value.Tags.Any(t => t == "Demo");
                if (isSchoolApi)
                {
                    var serviceUri = new Uri($"{service.Value.Address}:{service.Value.Port}");
                    _serverUrls.Add(serviceUri);
                }
            }

            //_logger.LogInformation($"{_serverUrls.Count} endpoints found.");
            var retries = _serverUrls.Count * 2 - 1;
            //_logger.LogInformation($"Retry count set to {retries}");

            _serverRetryPolicy = Policy.Handle<HttpRequestException>()
               .RetryAsync(retries, (exception, retryCount) =>
               {
                   ChooseNextServer(retryCount);
               });
        }

        private void ChooseNextServer(int retryCount)
        {
            if (retryCount % 2 == 0)
            {
              //  _logger.LogWarning("Trying next server... \n");
                _currentConfigIndex++;

                if (_currentConfigIndex > _serverUrls.Count - 1)
                    _currentConfigIndex = 0;
            }
        }

        public virtual Task<IEnumerable<string>> Get(string m)
        {
            var serverUrl = _serverUrls[_currentConfigIndex];
            var requestPath = $"{serverUrl}api/v1/" + m;
            
            return _serverRetryPolicy.ExecuteAsync(async () =>
            {
                //_logger.LogInformation($"Making request to {requestPath}");
                var response = await _apiClient.GetAsync(requestPath).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<IEnumerable<string>>(content);
            });
        }

        //public virtual Task<IEnumerable<Course>> GetCourses()
        //{
        //    return _serverRetryPolicy.ExecuteAsync(async () =>
        //    {
        //        var serverUrl = _serverUrls[_currentConfigIndex];
        //        var requestPath = $"{serverUrl}api/courses";

        //        _logger.LogInformation($"Making request to {serverUrl}");
        //        var response = await _apiClient.GetAsync(requestPath).ConfigureAwait(false);
        //        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        //        return JsonConvert.DeserializeObject<IEnumerable<Course>>(content);
        //    });
        //}
    }
}
