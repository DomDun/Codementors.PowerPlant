using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;

namespace PowerPlantCzarnobyl.WebApi.Server.Controllers
{
    [RoutePrefix("api/v1/recievedData")]
    public class RecievedDataController : ApiController
    {

        [HttpGet]
        [Route("data")]
        public async Task<PowerPlantDataSetData> GetDataFromLibrary()
        {
            return RecievedDataService.Instance.NewData;
        }
    }
}
