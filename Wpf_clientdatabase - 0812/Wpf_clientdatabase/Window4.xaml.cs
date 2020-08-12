using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf_clientdatabase
{
    /// <summary>
    /// Window4.xaml 的互動邏輯
    /// </summary>
    public partial class Window4 : Window
    {
        string company2, name2, constr2, user2;
        int index;
        Class_SQL _SQL;
        public string company
        {
            set { company2 = value; }
        }

        public string name
        {
            set { name2 = value; }
        }
        public string constr
        {
            set { constr2 = value; }
        }

        public string user
        {
            set { user2 = value; }
        }

        public Window4()
        {
            InitializeComponent();
            _SQL = new Class_SQL();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_company.Text = company2;
            tb_name.Text = name2;
            Update();

            if (dg_case.Items.Count > 0) { lb_1.Visibility = Visibility.Hidden; }
        }

        private void dg_case_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = dg_case.SelectedIndex;
            try
            {
                tb_project.Text = (dg_case.Columns[2].GetCellContent(dg_case.Items[index]) as TextBlock).Text;
                tb_stage.Text = (dg_case.Columns[4].GetCellContent(dg_case.Items[index]) as TextBlock).Text;
                tb_need.Text = (dg_case.Columns[3].GetCellContent(dg_case.Items[index]) as TextBlock).Text;
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        private void Update()
        {           
            string sql = "SELECT 公司,聯繫人,專案名稱,客戶需求,目前階段,建立人員,建立時間,序號,編輯紀錄 FROM 案件紀錄 where 公司 LIKE '" +
                   tb_company.Text + "' AND 聯繫人 LIKE '" + tb_name.Text + "'"; 
            _SQL.Query(dg_case, sql, constr2);
        }

        private void Update2()
        {
            string sql = "SELECT 公司,聯繫人,專案名稱,客戶需求,目前階段,建立人員 FROM 案件紀錄";
            _SQL.Query(dg_case, sql, constr2);
        }

        public bool Add()
        {
            try
            {
                DateTime dt = DateTime.Now; // 取得現在時間
                String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57
                Update2();
                int ind = dg_case.Items.Count + 1;

                string sql = "insert into 案件紀錄 (公司,聯繫人,專案名稱,客戶需求,目前階段,建立人員,建立時間,序號) " +
                    "values('" + tb_company.Text + "','" + tb_name.Text + "','" + tb_project.Text + "','" + tb_need.Text + "','" + tb_stage.Text
                    + "','" + user2 + "','" + str + "','" + ind + "')";

                _SQL.FUN(sql, constr2, "新增成功!");

                lb_1.Visibility = Visibility.Hidden;
                Update();
                return true;
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); return false; }          
        }

        private void Edit()
        {
            try
            {
                string i1 = (dg_case.Columns[7].GetCellContent(dg_case.Items[index]) as TextBlock).Text;
           
                DateTime dt = DateTime.Now; // 取得現在時間
                String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57               

                string sql = "update 案件紀錄 set 專案名稱 ='" + tb_project.Text + "'" + ",目前階段 ='" + tb_stage.Text + "'"
                    + ",編輯紀錄 ='" + user2 + "(" + str + ")'" +",客戶需求 ='" + tb_need.Text + "'"+          
                     "where 序號='" + i1 + "'";

                _SQL.FUN(sql, constr2, "編輯成功!");

                Update();
            }
            catch (Exception s) { MessageBox.Show(s.ToString()); }
        }


    }
}
