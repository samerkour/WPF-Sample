using Hs.Domain.Entities.SampleDbEntities;
using Hs.Domain.Interfaces;
using Hs.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Presentation;
using WPF_Presentation.ViewModel;

namespace WPF_Presentation
{


    
    
    public class Response<T> 
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public List<T> result { get; set; }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		protected readonly IUnitOfWork<SampleDbContext> _unitOfWork;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IHttpClientFactory _httpClientFactory;

        protected readonly DataReceiver _dataReceiver;
        protected readonly CarViewModel _carViewmodel; 


        public MainWindow(IUnitOfWork<SampleDbContext> unitOfWork, 
                IHttpContextAccessor httpContextAccessor, 
                IHttpClientFactory httpClientFactory,
                DataReceiver dataReceiver,
                CarViewModel carViewModel
            )
        {
            InitializeComponent();
			_unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _dataReceiver = dataReceiver;
            _carViewmodel = carViewModel;

            DataContext = _carViewmodel;

        }

        /// <summary>
        /// Safe cross-thread call by  Use a BackgroundWorker.
        /// Reference: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-make-thread-safe-calls?view=netdesktop-6.0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
		{
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }

		private void mnuNew_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("New");
		}

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(100);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }

   
      
        
    }

}
