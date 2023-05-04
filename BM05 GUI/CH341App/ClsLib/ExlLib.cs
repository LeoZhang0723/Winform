using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xl = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.Runtime.InteropServices; // 搜索Excel应用实例

namespace ClsLib
{
    public class ExlLib //Excel Operation Class
    {
        public enum WindowState { Maximized, Minimized, Normal, }

        private xl._Application objApp;
        private xl._Workbook objWorkbook;
        private xl._Worksheet objWorksheet;
        private xl._Chart objChart;
        public bool GetApplication()
        {
            try
            {
                objApp = null;
                objApp = (xl._Application)Marshal.GetActiveObject("Excel.Application");// 搜索Excel应用实例
                if (objApp == null) { return false; }

                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show("没有现成的Excel应用被打开。");
                return false;
            }
        }
        public bool CreatApplication(bool iIsVisible = true)
        {
            try
            {
                objApp = null;
                objApp = new xl.Application();
                if (objApp == null) { return false; }

                objApp.Visible = iIsVisible;
                objApp.DisplayAlerts = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开Excel应用出错，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool GetWorkbooksCount(ref int iCount)
        {
            try
            {
                iCount = objApp.Workbooks.Count;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel获取工作表计数错误，错误描述：" + ex.Message);
                return false;
            }
        }
        public bool OpenWorkbook(String iFilePath)
        {
            try
            {
                if (objApp == null) { return false; }
                objWorkbook = objApp.Workbooks.Open(iFilePath);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开Excel文件错误,请检查后重试。没有找到相关文件[" + iFilePath + "]。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool AddWorkbook()
        {
            try
            {
                objWorkbook = objApp.Workbooks.Add();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("产生新的Excel文件错误,请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool CheckWorkbook(String iFileName) // 注意：一个应用下不能打开相同名字的Excel文件
        {
            try
            {
                if (objApp.Workbooks.Count <= 0) { return false; }
                foreach (xl.Workbook wb in objApp.Workbooks)
                {
                    if (wb.Name == iFileName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("检查工作薄发生错误，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool AddWorksheet()
        {
            try
            {
                objWorksheet = (xl.Worksheet)objWorkbook.Sheets.Add();
                objWorksheet.Activate();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加图表页错误，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool ActiveWorksheet(string iWorksheetName)
        {
            try
            {
                objWorksheet = (xl.Worksheet)objWorkbook.Worksheets[iWorksheetName];
                // objWorksheet.Select();
                objWorksheet.Activate();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("激活工作页错误,请检查后重试。无法找到相关工作页[" + iWorksheetName + "]。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool CheckWorksheet(string iWorksheetName)
        {
            try
            {
                if (objWorkbook.Sheets.Count <= 0) { return false; }
                foreach (xl.Worksheet ws in objWorkbook.Worksheets)
                {
                    if (ws.Name == iWorksheetName) { return true; }
                }
                return false;
            }
            catch (Exception ex) //不需要中断程序
            {
                MessageBox.Show("检查数据表错误。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool RenameActiveWorksheet(string iWorksheetName)
        {
            try
            {
                objWorksheet.Name = iWorksheetName;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("重命名Sheet错误，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool RenameWorksheet(string iOldWorksheetName, string iNewWorksheetName)
        {
            try
            {
                if (objWorkbook.Charts.Count <= 0) { return false; }
                foreach (xl.Worksheet ws in objWorkbook.Worksheets)
                {
                    if (ws.Name == iOldWorksheetName)
                    {
                        ws.Name = iNewWorksheetName;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("重命名Sheet错误，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool AddChart()
        {
            try
            {
                objChart = (xl.Chart)objWorkbook.Charts.Add();
                objChart.Activate();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加图表页错误，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool CheckChart(string iChartName)
        {
            try
            {
                if (objWorkbook.Charts.Count <= 0) { return false; }
                foreach (xl.Chart cht in objWorkbook.Charts)
                {
                    if (cht.Name == iChartName) { return true; }
                }
                return false;
            }
            catch (Exception ex) //不需要中断程序
            {
                MessageBox.Show("检查数据表错误。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool ActiveChart(string iChartName)
        {
            try
            {
                objChart = (xl.Chart)objWorkbook.Charts[iChartName];
                // objChart.Select();
                objChart.Activate();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("激活工作页错误,请检查后重试。无法找到相关工作页[" + iChartName + "]。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool ActiveChart(int iIndex)
        {
            try
            {
                objChart = (xl.Chart)objWorkbook.Charts[iIndex + 1];
                // objChart.Select();
                objChart.Activate();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("激活工作页错误,请检查后重试。无法找到相关工作页[" + iIndex.ToString() + "]。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool RenameActiveChart(string iChartName)
        {
            try
            {
                if (iChartName == "")
                {
                    MessageBox.Show("名字不能为空");
                    return false;
                }
                objChart.Name = iChartName;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("重命名Chart错误，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool RenameChart(string iOldChartName, string iNewChartName)
        {
            try
            {
                if (iOldChartName == "" || iNewChartName == "")
                {
                    MessageBox.Show("名字不能为空");
                    return false;
                }
                if (objWorkbook.Charts.Count <= 0) { return false; }
                foreach (xl.Chart cht in objWorkbook.Charts)
                {
                    if (cht.Name == iOldChartName)
                    {
                        cht.Name = iNewChartName;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("重命名Chart错误，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool SeriesCollectionName(int iSeries, string iName, string strXVal, string strVal)
        {
            try
            {
                objChart.SeriesCollection(iSeries).NewSeries();
                objChart.SeriesCollection(iSeries).select();
                objChart.SeriesCollection(iSeries).Name = iName;
                objChart.SeriesCollection(iSeries).Border.ColorIndex = iSeries + 2;
                objChart.SeriesCollection(iSeries).XValues = strXVal[iSeries];
                objChart.SeriesCollection(iSeries).Values = strVal[iSeries];
                objChart.SeriesCollection(iSeries).MarkerStyle = 2;
                objChart.SeriesCollection(iSeries).MarkerSize = 5;
                objChart.SeriesCollection(iSeries).MarkerforegroundColorIndex = iSeries + 2;
                objChart.SeriesCollection(iSeries).MarkerBackgroundColorIndex = iSeries + 2;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSeriesCollection(int iSerColl)
        {
            try
            {
                objChart.SeriesCollection(1).Delete(iSerColl);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChartType(xl.XlChartType iChtTyp)
        {
            try
            {
                objChart.ChartType = iChtTyp;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool MoveChart()
        {
            try
            {
                objWorkbook.ActiveChart.Move(objWorksheet);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool WriteCellText(int iRow, int iCol, string iText)
        {
            try
            {
                if (iRow < 1 || iCol < 1)
                {
                    MessageBox.Show("单元格位置不能小于1");
                    return false;
                }

                objWorksheet.Cells[iRow, iCol].Value2 = iText;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("写单元格值错误。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool ReadCellText(int iRow, int iCol, ref string oText)
        {
            try
            {
                if (iRow < 1 || iCol < 1)
                {
                    MessageBox.Show("单元格位置不能小于1");
                    return false;
                }
                oText = objWorksheet.Cells[iRow, iCol].Text;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读单元格值错误。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool WriteText1D(int iRow, int iCol, bool iDir, string[] iArrText) // Direction = Horizontal/Vertical 
        {
            try
            {
                if (iRow < 1 || iCol < 1)
                {
                    MessageBox.Show("单元格位置不能小于1");
                    return false;
                }
                object[,] obj = new object[iDir ? 1 : iArrText.Length, iDir ? iArrText.Length : 1];
                for (int i = 0; i < iArrText.Length; i++)
                {
                    obj[iDir ? 0 : i, iDir ? i : 0] = iArrText[i];
                }
                xl.Range rng = objWorksheet.Range[objWorksheet.Cells[iRow, iCol], objWorksheet.Cells[iRow + (iDir ? 0 : iArrText.Length - 1), iCol + (iDir ? iArrText.Length - 1 : 0)]];
                rng.Value2 = obj;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("写单元格值错误。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool ReadText1D(int iRow, int iCol, int iLength, bool iDir, ref string[] oArrText) // Direction = Horizontal/Vertical 
        {
            try
            {
                if (iRow < 1 || iCol < 1)
                {
                    MessageBox.Show("单元格位置不能小于1");
                    return false;
                }
                xl.Range rng = objWorksheet.Range[objWorksheet.Cells[iRow, iCol], objWorksheet.Cells[iRow + (iDir ? 0 : iLength - 1), iCol + (iDir ? iLength - 1 : 0)]];
                object[,] obj = rng.Value2;
                for (int i = 0; i < obj.GetLength(iDir ? 1 : 0); i++)
                {
                    if (obj[i + 1, 1] != null)
                    {
                        oArrText[i] = obj[iDir ? 1 : i + 1, iDir ? i + 1 : 1].ToString();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("写单元格值错误。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool MergeCell(int iStartRow, int iStartCol, int iEndRow, int iEndCol)
        {
            try
            {
                objWorksheet.Range[objWorksheet.Cells[iStartRow, iStartCol], objWorksheet.Cells[iEndRow, iEndCol]].Merge();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("整理图片单元格错误。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool ShapesAddPicture(int iRow, int iCol, string iPlotPath)
        {  // 相效于Picture.Insert，Shapes.AddPicture有更多的参数，可以将插入的图片进行缩放及指定排列位置
            try
            {
                xl.Range objRange = objWorksheet.Cells[iRow, iCol];
                objWorksheet.Shapes.AddPicture(
                      Filename: iPlotPath,
                      LinkToFile: MsoTriState.msoFalse,
                      SaveWithDocument: MsoTriState.msoTrue,
                      Left: objRange.Left + 2, // 左边留空
                      Top: objRange.Top + 2, // 上边留空
                      Width: objRange.Width - 4, // 右边留空
                      Height: objRange.Height - 4); // 下边留空
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("插入图片错误，按确认后继续。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool SetColumnWidth(int iCol, double iWidth)
        {
            try
            {
                objWorksheet.Columns[iCol].ColumnWidth = iWidth;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置ColumnWidth出错。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool SetRowHeight(int iRow, double iHeight)
        {
            try
            {
                objWorksheet.Rows[iRow].RowHeight = iHeight;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置RowHeight出错。错误描述：" + ex.Message);
                return false;
            }


        }

        public bool SetWrapText(int iRow, int iCol)
        {
            try
            {
                objWorksheet.Cells[iRow, iCol].WrapText = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置WrapText出错。错误描述：" + ex.Message);
                return false;
            }
        }

        public void ClearRange(int iStartRow, int iStartCol, int iEndRow, int iEndCol)
        {
            objWorksheet.Range[objWorksheet.Cells[iStartRow, iStartCol], objWorksheet.Cells[iEndRow, iEndCol]].Clear();
        }

        public bool AddSideBorder(int iStartRow, int iStartCol, int iEndRow, int iEndCol)
        {
            try
            {
                xl.Range objRange = objWorksheet.Range[objWorksheet.Cells[iStartRow, iStartCol], objWorksheet.Cells[iEndRow, iEndCol]];
                objRange.Select();
                objRange.Borders[xl.XlBordersIndex.xlEdgeLeft].Weight = xl.XlBorderWeight.xlThin;
                objRange.Borders[xl.XlBordersIndex.xlEdgeTop].Weight = xl.XlBorderWeight.xlThin;
                objRange.Borders[xl.XlBordersIndex.xlEdgeBottom].Weight = xl.XlBorderWeight.xlThin;
                objRange.Borders[xl.XlBordersIndex.xlEdgeRight].Weight = xl.XlBorderWeight.xlThin;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool FindColumnNameNumber(int iRow, string iColHdr, ref int oColNum)
        {
            int colMax;
            try
            {
                colMax = objWorksheet.UsedRange.Columns.Count;
                for (int i = 0; i < colMax; i++)
                {
                    string str = objWorksheet.Cells[iRow, i + 1].Text;
                    if (str == iColHdr)
                    {
                        oColNum = i + 1;
                        return true;
                    }
                }
                MessageBox.Show("在（" + objWorksheet.Name + "）数据表中，列名(" + iColHdr + ")没有找到，请检查后重试");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据表出错。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool Axes(string textX, string textY, XlTrendlineType TrendlineType, float maxX, float minX, float maxY, float minY)
        {
            try
            {
                if (objChart == null) { return false; }
                objChart.PlotArea.Select();
                objChart.Axes(XlAxisType.xlCategory).ScaleType = TrendlineType;
                objChart.Axes(XlAxisType.xlCategory).MaximumScale = maxX;
                objChart.Axes(XlAxisType.xlCategory).MinimumScale = minX;
                objChart.Axes(XlAxisType.xlCategory, xl.XlAxisGroup.xlPrimary).select();
                objChart.Axes(XlAxisType.xlCategory, xl.XlAxisGroup.xlPrimary).HasTitle = true;
                objChart.Axes(XlAxisType.xlCategory, xl.XlAxisGroup.xlPrimary).AxisTitle.Characters.text = textX;

                objChart.Axes(XlAxisType.xlCategory).Select();
                objChart.Axes(XlAxisType.xlCategory).CrossesAt = minX;
                objChart.Axes(XlAxisType.xlCategory).Format.Line.Weight = 2;
                objChart.Axes(XlAxisType.xlCategory).AxisTitle.Format.TextFrame2.TextRange.Font.Size = 14;
                objChart.Axes(XlAxisType.xlCategory).HasMajorGridlines = true;
                //objChart.Axes(XlAxisType.xlCategory).HasMinorGridlines = True;
                objChart.Axes(XlAxisType.xlCategory).TickLabels.NumberFormat = "0.0000";
                //objChart.Axes(XlAxisType.xlCategory).MajorGridlines.Border.ColorIndex = 57;

                objChart.Axes(XlAxisType.xlValue).Select();
                objChart.Axes(XlAxisType.xlValue).MaximumScale = 100;//maxY
                objChart.Axes(XlAxisType.xlValue).MinimumScale = minY;
                objChart.Axes(XlAxisType.xlValue, xl.XlAxisGroup.xlPrimary).HasTitle = true;
                objChart.Axes(XlAxisType.xlValue, xl.XlAxisGroup.xlPrimary).AxisTitle.Characters.text = textY;
                objChart.Axes(XlAxisType.xlValue).Format.Line.Weight = 2;
                objChart.Axes(XlAxisType.xlValue).AxisTitle.Format.TextFrame2.TextRange.Font.Size = 14;
                objChart.Axes(XlAxisType.xlValue).HasMajorGridlines = true;
                //objChart.Axes(XlAxisType.xlValue).HasMinorGridlines = True;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PageSetup(xl.XlPageOrientation iPageOrientation, xl.XlPaperSize iPaperSize)
        {
            try
            {
                if (objChart == null) { return false; }
                objChart.PlotArea.Select();
                objChart.PageSetup.Orientation = iPageOrientation;
                objChart.PageSetup.PaperSize = iPaperSize;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PlotAreaFormatLine(MsoTriState iState, int iRGB)
        {
            try
            {
                if (objChart == null) { return false; }
                objChart.PlotArea.Select();
                objChart.PlotArea.Format.Line.Visible = iState;
                objChart.PlotArea.Format.Line.ForeColor.RGB = iRGB;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChartTitle(string iName, float iLeft, float iTop)
        {
            try
            {
                if (objChart == null) { return false; }
                objChart.HasTitle = true;
                objChart.ChartTitle.Select();
                objChart.ChartTitle.Characters.Text = iName;
                objChart.ChartTitle.Left = iLeft;
                objChart.ChartTitle.Top = iTop;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChartArea(float iLeft, float iTop, float iWidth, float iHeight)
        {
            try
            {
                if (objChart == null) { return false; }
                objChart.ChartArea.Select();
                objChart.ChartArea.Left = iLeft;
                objChart.ChartArea.Top = iTop;
                objChart.ChartArea.Width = iWidth;
                objChart.ChartArea.Height = iHeight;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PlotArea(float iLeft, float iTop, float iWidth, float iHeight)
        {
            try
            {
                if (objChart == null) { return false; }
                objChart.PlotArea.Select();
                objChart.PlotArea.Left = iLeft;
                objChart.PlotArea.Top = iTop;
                objChart.PlotArea.Width = iWidth;
                objChart.PlotArea.Height = iHeight;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Legend(float iLeft, float iTop, float iWidth, float iHeight)
        {
            try
            {
                if (objChart == null) { return false; }
                objChart.Legend.Select();
                objChart.Legend.Left = iLeft;
                objChart.Legend.Top = iTop;
                objChart.Legend.Width = iWidth;
                objChart.Legend.Height = iHeight;

                objChart.Legend.Format.TextFrame2.TextRange.Font.Size = 14; //
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LegendPosition(xl.XlLegendPosition iLgdPos)
        {
            try
            {
                if (objChart == null) { return false; }
                objChart.Legend.Select();
                objChart.Legend.Position = iLgdPos;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CopyChart()
        {
            try
            {
                if (objChart == null) { return false; }
                objChart.ChartArea.Copy();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("拷贝图表页错误，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool GetChartsCount(out int oCount)
        {
            oCount = 0;
            try
            {
                if (objWorkbook == null) { return false; }
                oCount = objWorkbook.Charts.Count;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取Charts计数错误，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool AddHyperLinks(int iRow, int iCol, string iSubAddr)
        {
            try
            {
                objWorksheet.Hyperlinks.Add
                    (
                    Anchor: objWorksheet.Cells[iRow, iCol],
                    Address: "",
                    SubAddress: iSubAddr
                    );
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool HorizontalAlignment2Center(int iRow, int iCol)
        {
            try
            {
                objWorksheet.Cells[iRow, iCol].HorizontalAlignment = xl.Constants.xlCenter;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SetFontBold(int iRow, int iCol)
        {
            try
            {
                objWorksheet.Cells[iRow, iCol].Font.Bold = true; ;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SetNumberFormatLocal(int iCol, string iStrFmt)
        {
            try
            {
                objWorksheet.Columns[iCol].NumberFormatLocal = iStrFmt;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SelectRange(int iStartRow, int iStartCol, int iEndRow, int iEndCol)
        {
            try
            {
                if (iEndRow <= 0 || iEndCol <= 0)
                {
                    objWorksheet.Cells.Select();
                }
                else
                {
                    objWorksheet.Range[objWorksheet.Cells[iStartRow, iStartCol], objWorksheet.Cells[iEndRow, iEndCol]].Select();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool FormatFontColor2PlotSheet()
        {
            try
            {
                xl.Range objRange = objWorksheet.Cells;
                objRange.Select();
                objRange.Font.Name = "Arial";
                objRange.Font.Size = 9;
                objRange.Interior.ThemeColor = xl.XlThemeColor.xlThemeColorDark1;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("格式化字体或添加边框错误。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool FormatFontRoundBorder()
        {
            try
            {
                xl.Range objRange = objWorksheet.Cells;
                objRange.Font.Name = "Arial";
                objRange.Font.Size = 9;
                objRange.EntireColumn.AutoFit();

                objWorksheet.Range[objWorksheet.Cells[1, 1], objWorksheet.Cells[objWorksheet.UsedRange.Rows.Count, objWorksheet.UsedRange.Columns.Count]].Select(); ;
                objApp.Selection.Borders(xl.XlBordersIndex.xlEdgeLeft).Weight = xl.XlBorderWeight.xlMedium;
                objApp.Selection.Borders(xl.XlBordersIndex.xlEdgeTop).Weight = xl.XlBorderWeight.xlMedium;
                objApp.Selection.Borders(xl.XlBordersIndex.xlEdgeBottom).Weight = xl.XlBorderWeight.xlMedium;
                objApp.Selection.Borders(xl.XlBordersIndex.xlEdgeRight).Weight = xl.XlBorderWeight.xlMedium;
                objApp.Selection.Borders(xl.XlBordersIndex.xlInsideVertical).Weight = xl.XlBorderWeight.xlThin;
                objApp.Selection.Borders(xl.XlBordersIndex.xlInsideHorizontal).Weight = xl.XlBorderWeight.xlThin;

                objWorksheet.Range[objWorksheet.Cells[1, 1], objWorksheet.Cells[1, objWorksheet.UsedRange.Columns.Count]].Select(); ;
                objApp.Selection.Borders(xl.XlBordersIndex.xlEdgeBottom).Weight = xl.XlBorderWeight.xlMedium;

                objWorksheet.Cells[1, 1].Select();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("格式化字体或添加边框错误。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool GetUsedRangeRowsCount(ref int oMaxRow)
        {
            try
            {
                oMaxRow = objWorksheet.UsedRange.Rows.Count;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取最大行数错误。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool GetUsedRangeColumnsCount(ref int oMaxCol)
        {
            try
            {
                oMaxCol = objWorksheet.UsedRange.Columns.Count;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取最大行数错误。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool ApplicationScreenUpdating(bool iState)
        {
            try
            {
                objWorkbook.Application.ScreenUpdating = iState;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置屏幕更新错误。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool SaveExcel(string iFilePath)
        {
            try
            {
                if (iFilePath == null || iFilePath == "")
                {
                    objWorkbook.Save();
                }
                else
                {
                    objWorkbook.SaveAs(iFilePath);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("没有Excel文件被保存，可能数据文件被提前关闭或没有文件被打开。错误描述：" + ex.Message);
                return false;
            }
        }
        public bool CloseExcel()
        {
            try
            {
                objChart = null;
                objWorksheet = null;
                if (objWorkbook != null)
                {
                    objWorkbook.Close();
                    objWorkbook = null;
                }
                if (objApp != null)
                {
                    objApp.Quit();
                    objApp = null; //DisposeExcel       
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel应用或Excel数据文件关闭失败。错误描述：" + ex.Message);
                return false;
            }
        }

        public bool IsClosed()
        {
            if (objWorksheet != null) { return false; }
            else if (objChart != null) { return false; }
            else if (objWorkbook != null) { return false; }
            else if (objApp != null) { return false; }
            else { return true; };
        }

        public bool SetWindowState(WindowState iStat)
        {
            try
            {
                switch (iStat)
                {
                    case WindowState.Maximized:
                        objApp.WindowState = xl.XlWindowState.xlMaximized;
                        break;
                    case WindowState.Minimized:
                        objApp.WindowState = xl.XlWindowState.xlMinimized;
                        break;
                    case WindowState.Normal:
                        objApp.WindowState = xl.XlWindowState.xlNormal;
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置窗口状态出错，请检查后重试。错误描述：" + ex.Message);
                return false;
            }
        }
    }
}
