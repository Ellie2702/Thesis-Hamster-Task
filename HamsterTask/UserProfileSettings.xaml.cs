using HamsterTask.Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            this.Loaded += UserProfileSettings_Loaded;
        }

        private void UserProfileSettings_Loaded(object sender, RoutedEventArgs e)
        {
            MyAvaTAR.Source = Images.GetMyAvatar();
        }

        string[] parts = Global.Guid.Split('|');

        private void ChangeAvatar_Click(object sender, RoutedEventArgs e)
        {
            Images.UploadImageAvatar();
            MyAvaTAR.Source = Images.GetMyAvatar();
        }

        private void ExitCompany_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = Helper.Http.GetRequest("http://localhost:8080/LeaveCompany/" + parts[0]);
                if (res == "Ok!")
                {
                    MessageBox.Show(TryFindResource("LeaveCompany").ToString());
                }
                else
                {
                    MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }

        private void DeleteMe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = Helper.Http.GetRequest("http://localhost:8080/RemoveProfile/" + parts[0]);
                if (res == "Ok!")
                {
                    MessageBox.Show(TryFindResource("LeaveHamsterTask").ToString());
                    Global.Guid = string.Empty;
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new UserPanel().Show();
            this.Close();
        }

        private void ChangeLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = Helper.Http.GetRequest("http://localhost:8080/ChangeLogin/" + parts[0] + "/" + NewLogin.Text);
                if(res == "Ok!")
                {
                    MessageBox.Show(TryFindResource("LoginChanged").ToString());
                } else{
                    MessageBox.Show(TryFindResource("TryAgainL").ToString());
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = Helper.Http.GetRequest("http://localhost:8080/ChangePass/" + parts[0] + "/" + NewPass.Password);
                if (res == "Ok!")
                {
                    MessageBox.Show(TryFindResource("PassChanged").ToString());
                }
                else
                {
                    MessageBox.Show(TryFindResource("TryAgainL").ToString());
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }

        private void ChangeMail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = Helper.Http.GetRequest("http://localhost:8080/ChangeMail/" + parts[0] + "/" + NewPass.Password);
                if (res == "Ok!")
                {
                    MessageBox.Show(TryFindResource("ChangeMail").ToString());
                }
                else
                {
                    MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }
    }
}
