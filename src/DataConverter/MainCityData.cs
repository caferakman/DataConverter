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
        public IList<CsvAddressInfo> ObjectResult { get; set; }

        public string AsXml()
        {
            return Helpers.XmlHelpers.XmlSerializeToString(new XmlAddressInfo
            {
                Cities = ObjectResult.GroupBy(g => g.CityName).Select(s => new City
                {
                    Code = s.FirstOrDefault().CityCode,
                    Name = s.Key,
                    Districts = s.GroupBy(g2 => g2.DistrictName).Select(s2 => new District
                    {
                        Name = s2.Key,
                        ZipCodes = s2.Select(s3 => new ZipCode
                        {
                            Code = s3.ZipCode
                        }).ToList(),
                    }).ToList()
                }).ToList()
            });
        }

        public string AsCsv()
        {
            return CsvSerializer.SerializeToCsv(ObjectResult);
        }

        public IFilterable Filter(Func<CsvAddressInfo, bool> filter)
        {
            ObjectResult = ObjectResult.Where(filter).ToList();
            return this; //Fluent Api
        }

        #region Static Members

        public static MainCityData FromCsv(string csv)
        {
            var addresses = csv.Split("\r\n").Select(x => x.Split(',')).Where(x => x.Length == 4)
                .Select(s => new CsvAddressInfo
                {
                    CityName = s[0].Trim(),
                    CityCode = s[1].Trim(),
                    DistrictName = s[2].Trim(),
                    ZipCode = s[3].Trim()
                }).ToList();

            addresses.RemoveAt(0); // remove first line


            return new MainCityData
            {
                ObjectResult = addresses
            };
        }

        public static MainCityData FromXml(string xml)
        {
            var cities = Helpers.XmlHelpers.XmlDeserializeFromString<XmlAddressInfo>(xml).Cities;


            IEnumerable<CsvAddressInfo> ConvertToCsvFormat(IList<City> data)
            {
                foreach (var city in data)
                    foreach (var district in city.Districts)
                        foreach (var zipcode in district.ZipCodes)
                            yield return new CsvAddressInfo
                            {
                                CityCode = city.Code,
                                CityName = city.Name,
                                ZipCode = zipcode.Code,
                                DistrictName = district.Name
                            };
            }

            return new MainCityData
            {
                ObjectResult = ConvertToCsvFormat(cities).ToList(),
            };
        }
        #endregion

        public byte[] ToByteArray(string plainText)
        {
            return Encoding.ASCII.GetBytes(plainText);
        }

        public MemoryStream ToStream(string plainText)
        {
            return new MemoryStream(ToByteArray(plainText));
        }
    }
}
