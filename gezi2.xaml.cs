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

namespace 智能化BP自动分析软件W7
{
    /// <summary>
    /// gezi2.xaml 的交互逻辑
    /// </summary>
    public partial class gezi2 : UserControl
    {
        public gezi2()
        {
            InitializeComponent();
        }
        private int fenshu = 0;
        private string zxzhis = "";
        private string yanse = "";
        private int xuhao = 0;
        public int Fenshu { get => fenshu; set => fenshu = value; }
        public string Zxzhis { get => zxzhis; set {
                zxzhis = value;
                var q = int.TryParse(zxzhis,out int c);
                if (q)
                {
                    zxzhi.Width = 50;
                    zxzhi.FontSize = 18;
                    zxzhi.Margin= new Thickness(0);
                }
                else
                {
                    zxzhi.Margin = new Thickness(6,-5,0,0);
                    zxzhi.Height = 66;
                    zxzhi.FontSize = 26;
                }
                zxzhi.Content = zxzhis;
            } }
        public string Yanse { get => yanse; set{
                if (value == "hong")
                {
                    zxzhi.Foreground = new SolidColorBrush(Colors.Red);
                }
                else {
                    zxzhi.Foreground = new SolidColorBrush(Colors.Blue);
                }
            } }
        public int Xuhao { get => xuhao; set
            { 
                  xuhao = value;
                bianhao.Content = xuhao;
            } }
    }
}
