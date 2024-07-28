using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            chushihua();
           /* var s = new List<int>() { 1, 2, 3, 4, 5 };
            var w = s.Skip(4).Where(x => {
                Debug.WriteLine(x);
                return true;
            }).ToList();*/
            //tb1.Text = "实\r\n时\r记\n录区";
            denglu d = new denglu();
            d.ShowDialog();
            if (d.tf == false)
            {
                Close();
            }
        }
        List<jubudata> jbguizes = new List<jubudata>();
        List<gezi1> gezi1s = new List<gezi1>();
        List<gezi2> gezi2s = new List<gezi2>();
        int zongfen = 0;
        public int shangxian = 5;
        public int xiaxian = 300;
        void chushihua()
        {
            w1.Children.Clear();
            w2.Children.Clear();
            zongfen = 0;
            gezi1s.Clear();
            for (int i = 0; i < 66; i++)
            {
                gezi1 g = new gezi1();
                g.Zxzhis = "";
                g.bianhao.Content = i + 1;
                gezi1s.Add(g);
                Border border = new Border();
                border.Width = 42.45;
                border.BorderThickness = new Thickness(1);
                border.Child = g;
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.Margin =g.Margin= new Thickness(0);
                if (i > 5)
                {
                    var q = border.Margin;
                    q.Left= -1;
                    border.Margin = q;
                }
                if (i % 6 == 1)
                {
                    var q = border.Margin;
                    q.Top = -1;
                    border.Margin = q;
                }
                if (i % 6 != 0)
                {
                    var q = border.BorderThickness;
                    q.Top = 0; 
                    border.BorderThickness = q;
                }
                if (i % 6 == 3)
                {
                    var q = border.Margin;
                    q.Top = -1;
                    border.Margin = q;
                }
                if (i % 6 == 4)
                {
                    var q = border.Margin;
                    q.Top = -1;
                    border.Margin = q;
                }
                if (i % 6 == 5)
                {
                    var q = border.Margin;
                    q.Top = -1;
                    border.Margin = q;
                }
                w1.Children.Add(border);
            }
            gezi2s.Clear();
            for (int i = 0; i < 66; i++)
            {
                gezi2 g = new gezi2();
                g.Zxzhis = "";
                g.bianhao.Content = i + 1;
                if (i < 4)
                {
                    g.Zxzhis = (i + 1).ToString();
                    g.bo1.Width = 0;
                    g.zxzhi.FontSize = 23;
                    g.zxzhi.FontWeight = FontWeights.Bold;
                    var gm = g.zxzhi.Margin;
                    gm.Top += -2;
                    gm.Left -= -8;
                    g.zxzhi.Margin = gm;
                }
                gezi2s.Add(g);
                Border border = new Border();
                border.Width = 42.45;
                border.Height = 33;
                border.BorderThickness = new Thickness(1);
                border.Child = g;
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.Margin = g.Margin = new Thickness(0);
                if (i > 5)
                {
                    var q = border.Margin;
                    q.Left = -1;
                    border.Margin = q;
                }
                if (true)
                {
                    var q = border.Margin;
                    q.Top = -1;
                    border.Margin = q;
                    var q2 = border.BorderThickness;
                    q2.Top = 0;
                    border.BorderThickness = q2;
                }
                w2.Children.Add(border);
            }
            tishil.Content = "请输入前四手";
            tishil.Foreground = Brushes.Black;
            try
            {
                var s = File.ReadAllText("jubu.data");
                var s1 = Decrypt(s);
                jbguizes = JsonSerializer.Deserialize<List<jubudata>>(s1);
                if (jbguizes.Count == 0)
                {
                    MessageBox.Show("规则不能为空");
                    Close(); return;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                Close();
            }
        }
        public class jubudata
        {
            public string guid { get; set; }
            public string da { get; set; }
        }
        static string encryptKey = "slb1";    //定义密钥 

        #region 加密字符串 
        /// <summary> /// 加密字符串  
        /// </summary> 
        /// <param name="str">要加密的字符串</param> 
        /// <returns>加密后的字符串</returns> 
        public static string Encrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象  

            byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥   

            byte[] data = Encoding.Unicode.GetBytes(str);//定义字节数组，用来存储要加密的字符串 

            MemoryStream MStream = new MemoryStream(); //实例化内存流对象     

            //使用内存流实例化加密流对象  
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);

            CStream.Write(data, 0, data.Length);  //向加密流中写入数据     

            CStream.FlushFinalBlock();              //释放加密流     

            return Convert.ToBase64String(MStream.ToArray());//返回加密后的字符串 
        }
        #endregion

        /// <summary> 
        /// 解密字符串  
        /// </summary> 
        /// <param name="str">要解密的字符串</param> 
        /// <returns>解密后的字符串</returns>   
        public static string Decrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象   

            byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥   

            byte[] data = Convert.FromBase64String(str);//定义字节数组，用来存储要解密的字符串 

            MemoryStream MStream = new MemoryStream(); //实例化内存流对象     

            //使用内存流实例化解密流对象      
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);

            CStream.Write(data, 0, data.Length);      //向解密流中写入数据    

            CStream.FlushFinalBlock();               //释放解密流     

            return Encoding.Unicode.GetString(MStream.ToArray());       //返回解密后的字符串 
        }
        int jilusuoyin = 0;
        int sfsuoyin = 0;
        int cunchusuoyin = -1;
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { 
            HandleButtonClick("庄");
        }
        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            HandleButtonClick("闲");
        }
        private void Label_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            HandleButtonClick("和");
        }
        private void HandleButtonClick(string betType)
        {
            if (jilusuoyin > 65)
            {
                return;
            }

            gezi1s[jilusuoyin].Zxzhis = betType;
            jilusuoyin++;

            if (jilusuoyin >= 4)
            {
                if (jilusuoyin > 4) UpdateScoreAndNotify(betType);
                tishi();
            }

            jisuanfenshu();
        }

        private void UpdateScoreAndNotify(string betType)
        {
            var previousBet = gezi2s[jilusuoyin - 1];
            if(previousBet.Zxzhis == "停") return;
            if (previousBet.Zxzhis == betType)
            {
                // 如果赌注类型与前一次相同，则根据赌注类型更新分数
                if (betType == "庄" || betType == "闲")
                {
                    zongfen += previousBet.Fenshu;
                    previousBet.Zxzhis = $"+{previousBet.Fenshu}";
                    shangyicijifen = previousBet.Fenshu;
                }
                else if (betType == "和")
                {
                    // 如果是“和”，则可能需要特殊的分数处理，例如返回赌注或特定的赔付比例
                    // 这里假设“和”时分数不变，只是标记为“和”
                    previousBet.Zxzhis = "和";
                    // shangyicijifen = 1;
                }
            }
            else
            {
                // 如果赌注类型不同，根据赌注类型减去分数
                if (betType != "和")
                {
                    zongfen -= previousBet.Fenshu;
                    previousBet.Zxzhis = $"-{previousBet.Fenshu}";
                    shangyicijifen = -previousBet.Fenshu;
                }
            }
        }

        private void jisuanfenshu()
        {
            double totalScoreExcludingCurrent = gezi2s.Take(jilusuoyin).Sum(x => x.Fenshu);
            if (totalScoreExcludingCurrent == 0)
            {
                xinxil.Content = $"{0}分，收益率{0}%";
                return;
            }

            double yieldPercentage = (zongfen / totalScoreExcludingCurrent) * 100;
            xinxil.Content = $"{zongfen}分，收益率{yieldPercentage:F0}%";

            if (zongfen >= shangxian || zongfen <= xiaxian * -1)
            {
                string message = zongfen >= shangxian
                    ? $"本局结束，总计赢{zongfen}倍下限"
                    : "本局结束，总计赢0倍下限";

                tishil.Content = message;
                cunchusuoyin = jilusuoyin;
                jilusuoyin = 66;
                tishil.Foreground = Brushes.Red;
            }
        }
        /*private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (jilusuoyin > 65)
            {
                return;
            }
            gezi1s[jilusuoyin].Zxzhis = "庄";
            jilusuoyin++;
            if (jilusuoyin == 4)
            {
                tishi();
            }
            if (jilusuoyin > 4)
            {

                var s = gezi2s[jilusuoyin - 1];
                if (s.Zxzhis == "庄")
                {
                    zongfen += s.Fenshu;
                    s.Zxzhis = $"+{s.Fenshu}";
                    shangyicijifen = s.Fenshu;

                }
                else if (s.Zxzhis == "闲")
                {
                    zongfen += s.Fenshu * -1;
                    s.Zxzhis = $"-{s.Fenshu}";
                    shangyicijifen = s.Fenshu * -1;
                }
                tishi();
            }

            jisuanfenshu();
        }
        int cunchusuoyin = -1;
        private void jisuanfenshu()
        {
            double s = gezi2s.Sum(x => x.Fenshu) - gezi2s[jilusuoyin].Fenshu;
            if (s == 0)
            {
                xinxil.Content = $"{0}分，收益率{0}%";
                return;
            }

            xinxil.Content = $"{zongfen}分，收益率{((double)zongfen / s * 100).ToString("F0")}%";

            if (zongfen >= shangxian || zongfen < xiaxian * -1)
            {
                if (zongfen >= shangxian)
                {
                    tishil.Content = $"本局结束，总计赢{zongfen}倍下限";
                }
                else
                {
                    tishil.Content = "本局结束，总计赢0倍下限";
                }
                cunchusuoyin = jilusuoyin;
                jilusuoyin = 66;
                tishil.Foreground = Brushes.Red;
            }
            Debug.WriteLine(s);
        }

        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (jilusuoyin > 65)
            {
                return;
            }
            gezi1s[jilusuoyin].Zxzhis = "闲";
            jilusuoyin++;
            if (jilusuoyin == 4)
            {
                tishi();
            }
            if (jilusuoyin > 4)
            {
                gezi2 s1 = gezi2s.Where(x =>
                {
                    return int.TryParse(x.Zxzhis, out int c);
                }).Last();
                var s = gezi2s[jilusuoyin - 1];
                if (s.Zxzhis == "闲")
                {
                    zongfen += s.Fenshu;
                    s.Zxzhis = $"+{s.Fenshu}";
                    shangyicijifen = s.Fenshu;
                }
                else if (s.Zxzhis == "庄")
                {
                    zongfen += s.Fenshu * -1;
                    s.Zxzhis = $"-{s.Fenshu}";
                    shangyicijifen = s.Fenshu * -1;
                }
                tishi();
            }
            jisuanfenshu();
        }

        private void Label_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (jilusuoyin > 65)
            {
                return;
            }
            gezi1s[jilusuoyin].Zxzhis = "和";
            jilusuoyin++;
            if (jilusuoyin > 3)
            {
                tishi();
            }
        }
        */
        void tishi()
        {
            var r = gezi1s.Where(x =>
            {
                return x.Zxzhis == "庄" || x.Zxzhis == "闲";
            }).ToList();//[^4..]
            var s = new List<gezi1>();
            if (r.Count < 4)
            {
                goto go1;
            }
            //else
            //{
            //    s = r.Skip(r.Count-4).ToList();
            //}
            var q = "";
            foreach (var x in r)
            {
                if (x.Zxzhis == "庄")
                {
                    q += "z";
                }
                else if (x.Zxzhis == "闲")
                {
                    q += "x";
                }
            }
            var ls=new List<jubudata>();
            foreach (var x in jbguizes)
            {
                if (x.da.Length == q.Length + 1&&x.da.Substring(0,x.da.Length-1)==q)
                {
                    ls.Add(x);
                }
                else if (x.da.Length == 5 && q.Length == 5 && x.da.Substring(0, 4) == q.Substring(1,4))
                {
                    ls.Add(x);
                }
                else if(x.da.Length >= q.Length)
                {
                    //不匹配
                }
                else
                {
                    var len = x.da.Length;
                    var q2=q.Substring(q.Length- len+1).ToString();
                    if (x.da.Substring(0,x.da.Length-1)== q2)
                    {
                        ls.Add(x);
                    }
                }
                
            }
            var t = ls.OrderByDescending(x =>
            {
                return x.da.Length;
            }).FirstOrDefault();
            if (t!= null)
            {
                var tt=ls.OrderByDescending(x =>
                {
                    return x.da.Length;
                }).ToList();
                // Debug.WriteLine(t.da.Length+"dd");
                var w = t.da.Last().ToString();
                if (zongfen >= shangxian || zongfen < xiaxian * -1) return;
                if (w == "z")
                {
                    gezi2s[jilusuoyin].Zxzhis = "庄";
                    gezi2s[jilusuoyin].Yanse = "hong";
                }
                else
                {
                    gezi2s[jilusuoyin].Zxzhis = "闲";
                    gezi2s[jilusuoyin].Yanse = "lan";
                }
            }
        go1:
            if (gezi2s[jilusuoyin].Zxzhis == "")
            {
                gezi2s[jilusuoyin].Zxzhis = "停";
                gezi2s[jilusuoyin].Yanse = "hong";
                tishil.Content = $"第{jilusuoyin + 1}手请暂停下注";
                tishil.Foreground = Brushes.Red;
            }
            dangqiangefenshu();
            if (gezi2s[jilusuoyin].Zxzhis == "庄" || gezi2s[jilusuoyin].Zxzhis == "闲")
            {
                tishil.Foreground = (gezi2s[jilusuoyin].Zxzhis == "庄") ? Brushes.Red : Brushes.Blue;
                tishil.Content = $"第{jilusuoyin+1}手请下注，{gezi2s[jilusuoyin].Zxzhis}:{gezi2s[jilusuoyin].Fenshu}倍下限";
            }

        }
        int dizenggezifenshucishu = 0;
        int shangyicijifen = 1;
        int[] fenshuList = new int[] { 1, 2, 3, 4, 5, 7, 10, 14, 18, 24, 32, 44, 60, 80 };
        void dangqiangefenshu()
        {
            if (gezi2s[jilusuoyin].Zxzhis == "停" || gezi1s[jilusuoyin - 1].Zxzhis == "和") 
            {
                gezi2s[jilusuoyin].Fenshu = gezi2s[jilusuoyin - 1].Fenshu;
                return;
            }
            if ( shangyicijifen > 1 )
            {
                if (zongfen == 3 && jilusuoyin == 6)
                {
                    dizenggezifenshucishu = 0;
                }
                else
                {
                    gezi2s[jilusuoyin].Fenshu = shangyicijifen * 2;
                    return;
                }
            }
            if(dizenggezifenshucishu < 0) dizenggezifenshucishu = 1;
            gezi2s[jilusuoyin].Fenshu = fenshuList[dizenggezifenshucishu];
            dizenggezifenshucishu++;
        }
        /** void dangqiangefenshu()
        {
            // dizenggezifenshucishu++;
            // Debug.WriteLine(gezi1s[jilusuoyin-1].Zxzhis=="和");
            if (gezi1s[jilusuoyin - 1].Zxzhis == "和") return;
            if (shangyicijifen == 0)
            {
                if (gezi2s[jilusuoyin].Zxzhis == "停")
                {
                    dizenggezifenshucishu = 0;
                }
                else
                {
                    dizenggezifenshucishu++;
                }
            }
            else if (shangyicijifen < 0)
            {
                if (gezi2s[jilusuoyin].Zxzhis == "停")
                {
                    dizenggezifenshucishu = 0;
                }
                else if (jilusuoyin == 4)
                {
                    dizenggezifenshucishu = 1;
                }
                else
                {
                    dizenggezifenshucishu++;
                }
                // 创建一个条件和值的映射字典
                Dictionary<int, int> scoreThresholds = new Dictionary<int, int>
                {
                    { -221, 80 },
                    { -161, 60 },
                    { -117, 44 },
                    { -85, 32 },
                    { -61, 24 },
                    { -43, 18 },
                    { -29, 14 },
                    { -19, 10 },
                    { -12, 7 },
                    { -10, 5 }
                };

                // 遍历字典，找到第一个满足条件的值
                foreach (var threshold in scoreThresholds)
                {
                    if (zongfen <= threshold.Key)
                    {
                        dizenggezifenshucishu = threshold.Value;
                        break;
                    }
                }
            }
            else if (shangyicijifen > 0)
            {
                dizenggezifenshucishu = shangyicijifen * 2;
            }
            // if (gezi2s[jilusuoyin].Zxzhis == "停")
            // {
            //     dizenggezifenshucishu = 0;
            // }
            // else if (jilusuoyin == 4)
            // {
            //     dizenggezifenshucishu = 1;
            // }
            
            if (zongfen == 3 && jilusuoyin == 6) dizenggezifenshucishu = 1;
            else if (zongfen == 0 && jilusuoyin == 9) dizenggezifenshucishu = 3;
            else if (zongfen == -1 && jilusuoyin == 6) dizenggezifenshucishu = 2;
            else if (zongfen == 0 && jilusuoyin == 7) dizenggezifenshucishu = 4;

            if (gezi2s[jilusuoyin].Zxzhis == "停")
            {
                dizenggezifenshucishu = 0;
            }
            gezi2s[jilusuoyin].Fenshu = dizenggezifenshucishu;

        } **/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dizenggezifenshucishu = 0;
            shangyicijifen = 1;
            jilusuoyin = 0;
            zongfen = 0;
            chushihua();
            jisuanfenshu();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (jilusuoyin == 0 )
            {
                return;
            }
            if(cunchusuoyin > 0 && jilusuoyin >= 66)
            {
                dizenggezifenshucishu++;
                jilusuoyin =cunchusuoyin;
                //重置记录
                cunchusuoyin = -1;
            }
            if (jilusuoyin < 6)
            {
                var t = jilusuoyin - 1;
                List<string> list = new List<string>();
                for (int i = 0; i < t; i++)
                {
                    list.Add(gezi1s[i].Zxzhis);
                }
                Button_Click(null,null);
                foreach (var item in list)
                {
                    if (item == "庄")
                    {
                        Label_MouseLeftButtonDown(null, null);
                    }
                    if (item == "闲")
                    {
                        Label_MouseLeftButtonDown_1(null, null);
                    }
                    if (item == "和")
                    {
                        Label_MouseLeftButtonDown_2(null, null);
                    }
                }
                return;
            }
            gezi1s[jilusuoyin].Zxzhis = "";
            gezi2s[jilusuoyin].Zxzhis = "";
            gezi2s[jilusuoyin].Fenshu = 0;
            jilusuoyin--;
            gezi1s[jilusuoyin].Zxzhis = "";
            var s = gezi2s[jilusuoyin].Zxzhis;
            var q = 0;
            if (s == "庄" || s == "闲" || s == "停")
            {
                q = 0;
            }
            else
            {
                q = int.Parse(s);
            }
            gezi2s[jilusuoyin].Zxzhis = "";
            zongfen -= q;
            var r = gezi2s.Skip(4).ToList().Where(x =>
            {
                return int.TryParse(x.Zxzhis, out int c);
            }).ToList();
            if (r.Count > 0)
            {
                string zxzhisValue = r.Last().Zxzhis;
                int result;
                bool success = int.TryParse(zxzhisValue, out result);

                if (success)
                {
                    shangyicijifen = result;
                    Debug.WriteLine(shangyicijifen + ",ss");
                    if (dizenggezifenshucishu > 0 && result <= 1) dizenggezifenshucishu -= 2;
                }
                else
                {
                    // 转换失败，可以设置默认值或抛出异常
                    shangyicijifen = 1; // 或者你认为合适的默认值
                                        // 或者抛出异常
                    throw new InvalidOperationException("The value could not be converted to an integer.");
                }
                /*shangyicijifen = r.Last().Zxzhis;
                Debug.WriteLine(shangyicijifen+ ",ss");*/
            }
            else
            {
                shangyicijifen = 1;
            }
            tishi();
            jisuanfenshu();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            peizhi p = new peizhi();
            p.win = this;
            p.t1.Text = shangxian.ToString();
            p.t2.Text = xiaxian.ToString();
            p.Show();
        }
    }
}
