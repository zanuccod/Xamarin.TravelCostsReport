using BusinnesLogic.Dto;
using BusinnesLogic.Helpers;
using BusinnesLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinnesLogic.Tests.Helpers
{
    public class ExcelHelperTest
    {
        [Fact]
        public void ReadExcel_FilePathEmpty_ShouldReturnException()
        {
            Assert.Throws<Exception>(() => ExcelHelper.ReadExcel(string.Empty));
        }
        [Fact]
        public void ReadExcel_FileNotExists_ShouldReturnException()
        {
            Assert.Throws<Exception>(() => ExcelHelper.ReadExcel("excelPower"));
        }
        [Theory]
        //[InlineData("travelReportOff.ods")] was tested
        [InlineData("travelReportTest.ods")]
        public void ReadExcel_FileGiven_ShouldReturnListOfCities(string filePath)
        {
            //Arrange
            var fileName = $"TestFiles/{filePath}";
            var expectedResult = new List<CityDto>()
            {
                new CityDto()
                {
                    Name = "staranzano",
                    CityItems = new List<CityItemDto>()
                    {
                        new CityItemDto()
                        {
                            Name = "Fogliano",
                            Distance = 9
                        } ,
                        new CityItemDto()
                        {
                            Name = "Grado centro",
                            Distance = 24
                        }
                    }
                },
                new CityDto()
                {
                    Name = "S.Pier",
                    CityItems = new List<CityItemDto>()
                    {
                        new CityItemDto()
                        {
                            Name = "Fogliano",
                            Distance = 3
                        } ,
                        new CityItemDto()
                        {
                            Name = "Grado centro",
                            Distance = 24
                        }
                    }
                }
            };

            //Act
            var result = ExcelHelper.ReadExcel(fileName);

            //Assert
            var boolResult = true;

            if (result.Count() != expectedResult.Count())
                boolResult = false;

            for (var idx = 0; idx < result.Count(); idx++)
            {
                boolResult &= result.ElementAt(idx).Equals(expectedResult.ElementAt(idx));
            }

            Assert.True(boolResult);
        }
    }
}
