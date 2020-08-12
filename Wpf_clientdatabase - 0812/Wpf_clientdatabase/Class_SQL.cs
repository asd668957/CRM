using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Threading;

namespace Wpf_clientdatabase
{
    class Class_SQL
    {       
        public OleDbConnection oleDb;

        //add
        public bool FUN(string sql, string constr, string msg)
        {
            oleDb = new OleDbConnection(constr);
            oleDb.Open();
            try
            {
                //往表1新增一條資料               
                OleDbCommand oleDbCommand = new OleDbCommand(sql, oleDb);

                int i = oleDbCommand.ExecuteNonQuery(); //返回被修改的數目
                MessageBox.Show(msg);
                return true;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return false; }
        }    

        public bool Query(DataGrid dg,string sql,string constr )
        {
            try
            {
                OleDbConnection con = new OleDbConnection(constr);
                con.Open();
                OleDbCommand cmd = new OleDbCommand
                {
                    CommandText = sql,
                    Connection = con
                };
                OleDbDataReader rd = cmd.ExecuteReader();
                //cmd.Parameters.Add();
                dg.ItemsSource = rd;
                return true;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString());  return false; }                     
        }

       

        public void ExportToExcel2(DataGrid dg)
        {

            Excel.Application excel = new Excel.Application();
            excel.Visible = true; //www.yazilimkodlama.com
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < dg.Columns.Count; j++) //Başlıklar için
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; //Başlığın Kalın olması için
                sheet1.Columns[j + 1].ColumnWidth = 15; //Sütun genişliği ayarı
                myRange.Value2 = dg.Columns[j].Header;
            }
            for (int i = 0; i < dg.Columns.Count; i++)
            { //www.yazilimkodlama.com
                for (int j = 0; j < dg.Items.Count; j++)
                {
                    TextBlock b = dg.Columns[i].GetCellContent(dg.Items[j]) as TextBlock;
                    Range myRange = (Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
            //欄位寬度自動調整
            sheet1.Columns.AutoFit();
        }



    }
}
