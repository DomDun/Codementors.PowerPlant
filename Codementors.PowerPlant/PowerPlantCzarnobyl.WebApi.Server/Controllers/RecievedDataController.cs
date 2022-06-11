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
        private readonly RecievedDataService _recievedDataService;

        public RecievedDataController()
        {
            var recievedDataRepository = new RecievedDataRepository();

            _recievedDataService = new RecievedDataService(recievedDataRepository);
        }

        [HttpGet]
        [Route("data")]
        public async Task<PowerPlantDataSetData> GetDataFromLibrary()
        {
            return RecievedDataService.Instance.NewData;
        }
    }
}
