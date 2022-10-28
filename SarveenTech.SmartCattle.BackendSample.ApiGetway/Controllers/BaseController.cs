using Hs.CrossCutting;
using Hs.Domain.Interfaces;
using Hs.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Globalization;
using System.Net.Http;

namespace SarveenTech.SmartCattle.BackendSample.Gateway.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected string userId;
        protected CultureInfo culture;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IUnitOfWork<SampleDbContext> _unitOfWork;
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IConfiguration _configuration;

        public BaseController(
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUnitOfWork<SampleDbContext> unitOfWork,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            userId = httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            culture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
        }
    }
}
