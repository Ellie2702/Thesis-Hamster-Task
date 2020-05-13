using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HamsterTask
{
    public class Global
    {
        public static string GlobLang;
        public static void LanguageSwitch(Window win)
        {
            ResourceDictionary res = new ResourceDictionary();
            switch (Global.GlobLang)
            {
                case "ENG":
                    res.Source = new Uri("/Language/ENG/EngDictionary.xaml", UriKind.Relative);
                    break;
                case "RUS":
                    res.Source = new Uri("/Language/RUS/RusDictionary.xaml", UriKind.Relative);
                    break;
            }
            win.Resources.MergedDictionaries.Add(res);

        }
    }
}
