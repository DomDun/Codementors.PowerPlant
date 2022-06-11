using Newtonsoft.Json;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.WebApi.Client.Clients
{
    public class RecievedDataWebApiClient
    {
        private readonly HttpClient _client;

        public RecievedDataWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<PowerPlantDataSet> GetData()
        {
            try
            {
                var responseBody = await _client.GetAsync($@"http://localhost:1992/api/v1/recievedData/data");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    Console.WriteLine("something is fucked up");
                }

                return JsonConvert.DeserializeObject<PowerPlantDataSet>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new PowerPlantDataSet();
            }
        }
    }
}
