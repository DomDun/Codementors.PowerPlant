using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace PowerPlantCzarnobyl.WebApi.Server.Controllers
{
    [RoutePrefix("api/v1/errors")]
    public class ErrorController : ApiController
    {
        private readonly IErrorsService _errorService;

        public ErrorController()
        {
            var errorsRepository = new ErrorsRepository();
            var dateProvider = new DateProvider();

            _errorService = new ErrorService(errorsRepository, dateProvider);
        }

        [HttpGet]
        [Route("errors/{startDate}/{endDate}")]
        public List<Error> GetAllErrorsAsync(DateTime startDate, DateTime endDate)
        {
            return _errorService.GetAllErrorsAsync(startDate, endDate);
        }

        [HttpGet]
        [Route("errorsToDict/{startDate}/{endDate}")]
        public Dictionary<string, int> GetAllErrorsInDictionaryAsync(DateTime startDate, DateTime endDate)
        {
            return _errorService.GetAllErrorsInDictionaryAsync(startDate, endDate);
        }
    }
}
