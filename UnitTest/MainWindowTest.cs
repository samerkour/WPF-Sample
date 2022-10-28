using Hs.Domain.Interfaces;
using Hs.Infrastructure.Context;
using System.Net.Http;
using WPF_Presentation;
using WPF_Presentation.ViewModel;
using Xunit;

namespace UnitTest
{
    public class MainWindowTest
    {

        protected readonly IUnitOfWork<SampleDbContext> _unitOfWork;
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly DataReceiver _dataReceiver;
        public MainWindowTest(IUnitOfWork<SampleDbContext> unitOfWork,
               IHttpClientFactory httpClientFactory,
               DataReceiver dataReceiver
           )
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _dataReceiver = dataReceiver;
        }



        [Fact]
        public void Test1()
        {
            // Arrange
            CarViewModel cvm = new CarViewModel(_unitOfWork,_httpClientFactory,_dataReceiver);

            // Act
            cvm.LoadFromDbAsync(null);

            // Assert
            Assert.True(cvm.Cars.Count == 4);

        }
    }
}