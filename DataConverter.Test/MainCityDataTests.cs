using DataConverter.Models;
using Moq;
using System;
using System.IO;
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

                obj.Filter(x => x.Name == "Antalya");

                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var fileName = "CsvToXmlAntalyaResult.xml";

                File.WriteAllText(Path.Combine(desktopPath,fileName), obj.AsXml());

                

                // Assert
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
