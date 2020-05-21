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
using HamsterTask.Helper;

namespace HamsterTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegOrgBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new RegistrationOrg().Show();
            this.Close();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Global.GlobLang = "RUS";
            Global.LanguageSwitch(this);
        }

        private void EngSw_Click(object sender, RoutedEventArgs e)
        {
            Global.GlobLang = "ENG";
            Border.Margin = EngSw.Margin;
            Global.LanguageSwitch(this);
        }

        private void RusSw_Click(object sender, RoutedEventArgs e)
        {
            Global.GlobLang = "RUS";
            Border.Margin = RusSw.Margin;
            Global.LanguageSwitch(this);
        }

        private void RegEmpBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new RegistrationUser().Show();
            this.Close();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Global.Guid = Helper.Http.GetRequest("http://localhost:8080/auth/" + Login.Text + "/" + Password.Password);
            MessageBox.Show(Global.Guid);
            new UserPanel().Show();
            this.Close();
        }

        
    }
}

