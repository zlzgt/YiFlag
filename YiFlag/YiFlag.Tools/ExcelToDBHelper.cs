using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Tools
{
   public static class ExcelToDBHelper
    {
        public static DataSet ImportExcel(string filePath)
        {
            DataSet ds = null;

            try
            {
                //打开文件
                FileStream fileStream = new FileStream(filePath, FileMode.Open);

                XSSFWorkbook workbook = new XSSFWorkbook(fileStream);   //.xlsx 后缀的Excel使用
                //HSSFWorkbook workbook = new HSSFWorkbook(fileStream);     //.xls 后缀的Excel使用
                ISheet sheet = null;
                IRow row = null;

                ds = new DataSet();
                DataTable dt = null;

                for (int i = 0; i < workbook.Count; i++)
                {
                    dt = new DataTable();
                    dt.TableName = "table" + i.ToString();

                    //获取 sheet 表
                    sheet = workbook.GetSheetAt(i);

                    //起始行索引
                    int rowIndex = sheet.FirstRowNum;

                    //获取行数
                    int rowCount = sheet.LastRowNum;

                    //获取第一行
                    IRow firstRow = sheet.GetRow(rowIndex);

                    //起始列索引
                    int colIndex = firstRow.FirstCellNum;

                    //获取列数
                    int colCount = firstRow.LastCellNum;

                    DataColumn dc = null;

                    //获取列名
                    for (int j = colIndex; j < colCount; j++)
                    {
                        dc = new DataColumn(firstRow.GetCell(j).StringCellValue);
                        dt.Columns.Add(dc);
                    }

                    //跳过第一行列名
                    rowIndex++;

                    for (int k = rowIndex; k <= rowCount; k++)
                    {
                        DataRow dr = dt.NewRow();
                        row = sheet.GetRow(k);

                        for (int l = colIndex; l < colCount; l++)
                        {
                            if (row.GetCell(l) == null)
                            {
                                continue;
                            }
                            dr[l] = row.GetCell(l).StringCellValue;
                        }

                        dt.Rows.Add(dr);
                    }

                    ds.Tables.Add(dt);
                }

                sheet = null;
                workbook = null;

                fileStream.Close();
                fileStream.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception($"出现异常{ex.Message}");
            }

            return ds;
        }
    }
}
