using Hs.Infrastructure;
using Hs.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_Presentation;
using WPF_Presentation.ViewModel;

namespace WPF_Presentation
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
        public readonly IServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {


            //services.AddDbContext<EmployeeDbContext>(options =>
            //{
            //    options.UseSqlite("Data Source = Employee.db");
            //});


            string connstr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


            // ...
            //SQL Server 
            services.AddDbContext<SampleDbContext>(options =>
            {
                options.UseSqlServer(connstr);
                options.EnableSensitiveDataLogging(true);
            }, ServiceLifetime.Transient).AddUnitOfWork<SampleDbContext>(ServiceLifetime.Transient); // Note the App prefix 



            //services.AddDbContext<IdentityServer4AdminContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityDbConnection")))
            //.AddUnitOfWork<IdentityServer4AdminContext>(); // Note the App prefix 


            services.AddHttpContextAccessor();


            var appSettings = ConfigurationManager.AppSettings;

            if (appSettings.Count == 0)
            {
                //Console.WriteLine("AppSettings is empty.");
            }
            else
            {
                //foreach (var key in appSettings.AllKeys)
                //{
                //    Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                //}

                //Refere to Tutorial https://www.talkingdotnet.com/3-ways-to-use-httpclientfactory-in-asp-net-core-2-1/
                services.AddHttpClient();
                services.AddHttpClient("BackendService", c =>
                {
                    c.BaseAddress = new Uri(appSettings["BaseAddress"]);
                    //c.DefaultRequestHeaders.Add("Authorization", "bearer " + Configuration.GetSection("appSettings").GetSection("AccessToken").Value);
                    //c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                    //c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
                    //c.DefaultRequestHeaders.Add("Content-Type", "application/form-data");
                });
            }

            services.AddTransient<DataReceiver>();
            services.AddTransient<CarViewModel>();

            services.AddTransient<MainWindow>();
        }
    }
}
