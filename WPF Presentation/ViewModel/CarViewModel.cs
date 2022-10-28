using Hs.Domain.Entities.SampleDbEntities;
using Hs.Domain.Interfaces;
using Hs.Infrastructure.Context;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WPF_Presentation;
using WPF_Presentation.Command;
using WPF_Presentation.Model;

namespace WPF_Presentation.ViewModel
{
   
    public class CarViewModel : ViewModelBase
    {


        protected readonly IUnitOfWork<SampleDbContext> _unitOfWork;
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly DataReceiver _dataReceiver;

        public CarViewModel(IUnitOfWork<SampleDbContext> unitOfWork,
                IHttpClientFactory httpClientFactory,
                DataReceiver dataReceiver
            )
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _dataReceiver = dataReceiver;

            _dataReceiver.DataReceived += OnDataReceiver;


            LoadFromDBCommand = new DelegateCommand(LoadFromDbAsync);
            LoadFromAPICommand = new DelegateCommand(LoadFromApiAsync);
        }

        public ObservableCollection<CarItemViewModel> Cars { get; } = new();
        public DelegateCommand LoadFromDBCommand { get; }
        public DelegateCommand LoadFromAPICommand { get; }


        //Register for DataRecieved Event
        public void OnDataReceiver(object source, EventArgs e)
        {
            MessageBox.Show("New Data Received ...");
        }

        public async void LoadFromApiAsync(object? parameter)
        {
            //var content =
            //   await Task.Run(() => JsonConvert.SerializeObject(new
            //   {
            //       apiKey = _configuration.GetValue<string>("IParsConfiguration:APIKey"),
            //       spId = farmId,
            //       mapType = "physical(json)"
            //   }));

            //Creating a IParsServices HTTPClient
            var client = _httpClientFactory.CreateClient("BackendService");
            HttpResponseMessage respons = await client.GetAsync($"/api/v1.0/Values/GetAsListAsync");


            if (respons.StatusCode != HttpStatusCode.OK)
                MessageBox.Show("Error...");



            var data = await respons.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<Response<Car>>(data);

            if (cars.result is not null)
            {
                foreach (var car in cars.result)
                {
                    Cars.Add(new CarItemViewModel(car));
                }
            }

        }

        public async void LoadFromDbAsync(object? parameter)
        {
            var cars = await _unitOfWork.GetRepository<Car>().GetAsync();

            if (cars is not null)
            {
                foreach (var car in cars)
                {
                    Cars.Add(new CarItemViewModel(car));
                }
            }
        }

    }
}
