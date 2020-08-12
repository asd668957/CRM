using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
    /// Window3.xaml 的互動邏輯
    /// </summary>
    public partial class Window3 : Window
    {
        public string user2,company2, name2,constr2,s_c,s_1,s_2,s_3,s_4;
        
        Class_SQL _SQL;
        public int i;

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

        public Window3()
        {
            InitializeComponent();
            _SQL = new Class_SQL();
        }

        private void b_new_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void b_edit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            string i1 = (dg_visit.Columns[7].GetCellContent(dg_visit.Items[i]) as TextBlock).Text;
            try
            {
                DateTime dt = DateTime.Now; // 取得現在時間
                String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57               

                string sql = "update 拜訪紀錄 set 內容 ='" + tb_content.Text + "'" + ",編輯紀錄 ='" + user2 + "(" + str + ")'" +
                     "where 序號='" + i1 + "'";

                _SQL.FUN(sql, constr2, "編輯成功!");

                Update();
                
            }
            catch (Exception s) { MessageBox.Show(s.ToString()); }
        }

        private void cb_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //combobox 取值
                var comboBoxItem = cb_2.Items[cb_2.SelectedIndex] as ComboBoxItem;
                if (comboBoxItem != null)
                {
                    s_2 = comboBoxItem.Content.ToString();
                }
            }
            catch { }
        }

        private void cb_3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //combobox 取值
                var comboBoxItem = cb_3.Items[cb_3.SelectedIndex] as ComboBoxItem;
                if (comboBoxItem != null)
                {
                    s_3 = comboBoxItem.Content.ToString();
                }
            }
            catch { }
        }

        private void cb_4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //combobox 取值
                var comboBoxItem = cb_4.Items[cb_4.SelectedIndex] as ComboBoxItem;
                if (comboBoxItem != null)
                {
                    s_4 = comboBoxItem.Content.ToString();
                }
            }
            catch { }
        }

        private void dg_visit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = dg_visit.SelectedIndex;
            try
            {
                tb_content.Text = (dg_visit.Columns[3].GetCellContent(dg_visit.Items[i]) as TextBlock).Text;
            }
            catch { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_company.Text = company2;
            tb_name.Text = name2;
            Update();

            if (dg_visit.Items.Count > 0) { lb_1.Visibility = Visibility.Hidden; }
        }

        private void Update()
        {
            try
            {
                string sql = "SELECT 公司,聯繫人,拜訪種類,內容,時間,建立人員,建立日期,序號,編輯紀錄 FROM 拜訪紀錄 where 公司 LIKE '" +
                    tb_company.Text + "' AND 聯繫人 LIKE '" + tb_name.Text + "'";
                //string sql = "select *from 電話紀錄";

                _SQL.Query(dg_visit, sql, constr2);               
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void cb_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //combobox 取值
                var comboBoxItem = cb_1.Items[cb_1.SelectedIndex] as ComboBoxItem;
                if (comboBoxItem != null)
                {
                    s_1 = comboBoxItem.Content.ToString();
                }
            }
            catch { }
        }

        private void cb_category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //combobox 取值
                var comboBoxItem = cb_category.Items[cb_category.SelectedIndex] as ComboBoxItem;
                if (comboBoxItem != null)
                {
                    s_c = comboBoxItem.Content.ToString();
                }
            }
            catch { }
        }

        public bool Add()
        {          
            DateTime dt = DateTime.Now; // 取得現在時間
            String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57
            Update2();
            int ind = dg_all.Items.Count + 1;

            try
            {
                string c = s_1 + s_2 + "~" + s_3 + s_4;

                string sql = "insert into 拜訪紀錄 (公司,聯繫人,拜訪種類,內容,時間,建立人員,建立日期,序號) " +
                    "values('" + tb_company.Text + "','" + tb_name.Text + "','" + s_c + "','" + tb_content.Text + "','" + c + "','" + user2 + "','"+str+ "','"+ind+"')";

                _SQL.FUN(sql, constr2, "新增成功!");
             
                lb_1.Visibility = Visibility.Hidden;
                Update();
                return i > 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return false; }
        }


        private void Update2()
        {
            try
            {
                string sql = "SELECT 公司,聯繫人,拜訪種類,內容,時間,建立人員,建立日期,序號 FROM 拜訪紀錄";
                //string sql = "select *from 電話紀錄";
                _SQL.Query(dg_all, sql, constr2);               
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
