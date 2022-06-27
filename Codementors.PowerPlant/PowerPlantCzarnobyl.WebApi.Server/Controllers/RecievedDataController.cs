using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using System.Web.Http;

namespace PowerPlantCzarnobyl.WebApi.Server.Controllers
{
    [RoutePrefix("api/v1/recievedData")]
    public class RecievedDataController : ApiController
    {

        [HttpGet]
        [Route("data")]
        public PowerPlantDataSetData GetDataFromLibrary()
        {
            return ReceivedDataService.Instance.NewData;
        }
    }
}
