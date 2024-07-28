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
    /// denglu.xaml 的交互逻辑
    /// </summary>
    public partial class denglu : Window
    {
        public denglu()
        {
            InitializeComponent();
        }
        public bool tf=false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a=t1.Text.Trim();
            try
            {
                var a1 = MainWindow.Decrypt(a);
                if (a1.Contains("zxcvbbao.") && a1.Contains("qwert"))
                {
                    var q = a1.Replace("zxcvbbao.", "").Replace("qwert", "");
                    var q1 = DateTime.Parse(q);
                    var d = (DateTime.Now - q1).Minutes;
                    if (d == 0)
                    {
                        tf = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("错误");
                    }
                }
                else
                {
                    MessageBox.Show("错误");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
