using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataConverter.Abstraction;
using ServiceStack.Text;

namespace DataConverter.Models
{
    public class MainCityData : ICsvData, IXmlData, IFilterable, IFileData
    {
        public IList<City> ObjectResult { get; set; }

        public string AsXml()
        {
            return Helpers.XmlHelpers.XmlSerializeToString(new AddressInfo
            {
                Cities = ObjectResult.ToList()
            });
        }

        public string AsCsv()
        {
            return CsvSerializer.SerializeToCsv(ObjectResult);
        }

        public IFilterable Filter(Func<City, bool> filter)
        {
            ObjectResult = ObjectResult.Where(filter).ToList();
            return this; //Fluent Api
        }

        #region Static Members

        public static MainCityData FromCsv(string csv)
        {
            //var root = csv.Split("\n").Select(x => x.Split(','))
            //    .Select(s => new
            //    {
            //        CityName = s[0].Trim(),
            //        CityCode = s[1].Trim(),
            //        DistrictName = s[2].Trim(),
            //        ZipCode = s[3].Trim()
            //    }).ToList();


            var result = new List<City>();
            


            return new MainCityData
            {
                ObjectResult = result
            };
        }

        public static MainCityData FromXml(string xml)
        {
            return new MainCityData
            {
                ObjectResult = Helpers.XmlHelpers.XmlDeserializeFromString<AddressInfo>(xml).Cities,
            };
        }


        #endregion

        public byte[] ToByteArray(string plainText)
        {
            return Encoding.ASCII.GetBytes(plainText);
        }

        public MemoryStream ToStream(string plainText)
        {
            return  new MemoryStream(ToByteArray(plainText));
        }
    }
}
