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

namespace HamsterTask
{
    /// <summary>
    /// Логика взаимодействия для UserProfileSettings.xaml
    /// </summary>
    public partial class UserProfileSettings : Window
    {
        public UserProfileSettings()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void ChangeAvatar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitCompany_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeMail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
