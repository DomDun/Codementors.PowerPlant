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
                var responseBody = await _client.GetAsync($@"http://localhost:1992/api/v1/errors/{startDate.ToString("yyyy-MM-ddTHH:mm:ss")}/{endDate.ToString("yyyy-MM-ddTHH:mm:ss")}");

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
    }
}
