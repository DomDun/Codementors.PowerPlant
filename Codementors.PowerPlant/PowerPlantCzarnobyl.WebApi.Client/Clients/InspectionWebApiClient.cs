using Newtonsoft.Json;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.WebApi.Client.Clients
{
    public class InspectionWebApiClient
    {
        private readonly HttpClient _client;

        public InspectionWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<bool> AddInspection(Inspection inspection)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(inspection), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:1992/api/v1/inspections", content);

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return false;
                }

                return bool.Parse(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return false;
            }
        }

        public async Task<List<Inspection>> GetAllInspections()
        {
            try
            {
                var url = $@"http://localhost:1992/api/v1/inspections";
                var responseBody = await _client.GetAsync(url);

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<Inspection>();
                }

                return JsonConvert.DeserializeObject<List<Inspection>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<Inspection>();
            }
        }
    }
}
