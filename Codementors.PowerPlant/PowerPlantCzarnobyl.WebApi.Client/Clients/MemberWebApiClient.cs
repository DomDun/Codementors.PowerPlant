using Newtonsoft.Json;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.WebApi.Client.Clients
{
    public class MemberWebApiClient
    {
        private readonly HttpClient _client;

        public MemberWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<bool> Login(string login, string password)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new MemberCredentials { Login = login, Password = password }), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:1992/api/v1/members/credentials", content);

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

        public async Task<bool> AddMember(MemberWebApi member)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(member), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:1992/api/v1/members", content);

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

        internal async Task<bool> Delete(string memberToDelete)
        {
            try
            {
                var responseBody = await _client.DeleteAsync($@"http://localhost:1992/api/v1/members/{memberToDelete}");

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

        public async Task<MemberWebApi> CheckMemberRole(string login)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new MemberCredentials { Login = login}), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.GetAsync($@"http://localhost:1992/api/v1/members/{login}");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new MemberWebApi();
                }

                return JsonConvert.DeserializeObject<MemberWebApi>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new MemberWebApi();
            }
        }
    }
}
