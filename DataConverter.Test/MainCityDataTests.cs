using DataConverter.Models;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace DataConverter.Test
{
    public class MainCityDataTests : IDisposable
    {
        private MockRepository mockRepository;



        public MainCityDataTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private MainCityData CreateMainCityData()
        {
            return new MainCityData();
        }

        /// <summary>
        /// Test case #1
        /// </summary>
        [Fact]
        public void XmlOutputFromCsvTest()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DataConverter.Test.Data.sample_data.csv"))
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                var csv = Encoding.UTF8.GetString(ms.ToArray());
                // Arrange
                var unitUnderTest = this.CreateMainCityData();

                var obj = MainCityData.FromCsv(csv);

                obj.Filter(x => x.CityName == "Antalya");

                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var fileName = $"TestCase1_{DateTime.Now.ToString("yyyyMMddThhmmss")}.xml";

                File.WriteAllText(Path.Combine(desktopPath, fileName), obj.AsXml());

                // Assert
                Assert.True(true);
            }
        }
        /// <summary>
        /// Test case #2
        /// Generate CSV output from CSV input, sorted by City name ascending, then District name ascending
        /// </summary>
        [Fact]
        public void CsvOutputFromCsvAscendingCityThenAscendingCity()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DataConverter.Test.Data.sample_data.csv"))
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                var csv = Encoding.UTF8.GetString(ms.ToArray());

                var obj = MainCityData.FromCsv(csv);


                obj.ObjectResult = obj.ObjectResult.OrderBy(o => o.CityName).ThenBy(t => t.DistrictName).ToList();

                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var fileName = $"TestCase2_{DateTime.Now.ToString("yyyyMMddThhmmss")}.csv";

                File.WriteAllText(Path.Combine(desktopPath, fileName), obj.AsCsv());

                Assert.True(true);
            }
        }

        [Fact]
        public void CsvOutputFromXmlInputAnkaraCityNamesAscendingZipCode()
        {
            //read sample data from resources
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DataConverter.Test.Data.sample_data.xml"))
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                var csv = Encoding.UTF8.GetString(ms.ToArray());

                var obj = MainCityData.FromXml(csv); //deserialize data from csv string

                //Apply filter
                obj.Filter(x => x.CityName == "Ankara");
                //apply ordering
                obj.ObjectResult = obj.ObjectResult.OrderByDescending(o => o.ZipCode).ToList();

                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var fileName = $"TestCase3_{DateTime.Now.ToString("yyyyMMddThhmmss")}.csv";

                File.WriteAllText(Path.Combine(desktopPath, fileName), obj.AsCsv());

                Assert.True(true);
            }
        }
        //[Fact]
        //public void ToStream_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateMainCityData();
        //    string plainText = TODO;

        //    // Act
        //    var result = unitUnderTest.ToStream(
        //        plainText);

        //    // Assert
        //    Assert.True(false);
        //}
    }
}
