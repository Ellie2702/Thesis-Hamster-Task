using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HamsterTask
{
    public class Global
    {
        public static string GlobTaskID;
        public static string GlobProjectID;
        public static string GlobMessID;
        public static string GlobUserID;
        public static string FROM;

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

        internal static void LanguageSwitchControll(Control c)
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
            c.Resources.MergedDictionaries.Add(res);
        }

        public static void LanguageSwitchControll(TaskControl task)
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
            task.Resources.MergedDictionaries.Add(res);

        }

        public static string Guid;
        public static string RegOrgInfo;
        public static string userMail;

        public class Emps
        {
            public int id;
            public string name;
        }
    }
}
   
