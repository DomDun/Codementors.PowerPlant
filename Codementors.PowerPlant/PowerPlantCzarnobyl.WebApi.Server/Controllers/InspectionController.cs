using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace PowerPlantCzarnobyl.WebApi.Server.Controllers
{
    [RoutePrefix("api/v1/inspections")]
    public class InspectionController : ApiController
    {

        private readonly IInspectionService _inspectionService;

        public InspectionController()
        {
            var inspectionRepository = new InspectionRepository();

            _inspectionService = new InspectionService(inspectionRepository);
        }

        [HttpPost]
        [Route("")]
        public async Task<bool> AddInspection([FromBody] Inspection inspection)
        {
            return await _inspectionService.AddInspection(inspection);
        }

        [HttpGet]
        [Route("")]
        public async Task<List<Inspection>> GetAllInspectionsAsync()
        {
            return await _inspectionService.GetAllInspections();
        }
    }
}
