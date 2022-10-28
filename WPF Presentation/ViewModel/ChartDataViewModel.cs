using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WPF_Presentation.Model;

namespace WPF_Presentation.ViewModel
{
    public class ChartDataViewModel
    {

        public string? Title { get; set; }
        public int? Count { get; set; }
        public List<Item>? ChartValues { get; set; }


        public ChartDataViewModel()
        {
            using (var fileStream = File.Open(@"Data\ChartData.xml", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ChartDataXMLDocument));
                var XMLDocument = (ChartDataXMLDocument)serializer.Deserialize(fileStream);

                Title = XMLDocument.Title;

                Count = XMLDocument.Count?.Value;

                ChartValues = XMLDocument?.ChartValues;

            }
        }

      

     


    }
}
