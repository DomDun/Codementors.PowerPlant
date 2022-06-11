using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace PowerPlantCzarnobyl.WebApi.Server.Controllers
{
    [RoutePrefix("api/v1/errors")]
    internal class ErrorController : ApiController
    {
        private readonly ErrorService _errorService;

        public ErrorController()
        {
            _errorService = new ErrorService(new ErrorsRepository(), new DateProvider());
        }

        [HttpGet]
        [Route("{startDate}/{endDate}")]
        public async Task<List<Error>> GetAllErrorsAsync(DateTime startDate, DateTime endDate)
        {
            return await _errorService.GetAllErrorsAsync(startDate, endDate);
        }
    }
}
