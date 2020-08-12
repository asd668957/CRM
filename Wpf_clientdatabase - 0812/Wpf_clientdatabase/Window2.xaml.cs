using System;
using System.Data.OleDb;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace Wpf_clientdatabase
{
    /// <summary>
    /// Window2.xaml 的互動邏輯
    /// </summary>
    public partial class Window2 : System.Windows.Window
    {
        //當前使用者
        public string a;

        //當前密碼
        public string p;

        public string s_city,s_n,s_area, s_company,s_addr,s_ind,s_name, s_phone, 
            s_phone2, s_phone3, s_email,s_dep,s_job,s_date, s_ip,s_trade,s_index;
        public int index,index2,index3;

        //database location
        public string constr;
        public OleDbConnection oleDb;

        //定義搜尋IP Class      
        Class_scan cs;

        //define sql function Class
        Class_SQL _SQL;

        Thread thread1;
        Thread thread2;

        public Window2()
        {
            InitializeComponent();
            
            //實例化 class_sql
            _SQL = new Class_SQL();
            disable();
        }

        public string InputString
        {
            set
            {
                lb_user.Content = value;
                a = value;
            }
        }
        //賦予label值
        public string InputString2
        {
            set
            {
                p = value;
            }
        }

        private void Search_ip() 
        {         
            cs = new Class_scan();
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Action<string>(search_method2), "搜尋中...");
                Dispatcher.Invoke(new Action<string>(search_method), cs.scan("192.168.1"));
            }                 
        }

        private void search_method(string s)
        {
            lb_search.Content = "";
            tb_ip.Text = s;
        }
        private void search_method2(string s )
        {
            lb_search.Content = s;
        }

        //客戶名稱
        private void tb_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_name = tb_name.Text;
        }
   
        //編輯
        private void b_edit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        //公司
        private void tb_company_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_company = tb_company.Text;
        }

        //電話
        private void tb_phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_phone = tb_phone.Text;
        }

        //選中資料表欄位時
        private void dg_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = dg_1.SelectedIndex;           
            try
            {
                tb_n.Text = (dg_1.Columns[0].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_city.Text = (dg_1.Columns[1].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_area.Text = (dg_1.Columns[2].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_company.Text = (dg_1.Columns[3].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_ind.Text = (dg_1.Columns[4].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_adr.Text = (dg_1.Columns[5].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_phone.Text = (dg_1.Columns[6].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_trade.Text = (dg_1.Columns[7].GetCellContent(dg_1.Items[index]) as TextBlock).Text;

                tb_index.Text = (dg_1.Columns[8].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_name.Text = (dg_1.Columns[9].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_phone2.Text = (dg_1.Columns[10].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_phone3.Text = (dg_1.Columns[11].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_email.Text = (dg_1.Columns[12].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_dep.Text = (dg_1.Columns[13].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
                tb_job.Text = (dg_1.Columns[14].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
            }
            catch { }
        }

        private void b_refresh_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        //分機
        private void tb_phone2_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_phone2 = tb_phone2.Text;
        }

        //連線至區網路徑
        private void b_connect_Click(object sender, RoutedEventArgs e)
        {
            constr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = \\" + s_ip + @"\自動化伺服器\3. 技術部\1. 機器視覺部\98. 表單\C_Database_v2.mdb";
            Update();
        }

        private void b_clear_Click(object sender, RoutedEventArgs e)
        {
            tb_n.Text = "";
            tb_company.Text = "";
            tb_city.Text = "";
            tb_area.Text = "";
            tb_adr.Text = "";
            tb_ind.Text = "";
            tb_phone.Text = "";
            tb_trade.Text = "";

            tb_index.Text = "";
            tb_name.Text = "";           
            tb_phone2.Text = "";
            tb_phone3.Text = "";
            tb_email.Text = "";
            tb_dep.Text = "";
            tb_job.Text = "";          
        }

        //搜尋主機IP
        private void b_search_Click(object sender, RoutedEventArgs e)
        {          
            thread2 = new Thread(new ThreadStart(Search_ip));
            thread2.Start();
        }

        private void tb_area_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_area = tb_area.Text;
        }

        private void tb_email_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_email = tb_email.Text;
        }

        private void tb_dep_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_dep = tb_dep.Text;
        }

        //總查詢
        private void b_connect_Copy_Click(object sender, RoutedEventArgs e)
        {
            Query2();
        }

        private void tb_job_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_job = tb_job.Text;
        }
      

        //人員選擇
        private void dg_3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index3 = dg_3.SelectedIndex;
        }

        //拜訪紀錄
        private void b_visit_Click(object sender, RoutedEventArgs e)
        {
            //ii=所選序號
            try
            {
                string ii = (dg_3.Columns[0].GetCellContent(dg_3.Items[index3]) as TextBlock).Text;
                string ii2 = (dg_3.Columns[1].GetCellContent(dg_3.Items[index3]) as TextBlock).Text;
                Window1 w1 = new Window1
                {                                     
                    constr1 = constr
                };
                w1.Show();
            }
            catch { MessageBox.Show("請先選擇欄位!"); }
        }

        //日期查詢
        private void b_search3_Click(object sender, RoutedEventArgs e)
        {
            Query4();
        }

        private void tb_ip_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_ip = tb_ip.Text;
        }

        //序號變更
        private void tb_n_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_n = tb_n.Text;
        }

        //檢查公司名稱(dg_company)
        private void b_check_Click(object sender, RoutedEventArgs e)
        {          
            //若已存在資料
            if (Query() && dg_company.Items.Count > 0)
            {
                MessageBox.Show("公司名稱重複!");
                tb_n.Text = (dg_company.Columns[0].GetCellContent(dg_company.Items[0]) as TextBlock).Text;
                tb_city.Text = (dg_company.Columns[1].GetCellContent(dg_company.Items[0]) as TextBlock).Text;
                tb_area.Text = (dg_company.Columns[2].GetCellContent(dg_company.Items[0]) as TextBlock).Text;
                tb_ind.Text = (dg_company.Columns[4].GetCellContent(dg_company.Items[0]) as TextBlock).Text;
                tb_adr.Text = (dg_company.Columns[5].GetCellContent(dg_company.Items[0]) as TextBlock).Text;
                tb_phone.Text = (dg_company.Columns[6].GetCellContent(dg_company.Items[0]) as TextBlock).Text;
                tb_trade.Text = (dg_company.Columns[7].GetCellContent(dg_company.Items[0]) as TextBlock).Text;
                //人員序號產生
                tb_index.Text = (dg_company.Items.Count + 1).ToString();
            }
            else
            {
                MessageBox.Show("查無此公司!");
                Query_company();

                //自動生成序號A000C
                int count = dg_company.Items.Count + 1;
                string ccount = "A" + (count.ToString()).PadLeft(4, '0');             
                tb_n.Text = ccount;
                tb_index.Text = "1";               
            }
        }

        private void tb_ind_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_ind = tb_ind.Text;
        }     

        private void tb_index_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_index = tb_index.Text;
        }

        private void b_checkname_Click(object sender, RoutedEventArgs e)
        {
            if (tb_name.Text == "") { }
            if (Query_name() && dg_company.Items.Count > 0) { MessageBox.Show("此人員已存在!"); }
            else { MessageBox.Show("查無此人"); }
        }

        //電發
        private void b_pd_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1
            {
                constr1 = constr,
                name = s_name,
                company = s_company,
                user = a
            };
            w1.Show();
        }

        //拜訪
        private void b_visit1_Click(object sender, RoutedEventArgs e)
        {
            Window3 w3 = new Window3
            {
                company = s_company,
                name = s_name,
                constr = constr,
                user = a
            };
            w3.Show();
        }

        private void b_enable_Click(object sender, RoutedEventArgs e)
        {
            if (b_enable.Content as string == "啟用編輯") 
            {
                b_enable.Foreground = new SolidColorBrush(Color.FromRgb(255,0,0));
                b_enable.Content = "關閉編輯";
                enable();
            }
            else 
            {
                b_enable.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                b_enable.Content = "啟用編輯";
                disable();
            }
        }

        private void tb_trade_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_trade = tb_trade.Text;
        }

        private void tb_city_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_city = tb_city.Text;
        }

        //案件紀錄
        private void b_case_Click(object sender, RoutedEventArgs e)
        {
            Window4 w4 = new Window4
            {
                company = s_company,
                name = s_name,
                constr = constr,
                user = a
            };
            w4.Show();
        }

        private void b_EPexcel_Click(object sender, RoutedEventArgs e)
        {
            _SQL = new Class_SQL();
            thread1 = new Thread(() => ExportToExcel(dg_2));           
            thread1.Start();           
        }
        

        //拜訪紀錄查詢
        private void b_search2_Click_1(object sender, RoutedEventArgs e)
        {
            Query5();
        }

        //電話開發查詢
        private void b_search1_Click(object sender, RoutedEventArgs e)
        {
            Query4();
        }

        //案件紀錄查詢
        private void b_search3_Click_1(object sender, RoutedEventArgs e)
        {
            Query6();
        }

        //報價查詢
        private void b_search8_Click(object sender, RoutedEventArgs e)
        {
            Query7();
        }

        //報價紀錄
        private void b_price_Click(object sender, RoutedEventArgs e)
        {
            Window5 w5 = new Window5
            {
                company = s_company,
                name = s_name,
                constr = constr,
                user = a
            };
            w5.Show();
        }

        //地址變更
        private void tb_adr_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_addr = tb_adr.Text;
        }
       
        //公司資料表選擇
        private void dg_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index2 = dg_2.SelectedIndex;
        }

        //手機
        private void tb_phone3_TextChanged(object sender, TextChangedEventArgs e)
        {
            s_phone3 = tb_phone3.Text;
        }
      
        //新增
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        //顯示資料表內容
        private void Update()
        {
            try
            {
                string sql = "SELECT  代碼,縣市,區域,公司,產業,地址,電話,交易條件,序號,聯繫人" +
                    ",分機,手機,mail,部門,職稱,建立日期,建立人員,編輯紀錄 FROM 客戶資料庫";

                oleDb = new OleDbConnection(constr);
                oleDb.Open();
                OleDbCommand cmd = new OleDbCommand
                {
                    CommandText = sql,
                    Connection = oleDb
                };
                OleDbDataReader rd = cmd.ExecuteReader();               
                dg_1.ItemsSource = rd;
                //label foreground color
                lb_status.Foreground =new SolidColorBrush(Color.FromRgb(0,255,0));
                lb_status.Content = "已連線";
            }
            catch { MessageBox.Show("主機位址錯誤!"); }
        }

        //新增
        public void Add()
        {          
            DateTime dt = DateTime.Now; // 取得現在時間
            String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57

            //sql語句不可含'-'
            string sql = "insert into 客戶資料庫 (代碼,縣市,區域,公司,產業,地址,電話,交易條件,序號,聯繫人" +
                    ",分機,手機,mail,部門,職稱,建立日期,建立人員,編輯紀錄) " +
                    "values('" + s_n + "','" + s_city + "','" + s_area + "','" + s_company + "','" + s_ind + "','" + s_addr + "','" +
                    s_phone + "','" + s_trade + "','" + s_index + "','" + s_name + "','" + s_phone2 + "','" + s_phone3 + "','" + s_email +
                    "','" + s_dep + "','" + s_job + "','" + str + "','" + a + "','" + "" + "')";
            _SQL.FUN(sql,constr,"新增成功!");
            Update();
        }

        //編輯
        public void Edit()
        {
            string i1 = (dg_1.Columns[0].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
            string i2 = (dg_1.Columns[8].GetCellContent(dg_1.Items[index]) as TextBlock).Text;

            DateTime dt = DateTime.Now; // 取得現在時間
            String str = dt.ToShortDateString(); // 轉成字串，例：2012/6/5 下午 04:43:57               

            string sql = "update 客戶資料庫 set 代碼 ='" + s_n + "',縣市='" + s_city + "',區域='" + s_area +
                "',公司='" + s_company + "',產業='" + s_ind + "',地址='" + s_addr + "',電話='" + s_phone +
                "',交易條件='" + s_trade + "',聯繫人='" + s_name + "',分機='" + s_phone2 + "',手機='" + s_phone3 +
                "',mail='" + s_email + "',部門='" + s_dep + "',職稱='" + s_job +
                "',編輯紀錄='" + a + "(" + str + ")'" + "where 代碼='" + i1 + "'" + "and 序號='" + i2 + "'";

            _SQL.FUN(sql, constr, "編輯成功!");
            Update();
        }

        //刪除
        public void Del()
        {         
            string s = (dg_1.Columns[1].GetCellContent(dg_1.Items[index]) as TextBlock).Text;
            string sql = "delete from 客戶資料庫 where 客戶名稱 =" + "'" + s + "'";
            _SQL.FUN(sql, constr, "刪除成功!");          
            Update();           
        }

        //總查詢
        private void Query2()
        {
            string sql = "SELECT  代碼,縣市,區域,公司,產業,地址,電話,交易條件,序號,聯繫人" +
                    ",分機,手機,mail,部門,職稱,建立日期,編輯紀錄 FROM 客戶資料庫 客戶資料庫 " +
                    "where 公司 LIKE'%" + tb_tsearch.Text + "%'" +
                "OR 聯繫人 LIKE'%" + tb_tsearch.Text + "%'";

            _SQL.Query(dg_1, sql, constr);
        }

        //日期查詢
        private void Query4()
        {
            try
            {
                string s1 = dp_from2.SelectedDate.Value.ToShortDateString();//.Replace('/','-');
                string s2 = dp_to2.SelectedDate.Value.ToShortDateString();//.Replace('/', '-');

                string sql = "SELECT 公司,聯繫人,內容,建立人員,建立時間 FROM 電話紀錄 where 公司 LIKE '%"
                        + tb_company2.Text + "%' AND 聯繫人 LIKE '%" + tb_name2.Text + "%' AND 建立人員 LIKE '%"
                        + tb_user.Text + "%'" + "AND 建立時間 >= '" + s1 + " 'AND 建立時間<= '" + s2 + "'" + " order by 建立時間";
                _SQL.Query(dg_2, sql, constr);
              
                lb_total.Content = dg_2.Items.Count;
            }
            catch { MessageBox.Show("請先選擇日期!"); }           
        }

        //拜訪紀錄
        private void Query5()
        {
            try
            {
                string s1 = dp_from3.SelectedDate.Value.ToShortDateString();//.Replace('/', '-');
                string s2 = dp_to3.SelectedDate.Value.ToShortDateString();//.Replace('/', '-');

                //搜尋日期區間
                string sql = "SELECT 公司,聯繫人,拜訪種類,內容,時間,建立人員,建立日期,編輯紀錄 FROM 拜訪紀錄 where 公司 LIKE '%"
                    + tb_company3.Text + "%' AND 聯繫人 LIKE '%" + tb_name3.Text + "%' AND 建立人員 LIKE '%"
                    + tb_user3.Text + "%'" + "AND 建立日期 BETWEEN '" + s1 + " 'AND '" + s2 + "'" + " order by 建立日期";

                _SQL.Query(dg_3, sql, constr);
            }
            catch { MessageBox.Show("請先選擇日期!"); }
        }

        //案件紀錄
        private void Query6()
        {
            try
            {
                string s1 = dp_from.SelectedDate.Value.ToShortDateString();
                string s2 = dp_to.SelectedDate.Value.ToShortDateString();

                //搜尋日期區間
                string sql = "SELECT 公司,聯繫人,專案名稱,客戶需求,目前階段,建立人員,建立時間,編輯紀錄 FROM 案件紀錄 where 公司 LIKE '%"
                    + tb_company4.Text + "%' AND 聯繫人 LIKE '%" + tb_name4.Text + "%' AND 建立人員 LIKE '%"
                    + tb_user4.Text + "%'" + "AND 建立時間 BETWEEN '" + s1 + " 'AND '" + s2 + "'" + " order by 建立時間";

                _SQL.Query(dg_4, sql, constr);
            }
            catch { MessageBox.Show("請先選擇日期!"); }
        }

        private void Query7()
        {
            try
            {
                string s1 = dp_from6.SelectedDate.Value.ToShortDateString();
                string s2 = dp_to6.SelectedDate.Value.ToShortDateString();

                //搜尋日期區間
                string sql = "SELECT 公司,聯繫人,規格,數量,價格,報價單號,建立人員,建立時間,編輯紀錄 FROM 報價紀錄 where 公司 LIKE '%"
                    + tb_company5.Text + "%' AND 聯繫人 LIKE '%" + tb_name5.Text + "%' AND 建立人員 LIKE '%"
                    + tb_user5.Text + "%'" + "AND 建立時間 BETWEEN '" + s1 + " 'AND '" + s2 + "'" + " order by 建立時間";

                _SQL.Query(dg_99, sql, constr);
            }
            catch { MessageBox.Show("請先選擇日期!"); }
        }


        //公司搜尋
        private bool Query()
        {
            //"SELECT DISTINCT 公司 FROM 客戶資料庫 "
            string sql = "SELECT 代碼,縣市,區域,公司,產業,地址,電話,交易條件 FROM 客戶資料庫 where 公司 LIKE '" + tb_company.Text + "'";
            try
            {
                _SQL.Query(dg_company, sql, constr);
                return true;
            }
            catch { return false; }
        }

        private bool Query_company()
        {           
            string sql = "SELECT DISTINCT 公司 FROM 客戶資料庫 ";
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
                dg_company.ItemsSource = rd;
                return true;
            }
            catch { MessageBox.Show("請先與資料庫連線!"); return false; }
        }

        //名稱搜尋
        private bool Query_name()
        {
            string sql = "SELECT 代碼,縣市,區域,公司,產業,地址,電話,交易條件" +
                " FROM 客戶資料庫 where 聯繫人 LIKE '" + tb_name.Text + "'"+"AND 公司 LIKE '"+tb_company.Text+"'";
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
                dg_company.ItemsSource = rd;
                return true;
            }
            catch { MessageBox.Show("請先與資料庫連線!"); return false; }
        }

        private void enable()
        {
            tb_company.IsEnabled = true;
            tb_city.IsEnabled = true;
            tb_area.IsEnabled = true;
            b_check.IsEnabled = true;
            tb_adr.IsEnabled = true;
            tb_ind.IsEnabled = true;
            tb_phone.IsEnabled = true;
            tb_trade.IsEnabled = true;

            tb_name.IsEnabled = true;
            b_checkname.IsEnabled = true;
            tb_phone2.IsEnabled = true;
            tb_phone3.IsEnabled = true;
            tb_email.IsEnabled = true;
            tb_dep.IsEnabled = true;
            tb_job.IsEnabled = true;

            b_new.IsEnabled = true;
            b_edit.IsEnabled = true;
        }

        private void disable() 
        {
            tb_company.IsEnabled = false;
            tb_city.IsEnabled = false;
            tb_area.IsEnabled = false;
            b_check.IsEnabled = false;
            tb_adr.IsEnabled = false;
            tb_ind.IsEnabled = false;
            tb_phone.IsEnabled = false;
            tb_trade.IsEnabled = false;

            tb_name.IsEnabled = false;
            b_checkname.IsEnabled = false;
            tb_phone2.IsEnabled = false;
            tb_phone3.IsEnabled = false;
            tb_email.IsEnabled = false;
            tb_dep.IsEnabled = false;
            tb_job.IsEnabled = false;

            b_new.IsEnabled = false;
            b_edit.IsEnabled = false;
        }

        private void ExportToExcel(DataGrid dg)
        {
            if (!Dispatcher.CheckAccess())
            {

                Dispatcher.Invoke(new System.Action(()=>_SQL.ExportToExcel2(dg)));
                
            }           
        }
    
    }
}
