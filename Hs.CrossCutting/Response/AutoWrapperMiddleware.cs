using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoWrapper.Base;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Hs.CrossCutting;

namespace AutoWrapper
{
    internal class AutoWrapperMiddleware : WrapperBase
    {
        private readonly AutoWrapperMembers _awm;
        public AutoWrapperMiddleware(
            RequestDelegate next, 
            AutoWrapperOptions options, 
            ILogger<AutoWrapperMiddleware> logger, 
            IActionResultExecutor<ObjectResult> executor,
            IStringLocalizer<SharedResource> sharedLocalizer) 
            : base(next, options, logger, executor,sharedLocalizer)
        {
            var jsonSettings = Helpers.JSONHelper.GetJSONSettings(options.IgnoreNullValue, options.ReferenceLoopHandling, options.UseCamelCaseNamingStrategy);
            _awm = new AutoWrapperMembers(options, logger, jsonSettings, sharedLocalizer);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await InvokeAsyncBase(context, _awm);
        }
    }

    internal class AutoWrapperMiddleware<T> : WrapperBase
    {
        private readonly AutoWrapperMembers _awm;
        public AutoWrapperMiddleware(
            RequestDelegate next, 
            AutoWrapperOptions options, 
            ILogger<AutoWrapperMiddleware> logger, 
            IActionResultExecutor<ObjectResult> executor,
            IStringLocalizer<SharedResource> sharedLocalizer)
            : base(next, options, logger, executor,sharedLocalizer)
        {
            var (Settings, Mappings) = Helpers.JSONHelper.GetJSONSettings<T>(options.IgnoreNullValue, options.ReferenceLoopHandling, options.UseCamelCaseNamingStrategy);
            _awm = new AutoWrapperMembers(options, logger, Settings, sharedLocalizer, Mappings, true);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await InvokeAsyncBase(context, _awm);
        }

    }
}
