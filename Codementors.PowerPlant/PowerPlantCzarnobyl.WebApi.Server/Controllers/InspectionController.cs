using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
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
            return await _inspectionService.AddInspectionAsync(inspection);
        }

        [HttpGet]
        [Route("")]
        public List<Inspection> GetAllInspectionsAsync()
        {
            return _inspectionService.GetAllInspections();
        }

        [HttpGet]
        [Route("{id}")]
        public Inspection GetInspectionAsync(int id)
        {
            return _inspectionService.GetInspection(id);
        }

        [HttpPut]
        [Route("{id}")]
        public bool UpdateInspection([FromUri] int id, [FromBody] Inspection inspection)
        {
            return _inspectionService.UpdateInspection(id, inspection);
        }
    }
}
