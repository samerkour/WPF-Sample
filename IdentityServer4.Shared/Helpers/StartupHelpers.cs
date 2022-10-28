using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using IdentityServer4.Shared.Configuration.Email;
using IdentityServer4.Shared.Configuration.Sms;
using IdentityServer4.Shared.Email;
using IdentityServer4.Shared.Sms;
using IdentityServer4.Shared.Totp;

namespace IdentityServer4.Shared.Helpers
{
    public static class StartupHelpers
    {
        /// <summary>
        /// Add email senders - configuration of sendgrid, smtp senders
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddEmailSenders(this IServiceCollection services, IConfiguration configuration)
        {
            var smtpConfiguration = configuration.GetSection(nameof(SmtpConfiguration)).Get<SmtpConfiguration>();
            var sendGridConfiguration = configuration.GetSection(nameof(SendGridConfiguration)).Get<SendGridConfiguration>();

            if (sendGridConfiguration != null && !string.IsNullOrWhiteSpace(sendGridConfiguration.ApiKey))
            {
                services.AddSingleton<ISendGridClient>(_ => new SendGridClient(sendGridConfiguration.ApiKey));
                services.AddSingleton(sendGridConfiguration);
                services.AddTransient<IEmailSender, SendGridEmailSender>();
            }
            else if (smtpConfiguration != null && !string.IsNullOrWhiteSpace(smtpConfiguration.Host))
            {
                services.AddSingleton(smtpConfiguration);
                services.AddTransient<IEmailSender, SmtpEmailSender>();
            }
            else
            {
                services.AddSingleton<IEmailSender, LogEmailSender>();
            }
        }



        /// <summary>
        /// Add sms senders - configuration of sendgrid, sms senders
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSmsSenders(this IServiceCollection services, IConfiguration configuration) 
        {
            var smtpConfiguration = configuration.GetSection(nameof(SendSmsConfiguration)).Get<SendSmsConfiguration>();
            var sendGridConfiguration = configuration.GetSection(nameof(SendSmsConfiguration)).Get<SendSmsConfiguration>();

           if (smtpConfiguration != null && !string.IsNullOrWhiteSpace(smtpConfiguration.BaseUrl))
            {
                //services.AddSingleton<ISendGridClient>(_ => new SendGridClient(sendGridConfiguration.ApiKey));
                services.AddSingleton(smtpConfiguration);
                services.AddTransient<ISmsSender, FarazSmsSender>();
            }
            else
            {
                services.AddSingleton<ISmsSender, LogSmsSender>();
            }
        }


        /// <summary>
        /// Add Otp - configuration of Otp.Net
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddOtp(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton(smtpConfiguration);
            services.AddTransient<IOtp, Otp>();
        }

    }
}
