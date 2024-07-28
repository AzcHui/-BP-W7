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

namespace 智能化BP自动分析软件W7
{
    /// <summary>
    /// peizhi.xaml 的交互逻辑
    /// </summary>
    public partial class peizhi : Window
    {
        internal MainWindow win;

        public peizhi()
        {
            InitializeComponent();
            lab4.Content = "手，指当趋势比较危险时给出报\r警提示，并将押注建议取反值";
            lab5.Content = "手，指当趋势非常危险时强制中\r止的手数";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = int.TryParse(t1.Text, out int sx);
            var s2 = int.TryParse(t2.Text, out int xx);
            if (!s || !s2)
            {
                MessageBox.Show("请输入整数");
                return;
            }
            win.shangxian = sx;
            win.xiaxian = xx;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            t1.Text = "5";
            t2.Text = "300";
            t3.Text = "4";
            t4.Text = "10";
            t5.Text = "40";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
