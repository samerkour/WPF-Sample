using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using SendGrid;
using IdentityServer4.Shared.Configuration.Email;
using System.Threading.Tasks;
using System;
using IdentityServer4.Shared.Configuration.Sms;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;

namespace IdentityServer4.Shared.Sms 
{
    public interface ISmsSender
    {
        //Task SendSmsAsync(string toNo, string totpCode, IDictionary<string, string> inputData);

        Task SendSmsAsync(string toNo, string totpCode);
    }

    public class RestRequestSmsBody { 
        public string op { get; set; }
        public string user { get; set; }
        public string pass { get; set; }

        public string fromNum { get; set; }
        public string toNum { get; set; }

        public string patternCode { get; set; } 
        public IDictionary<string, string> inputData { get; set; } 

    }
    public class FarazSmsSender : ISmsSender
    {
        private readonly ILogger<FarazSmsSender> _logger;
        private readonly SendSmsConfiguration _configuration;
        RestClient _client = new RestClient();
        RestRequest _request = new RestRequest(Method.POST);

        public FarazSmsSender(ILogger<FarazSmsSender> logger, SendSmsConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client.BaseUrl = new Uri(configuration.BaseUrl);
            _request.AddHeader("cache-control", "no-cache");
            _request.AddHeader("Content-Type", "application/json");
        }

        public Task SendSmsAsync(string toNo, string totpCode)
        {
            _logger.LogInformation($"Sending to: {toNo}, totpCode: {totpCode}");

            try
            {
                //_request.AddParameter("SendTotp", 
                //    "{\"op\" : \"pattern\"" +
                //    ",\"user\" : \"yourUsername\"" +
                //    ",\"pass\":  \"yourPassword\"" +
                //    ",\"fromNum\" : \"100009\"" +
                //    ",\"toNum\": \"09122000098\"" +
                //    ",\"patternCode\": \"545\"" +
                //    ",\"inputData\" : [{\"verification-code\": \"asdadas\"}]}"
                //, ParameterType.RequestBody);

                var body = new RestRequestSmsBody()
                { 
                    op= "patternV2",
                    user=_configuration.UserName,
                    pass=_configuration.Password,
                    fromNum=_configuration.FromNo,
                    toNum=toNo,
                    patternCode=_configuration.PatternCode,
                    inputData= new Dictionary<string, string>() { 
                        {"verification-code", totpCode }
                    }
                };

                _request.AddParameter("SendTotp",
                   JsonConvert.SerializeObject(body)
                , ParameterType.RequestBody);

                IRestResponse response = _client.Execute(_request);

                //Console.WriteLine(response.Content);

                _logger.LogInformation(response.Content);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Sending to: {toNo}, totpCode: {totpCode}");
                throw;
            }
        }
    }
}
