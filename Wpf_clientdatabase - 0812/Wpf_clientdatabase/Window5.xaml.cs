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
    /// Window5.xaml 的互動邏輯
    /// </summary>
    public partial class Window5 : Window
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

        private void dg_price_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = dg_price.SelectedIndex;
            try
            {
                tb_type.Text = (dg_price.Columns[0].GetCellContent(dg_price.Items[index]) as TextBlock).Text;
                tb_num.Text = (dg_price.Columns[1].GetCellContent(dg_price.Items[index]) as TextBlock).Text;
                tb_price.Text = (dg_price.Columns[2].GetCellContent(dg_price.Items[index]) as TextBlock).Text;
                tb_kn.Text = (dg_price.Columns[3].GetCellContent(dg_price.Items[index]) as TextBlock).Text;
            }
            catch { }
        }

        private void b_edit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        

        private void b_new_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_company.Text = company2;
            tb_name.Text = name2;
            Update();

            if (dg_price.Items.Count > 0) { lb_1.Visibility = Visibility.Hidden; }
        }
     
        public Window5()
        {
            InitializeComponent();
            _SQL = new Class_SQL();
        }


        private void Update()
        {
            string sql = "SELECT 規格,數量,價格,報價單號,聯繫人,公司,建立人員,建立時間,序號,編輯紀錄 FROM 報價紀錄 where 公司 LIKE '" +
                  tb_company.Text + "' AND 聯繫人 LIKE '" + tb_name.Text + "'";
            _SQL.Query(dg_price, sql, constr2);
        }

        private void Update2()
        {
            string sql = "SELECT 規格,數量,價格,報價單號,聯繫人,公司,建立人員,序號 FROM 報價紀錄";
            _SQL.Query(dg_price, sql, constr2);
        }

        private void Edit()
        {
            try
            {
                string i1 = (dg_price.Columns[8].GetCellContent(dg_price.Items[index]) as TextBlock).Text;

                DateTime dt = DateTime.Now; // 取得現在時間
                String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57               

                string sql = "update 報價紀錄 set 規格 ='" + tb_type.Text + "'" + ",數量 ='" + tb_num.Text + "'"
                    + ",編輯紀錄 ='" + user2 + "(" + str + ")'" + ",價格 ='" + tb_price.Text + "'"+ ",報價單號 ='" + tb_kn.Text + "'" +
                    "where 序號='" + i1 + "'";

                _SQL.FUN(sql, constr2, "編輯成功!");

                Update();
            }
            catch (Exception s) { MessageBox.Show(s.ToString()); }
        }

        private bool Add()
        {
            try
            {
                DateTime dt = DateTime.Now; // 取得現在時間
                String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57
                Update2();
                int ind = dg_price.Items.Count + 1;

                string sql = "insert into 報價紀錄 (規格,數量,價格,報價單號,聯繫人,公司,建立人員,建立時間,序號) " +
                    "values('" + tb_type.Text + "','" + tb_num.Text + "','" + tb_price.Text + "','" + tb_kn.Text + "','" + name2
                    + "','" + company2 + "','"  + user2 + "','" + str + "','" + ind + "')";

                _SQL.FUN(sql, constr2, "新增成功!");

                lb_1.Visibility = Visibility.Hidden;
                Update();
                return true;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return false; }
        }

    }
}
