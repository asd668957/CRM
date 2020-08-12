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
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        public string sn;
        public string pn;
        public string constr2,company2,name2,user2;
        public int i;
        public OleDbConnection oleDb;
        Class_SQL _SQL;

        public Window1()
        {          
            InitializeComponent();
            _SQL = new Class_SQL();
        }

        public string constr1
        {
            set { constr2 = value; }
        }

        public string company
        {
            set { company2 = value; }
        }
        public string name
        {
            set { name2 = value; }
            
        }

        public string user
        {
            set { user2 = value; }
        }

        private void Update()
        {
            try
            {
                string sql = "SELECT 公司,聯繫人,內容,建立人員,建立時間,序號,編輯紀錄 FROM 電話紀錄 where 公司 LIKE '"+
                    tb_company.Text+"' AND 聯繫人 LIKE '"+tb_name.Text+"'";
                //string sql = "select *from 電話紀錄";
                _SQL.Query(dg_pd,sql,constr2);

                           
            }
            catch (Exception ex) {MessageBox.Show(ex.ToString()); }      
        }

        private void Update2()
        {
            try
            {
                string sql = "SELECT 公司,聯繫人,內容,建立人員,建立時間,序號 FROM 電話紀錄";

                _SQL.Query(dg_pp, sql, constr2);

                
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        //編輯
        private void b_edit2_Click(object sender, RoutedEventArgs e)
        {
            edit();
        }

        //新增
        private void b_2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Add();
                lb_1.Visibility = Visibility.Hidden;
            }
            catch { }

        }

        private void dg_pd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            i = dg_pd.SelectedIndex;
            try
            {
                tb_company.Text = (dg_pd.Columns[0].GetCellContent(dg_pd.Items[i]) as TextBlock).Text;
                tb_name.Text = (dg_pd.Columns[1].GetCellContent(dg_pd.Items[i]) as TextBlock).Text;
                tb_p.Text = (dg_pd.Columns[2].GetCellContent(dg_pd.Items[i]) as TextBlock).Text;
            }
            catch { }
        }    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_company.Text = company2;
            tb_name.Text = name2;
            Update();
            
            if (dg_pd.Items.Count == 0) { lb_1.Visibility = Visibility.Visible; }
        }

        public bool Add()
        {
            
            DateTime dt = DateTime.Now; // 取得現在時間
            String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57
            Update2();
            int ind = dg_pp.Items.Count + 1;

            try
            {
                string sql = "insert into 電話紀錄 (公司,聯繫人,內容,建立時間,建立人員,序號) " +
                    "values('" + tb_company.Text + "','" + tb_name.Text + "','" + tb_p.Text + "','" + str + "','" + user2 + "','" + ind + "')";

                _SQL.FUN(sql, constr2,"新增成功!");

                
                Update();
                return i > 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return false; }
        }

        private bool edit()
        {
            string i1 = (dg_pd.Columns[5].GetCellContent(dg_pd.Items[i]) as TextBlock).Text;
            try
            {
                
                DateTime dt = DateTime.Now; // 取得現在時間
                String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57               

                string sql = "update 電話紀錄 set 內容 ='" + tb_p.Text + "'"+",編輯紀錄 ='"+user2+"("+str+")'"+
                     "where 序號='" + i1 + "'";
                _SQL.FUN(sql, constr2, "編輯成功!");

               
                Update();
                return false;              
            }
            catch (Exception s) { MessageBox.Show(s.ToString()); return true; }
        }
    }
}
