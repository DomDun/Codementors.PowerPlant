using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace PowerPlantCzarnobyl.WebApi.Server.Controllers
{
    [RoutePrefix("api/v1/errors")]
    public class ErrorController : ApiController
    {
        private readonly IErrorService _errorService;

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
            return _errorService.GetAllErrors(startDate, endDate);
        }

        [HttpGet]
        [Route("errorsToDict/{startDate}/{endDate}")]
        public Dictionary<string, int> GetAllErrorsInDictionaryAsync(DateTime startDate, DateTime endDate)
        {
            return _errorService.GetAllErrorsInDictionary(startDate, endDate);
        }

        [HttpPost]
        [Route("")]
        public void AddError([FromBody] Error error)
        {
            _errorService.AddError(error);
        }
    }
}
