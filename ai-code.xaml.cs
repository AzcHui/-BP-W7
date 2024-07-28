private void Tishi()  
{  
    var r = gezi1s.Where(x => x.Zxzhis == "庄" || x.Zxzhis == "闲").ToList();  
    var q = new StringBuilder();  
    foreach (var x in r)  
    {  
        q.Append(x.Zxzhis == "庄" ? 'z' : 'x');  
    }  
  
    var ls = jbguizes.Where(x =>  
        x.da.Length == q.Length + 1 && x.da.EndsWith(q, StringComparison.Ordinal) ||  
        (x.da.Length == q.Length && (x.da.EndsWith("z" + q, StringComparison.Ordinal) || x.da.EndsWith("x" + q, StringComparison.Ordinal)))).ToList();  
  
    if (ls.Count > 0)  
    {  
        var t = ls.OrderByDescending(x => x.da.Length).First();  
        char lastChar = t.da[^1]; // 使用C# 8.0的索引器语法获取最后一个字符  
        if (lastChar == 'z')  
        {  
            gezi2s[jilusuoyin].Zxzhis = "庄";  
            gezi2s[jilusuoyin].Yanse = "hong";  
        }  
        else if (lastChar == 'x')  
        {  
            gezi2s[jilusuoyin].Zxzhis = "闲";  
            gezi2s[jilusuoyin].Yanse = "lan";  
        }  
  
        if (zongfen >= shangxian || zongfen < xiaxian * -1) return;  
    }  
  
    if (string.IsNullOrEmpty(gezi2s[jilusuoyin].Zxzhis))  
    {  
        gezi2s[jilusuoyin].Zxzhis = "停";  
        gezi2s[jilusuoyin].Yanse = "hong";  
        tishil.Content = $"第{jilusuoyin + 1}手请暂停下注";  
        tishil.Foreground = Brushes.Red;  
    }  
  
    dangqiangefenshu();  
  
    if (!string.IsNullOrEmpty(gezi2s[jilusuoyin].Zxzhis) && (gezi2s[jilusuoyin].Zxzhis == "庄" || gezi2s[jilusuoyin].Zxzhis == "闲"))  
    {  
        tishil.Foreground = (gezi2s[jilusuoyin].Zxzhis == "庄") ? Brushes.Red : Brushes.Blue;  
        tishil.Content = $"第{jilusuoyin + 1}手请下注，{gezi2s[jilusuoyin].Zxzhis}:{gezi2s[jilusuoyin].Fenshu}倍下限";  
    }  
}

    // ... 其他成员变量保持不变 ...  
  
    void InitializeUIElements()  
    {  
        // 清空之前的子元素  
        w1.Children.Clear();  
        w2.Children.Clear();  
  
        // 初始化gezi1列表和对应的UI元素  
        for (int i = 0; i < 66; i++)  
        {  
            var g = new gezi1 { Zxzhis = "", bianhao = { Content = i + 1 } };  
            gezi1s.Add(g);  
  
            var border = CreateBorder(g, 42.45, 0);  
            AdjustBorderMargins(border, i);  
            w1.Children.Add(border);  
        }  
  
        // 初始化gezi2列表和对应的UI元素  
        for (int i = 0; i < 66; i++)  
        {  
            var g = new gezi2  
            {  
                Zxzhis = i < 4 ? (i + 1).ToString() : "",  
                bianhao = { Content = i + 1 }  
            };  
  
            if (i < 4)  
            {  
                // 特殊设置前四个gezi2元素  
                g.bo1.Width = 0;  
                g.zxzhi.FontSize = 23;  
                g.zxzhi.FontWeight = FontWeights.Bold;  
                var gm = g.zxzhi.Margin;  
                gm.Top -= 2;  
                gm.Left -= 8;  
                g.zxzhi.Margin = gm;  
            }  
  
            gezi2s.Add(g);  
  
            var border = CreateBorder(g, 42.45, 33);  
            AdjustBorderMargins(border, i, true);  
            w2.Children.Add(border);  
        }  
  
        // 设置提示信息和前景色  
        tishil.Content = "请输入前四手";  
        tishil.Foreground = Brushes.Black;  
  
        // 加载并解析规则文件  
        LoadAndParseRules();  
    }  
  
    Border CreateBorder(UIElement child, double width, double height = 0)  
    {  
        var border = new Border  
        {  
            Width = width,  
            Height = height == 0 ? double.NaN : height, // 使用NaN表示不设置高度  
            BorderThickness = new Thickness(1),  
            Child = child,  
            BorderBrush = new SolidColorBrush(Colors.Black)  
        };  
        child.Margin = new Thickness(0); // 可以在这里统一设置Margin，但具体调整在AdjustBorderMargins中  
        return border;  
    }  
  
    void AdjustBorderMargins(Border border, int index, bool adjustBorderThickness = false)  
    {  
        var margin = border.Margin;  
  
        // 调整左边距（从第6个元素开始）  
        if (index > 5)  
        {  
            margin.Left = -1;  
        }  
  
        // 调整上边距（每6个元素调整一次）  
        if (index % 6 == 1 || index < 6) // 注意：这里修改了条件以避免不必要的重复调整  
        {  
            margin.Top = -1;  
        }  
  
        // 如果需要，调整上边框厚度  
        if (adjustBorderThickness)  
        {  
            var borderThickness = border.BorderThickness;  
            borderThickness.Top = 0;  
            border.BorderThickness = borderThickness;  
        }  
  
        border.Margin = margin;  
    }  
  
    void LoadAndParseRules()  
    {  
        try  
        {  
            var s = File.ReadAllText("jubu.data");  
            var decrypted = Decrypt(s);  
            jbguizes = JsonSerializer.Deserialize<List<jubudata>>(decrypted);  
            if (jbguizes.Count == 0)  
            {  
                MessageBox.Show("规则不能为空");  
                Close();  
            }  
        }  
        catch (Exception ee)  
        {  
            MessageBox.Show(ee.Message);  
            Close();  
        }  
    }
    // ... 其他方法保持不变 ...  

