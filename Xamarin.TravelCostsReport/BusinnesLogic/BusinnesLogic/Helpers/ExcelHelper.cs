using BusinnesLogic.Dto;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;

namespace BusinnesLogic.Helpers
{
    public class ExcelHelper
    {
        private static readonly int CITY_ITEM_NAMES_ROW = 1;

        public static IEnumerable<CityDto> ReadExcel(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("The file path given was empty.");
            }

            if (!File.Exists(filePath))
            {
                throw new Exception("The file do not exists.");
            }

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = ExcelFile.Load(filePath);
            var worksheet = workbook.Worksheets["Sheet1"];

            var result = new List<CityDto>();
            for (var rowIdx = 0; rowIdx <= worksheet.Rows.Count; rowIdx++)
            {
                //city name always on first column (column A)
                var cityNameCell = worksheet.Cells[rowIdx, 0];
                if (cityNameCell.ValueType != CellValueType.Null)
                {
                    var city = new CityDto();
                    city.Name = cityNameCell.Value.ToString();
                    var cityItems = new List<CityItemDto>();

                    for (var colIdx = 0; colIdx <= worksheet.Columns.Count; colIdx++)
                    {
                        var cityItemName = worksheet.Cells[CITY_ITEM_NAMES_ROW, colIdx];
                        if (cityItemName.ValueType != CellValueType.Null)
                        {
                            cityItems.Add(new CityItemDto()
                            {
                                Name = cityItemName.Value.ToString(),
                                Distance = Convert.ToInt32(worksheet.Cells[rowIdx, colIdx].Value)
                            });
                        }
                    }
                    city.CityItems = cityItems;
                    result.Add(city);
                 }
            }    

            return result;
        }
    }
}
