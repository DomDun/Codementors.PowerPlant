using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System.Web.Http;

namespace PowerPlantCzarnobyl.WebApi.Server.Controllers
{
    [RoutePrefix("api/v1/inspections")]
    public class InspectionController
    {

        private readonly InspectionService _inspectionService;

        public InspectionController()
        {
            _inspectionService = new InspectionService(new InspectionRepository());
        }

        [HttpPost]
        [Route("")]
        public bool AddInspection([FromBody] Inspection inspection)
        {
            return _inspectionService.AddInspection(inspection);
        }
    }
}