private void Button_Click_1(object sender, RoutedEventArgs e)  
{  
    if (jilusuoyin == 0)  
    {  
        return;  
    }  
  
    // 处理重置逻辑  
    if (cunchusuoyin > 0 && jilusuoyin >= 66)  
    {  
        dizenggezifenshucishu++;  
        jilusuoyin = cunchusuoyin;  
        cunchusuoyin = -1;  
        return; // 重置后直接返回，避免执行下面的撤销逻辑  
    }  
  
    // 处理撤销逻辑（仅当jilusuoyin大于或等于6时执行）  
    if (jilusuoyin < 6)  
    {  
        // 撤销到当前步骤之前的所有下注  
        for (int i = 0; i < jilusuoyin; i++)  
        {  
            UndoBet(i);  
        }  
        jilusuoyin = 0; // 重置到第一步之前  
        Button_Click(null, null); // 重置游戏状态  
        return;  
    }  
  
    // 撤销当前步骤的下注  
    UndoBet(jilusuoyin - 1);  
    jilusuoyin--; // 回到上一步  
  
    // 更新界面和逻辑  
    tishi();  
    jisuanfenshu();  
  
    // 提取撤销下注的逻辑到单独的方法中  
    void UndoBet(int index)  
    {  
        gezi1s[index].Zxzhis = "";  
        gezi2s[index].Zxzhis = "";  
        gezi2s[index].Fenshu = 0;  
  
        // 如果上一步有具体的下注分数，则减去该分数  
        if (!string.IsNullOrEmpty(gezi2s[index + 1].Zxzhis) && int.TryParse(gezi2s[index + 1].Zxzhis, out int scoreToRemove))  
        {  
            zongfen -= scoreToRemove;  
        }  
  
        // 更新上一次的赌注基数（shangyicijifen）  
        var lastValidScore = gezi2s.Skip(4).LastOrDefault(x => int.TryParse(x.Zxzhis, out _));  
        shangyicijifen = lastValidScore?.Zxzhis.TryGetInt() ?? 1;  
  
        // 辅助方法：尝试将字符串转换为整数，失败时返回默认值  
        int? TryGetInt(this string str)  
        {  
            if (int.TryParse(str, out int result))  
                return result;  
            return null;  
        }  
    }  
}
