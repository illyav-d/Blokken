﻿//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Reflection;
//using System.Windows;
//using Excel = Microsoft.Office.Interop.Excel;
//using Word = Microsoft.Office.Interop.Word;

//namespace Groepsproject_Blokken
//{
//    internal class ExcelWordStatic
//    {
//        public static void PrintHighScore(Player player, int highScore)
//        {
//            Word.Application wordApp = null;
//            Word.Document wordDoc = null;
//            try
//            {

//                string datum = DateTime.Now.ToString("d");
//                wordApp = new Word.Application();
//                wordDoc = wordApp.Documents.Add(Environment.CurrentDirectory + @"\assets\ShareScore.dotx");
//                foreach (Word.Bookmark bm in wordDoc.Bookmarks)
//                {
//                    switch (bm.Name)
//                    {
//                        case "naam": bm.Range.Text = player.Name; break;
//                        case "score": bm.Range.Text = highScore.ToString(); break;
//                        case "datum": bm.Range.Text = datum; break;

//                    }

//                }

//                wordDoc.SaveAs(Environment.CurrentDirectory + @"..\..\..\Printouts\score" + player.Name + ".docx");
//                wordDoc.Close(true);
//                MessageBox.Show("Uw score is afgeprint!", "Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Er is iets fout gelopen:" + ex.Message);
//            }
//            finally
//            {

//                wordApp.Quit();
//                wordDoc = null;
//                wordApp = null;
//            }
//        }
//        public static void PrintStatusSpeler(Player player)
//        {
//            Word.Application wordApp = null;
//            Word.Document wordDoc = null;
//            try
//            {

//                wordApp = new Word.Application();
//                wordDoc = wordApp.Documents.Add(Environment.CurrentDirectory + @"\assets\ShareStatistics.dotx");
//                foreach (Word.Bookmark bm in wordDoc.Bookmarks)
//                {
//                    switch (bm.Name)
//                    {
//                        case "naam": bm.Range.Text = player.Name; break;
//                        case "gamesSP": bm.Range.Text = player.SPGamesPlayed.ToString(); break;
//                        case "gamesVS": bm.Range.Text = player.VSGamesPlayed.ToString(); break;
//                        case "scoreSP": bm.Range.Text = player.SPHighscore.ToString(); break;
//                        case "scoreVS": bm.Range.Text = player.VSHighscore.ToString(); break;
//                        case "winrateSP": bm.Range.Text = player.CalculateSPWinRate().ToString(); break;
//                        case "winrateVS": bm.Range.Text = player.CalculateVSWinRate().ToString(); break;

//                    }

//                }

//                wordDoc.SaveAs(Environment.CurrentDirectory + @"..\..\..\Printouts\statistics" + player.Name + ".docx");
//                wordDoc.Close(true);
//                MessageBox.Show("Uw statistieken werden afgeprint!", "Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Er is iets fout gelopen:" + ex.Message);
//            }
//            finally
//            {

//                wordApp.Quit();
//                wordDoc = null;
//                wordApp = null;
//            }
//        }
//        public static void PrintExcel(List<Player> lijstPlayers)
//        {
//            Excel.Application xlApp = null;
//            Excel.Workbook xlWorkbook = null;
//            Excel.Worksheet xlWorksheet = null;
//            Excel.Chart chart;
//            Excel.ChartObject chartobj;
//            Excel.Range chartRange = null;
//            try
//            {
//                xlApp = new Excel.Application();
//                xlWorkbook = xlApp.Workbooks.Add();
//                xlWorksheet = xlWorkbook.Sheets.Add();
//                xlWorksheet.Name = "DataSheet";
//                DataSet dataSet = new DataSet();
//                dataSet.Tables.Add(ConvertToDataTable(lijstPlayers));
//                foreach (System.Data.DataTable table in dataSet.Tables)
//                {
//                    //Add a new worksheet to workbook with the Datatable name  
//                    xlWorksheet = xlWorkbook.Sheets.Add();
//                    xlWorksheet.Name = table.TableName;

//                    // add all the columns  
//                    for (int i = 1; i < table.Columns.Count + 1; i++)
//                    {
//                        xlWorksheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
//                    }

//                    // add all the rows  
//                    for (int j = 0; j < table.Rows.Count; j++)
//                    {
//                        for (int k = 0; k < table.Columns.Count; k++)
//                        {
//                            xlWorksheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
//                        }
//                    }
//                }

//                chartobj = xlWorksheet.ChartObjects().Add(250, 30, 400, 250);
//                chart = chartobj.Chart;
//                int maxrij = xlWorksheet.UsedRange.Rows.Count;
//                chartRange = xlWorksheet.get_Range("A2:C" + maxrij.ToString());
//                chart.SetSourceData(chartRange);


//                chart.HasLegend = true;
//                chart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionRight;

//                chart.ChartType = Excel.XlChartType.xlLine;

//                chart.HasTitle = true;
//                chart.ChartTitle.Text = "Highscores";
//                Excel.Series series1 = chart.SeriesCollection(1);
//                Excel.Series series2 = chart.SeriesCollection(2);

//                series1.Name = "Single Player";
//                series2.Name = "Versus";
//                //set titles for Axis values and categories
//                chart.Axes(Excel.XlAxisType.xlCategory).HasTitle = true;
//                chart.Axes(Excel.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Players";
//                chart.Axes(Excel.XlAxisType.xlValue).HasTitle = true;
//                chart.Axes(Excel.XlAxisType.xlValue).AxisTitle.Characters.Text = "Highscores";

//                xlWorkbook.SaveAs(Environment.CurrentDirectory + @"..\..\..\Printouts\highscores.xlsx");
//                xlWorkbook.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Er is iets fout gelopen:" + ex.Message);
//            }
//            finally
//            {
//                xlApp.Quit();
//                chartRange = null;
//                chart = null;
//                chartobj = null;
//                xlWorksheet = null;
//                xlWorkbook = null;
//                xlApp = null;
//            }
//        }

//        public static System.Data.DataTable ConvertToDataTable<T>(IList<T> data)
//        {
//            // creating a data table instance and typed it as our incoming model   
//            // as I make it generic, if you want, you can make it the model typed you want.  
//            System.Data.DataTable dataTable = new System.Data.DataTable(typeof(T).Name);

//            //Get all the properties of that model  
//            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

//            // Loop through all the properties              
//            // Adding Column name to our datatable  
//            foreach (PropertyInfo prop in Props)
//            {
//                //Setting column names as Property names
//                if (prop.Name == "SPHighscore" || prop.Name == "VSHighscore" || prop.Name == "Name")
//                    dataTable.Columns.Add(prop.Name);
//            }
//            // Adding Row and its value to our dataTable  
//            foreach (T item in data)
//            {
//                var values = new object[3];
//                for (int i = 0; i < Props.Length; i++)
//                {
//                    //inserting property values to datatable rows
//                    switch (Props[i].Name)
//                    {
//                        case "Name":
//                            values[0] = Props[i].GetValue(item, null);
//                            break;
//                        case "SPHighscore":
//                            if (Props[i].GetValue(item, null) != null)
//                                values[1] = Props[i].GetValue(item, null);
//                            else values[1] = 0;
//                            break;
//                        case "VSHighscore":
//                            if (Props[i].GetValue(item, null) != null)
//                                values[2] = Props[i].GetValue(item, null);
//                            else values[2] = 0;
//                            break;
//                        default:
//                            break;
//                    }
//                }
//                // Finally add value to datatable    
//                dataTable.Rows.Add(values);
//            }
//            return dataTable;
//        }
//    }
//}
