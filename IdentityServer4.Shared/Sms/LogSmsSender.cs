using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer4.Shared.Sms
{
    public class LogSmsSender : ISmsSender 
    {
        private readonly ILogger<LogSmsSender> _logger;

        public LogSmsSender(ILogger<LogSmsSender> logger) 
        {
            _logger = logger;
        }

        public Task SendSmsAsync(string toNo, string totpCode)
        {
            _logger.LogInformation($"Sending to: {toNo}, totpCode: {totpCode}");

            return Task.FromResult(0);
        }
    }
}