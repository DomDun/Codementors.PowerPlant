using Newtonsoft.Json;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.WebApi.Client.Clients
{
    public class ErrorWebApiClient
    {
        private readonly HttpClient _client;

        public ErrorWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<List<Error>> GetAllErrors(DateTime startDate, DateTime endDate)
        {
            try
            {
                var url = $@"http://localhost:1992/api/v1/errors/errors/{startDate:yyyy-MM-ddTHH:mm:ss}/{endDate:yyyy-MM-ddTHH:mm:ss}";
                var responseBody = await _client.GetAsync(url);

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<Error>();
                }

                return JsonConvert.DeserializeObject<List<Error>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<Error>();
            }
        }

        public async Task<Dictionary<string, int>> GetAllErrorsInDictionary(DateTime startDate, DateTime endDate)
        {
            try
            {
                var url = $@"http://localhost:1992/api/v1/errors/errorsToDict/{startDate:yyyy-MM-ddTHH:mm:ss}/{endDate:yyyy-MM-ddTHH:mm:ss}";
                var responseBody = await _client.GetAsync(url);

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new Dictionary<string, int>();
                }

                return JsonConvert.DeserializeObject<Dictionary<string, int>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new Dictionary<string, int>();
            }
        }

        public async void AddError(Error error)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(error), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:1992/api/v1/errors", content);

                var result = await responseBody.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
