using System.Web.Http;

namespace PowerPlantCzarnobyl.WebApi.Server.Controllers
{
    [RoutePrefix("api/v1/status")]
    public class StatusController : ApiController
    {
        [HttpGet()]
        [Route("")]
        public string Status()
        {
            return "Status OK PowerPlantCzarnobyl.WebApi.Server";
        }
    }
}
