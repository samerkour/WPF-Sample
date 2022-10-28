using Hs.CrossCutting;
using Hs.Domain.Interfaces;
using Hs.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using IdentityServer4.Shared.Configuration.Constants;
using System.Net.Http;
//using SarveenTech.SmartCattle.BackendSample.Gateway.ExceptionHandling;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SarveenTech.SmartCattle.BackendSample.Gateway.Controllers.V2
{
    //[Route("api/[controller]")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    [Produces("application/json", "application/problem+json")]
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    public class ValuesController : BaseController
    {

        protected readonly IStringLocalizer<ValuesController> _localizer;
        public ValuesController(
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<ValuesController> localizer,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUnitOfWork<SampleDbContext> unitOfWork
            ) : base(httpContextAccessor, sharedLocalizer, unitOfWork, httpClientFactory, configuration)
        {
            _localizer = localizer;
        }

        // GET: api/<FarmsController>
        [HttpGet("GetAsListAsync")]
        public IActionResult GetAsListAsync()
        {
            //return new string[] { "value1", "value2" };
            var str = _localizer["Title"];
            var sharedStr = _sharedLocalizer["Title"];
            return Ok(str.Value);
        }

        //// GET api/<FarmsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<FarmsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<FarmsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<FarmsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
