using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace WPF_Presentation.Model
{
    [XmlRoot("ChartDataXMLDocument")]
    public class ChartDataXMLDocument
    {
        public string? Title { get; set; }

        public Count? Count { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Item")]
        public List<Item>? ChartValues { get; set; } 
    }

    public class Count:Item
    {  
    }

    public class Item 
    {
        [XmlAttribute("value")]
        public int Value { get; set; }
    }
}
