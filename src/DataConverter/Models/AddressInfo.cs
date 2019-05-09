using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataConverter.Models
{
    [XmlRoot("AddressInfo")]
    internal class AddressInfo
    {
        [XmlElement("City")]
        public List<City> Cities { get; set; }
    }
    public class City
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("code")]
        public string Code { get; set; }
        [XmlElement("District")]
        public List<District> Districts { get; set; }
    }

    public class District
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("Zip")]
        public List<ZipCode> ZipCode { get; set; }
    }
    public class ZipCode
    {
        [XmlAttribute("code")]
        public string Code { get; set; }
    }
}

