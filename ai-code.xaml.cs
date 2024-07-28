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
