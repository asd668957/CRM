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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_clientdatabase
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Dictionary<string, string> user = new Dictionary<string, string>();
      
        string a,p;
        public MainWindow()
        {
            //添加使用者
            user.Add("user1", "123");
            user.Add("user2", "456");

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //若帳密輸入正確
            if (user.ContainsKey(a)&&p==user[a]) 
            {
                Window2 w2 = new Window2

                {
                    InputString = a,    //先將初始值賦予window2的InputString
                    InputString2 = p   //          ,,         InputString2                  
                };

                w2.Show();
                this.Close();
            }
            else 
            {
                MessageBox.Show("請確認輸入資料!");
            }
        }

        private void tb_account_TextChanged(object sender, TextChangedEventArgs e)
        {
            a = tb_account.Text;
        }
        
        private void tb_password_TextChanged(object sender, TextChangedEventArgs e)
        {
            p = tb_password1.Text;
        }
    }
}
