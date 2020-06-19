using HamsterTask.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        public UserPanel()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Upload();
        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Global.Guid = String.Empty;
            new MainWindow().Show();
            this.Close();
        }

        private void UserTask_Click(object sender, RoutedEventArgs e)
        {
            Global.FROM = "UserPanel";
            new Tasks().Show();
            this.Close();
        }

        private void UserMessages_Click(object sender, RoutedEventArgs e)
        {
            new UserMessage().Show();
            this.Close();
        }

        private void BTN_IHaveCode_Click(object sender, RoutedEventArgs e)
        {
            new CompanyCode().Show();
        }

        private void CopyAdmin_Click(object sender, RoutedEventArgs e)
        {
            Global.userMail = AdminMail.Content.ToString();
        }

        private void UserCustom_Click(object sender, RoutedEventArgs e)
        {
            new UserProfileSettings().Show();
            this.Close();
        }

        private void Upload()
        {
            string[] part = Global.Guid.Split('|');
            string[] parts = Helper.Http.GetRequest("http://localhost:8080/Inform/" + part[0]).Split('|');
            UserName.Content = parts[3] + " " + parts[4];
            if (parts[parts.Length - 1] == "W")
            {
                Position.Content = parts[7];
                Company.Content = parts[8];
                var work = Helper.Http.GetRequest("http://localhost:8080/IsWork/" + parts[0]).Split('|');
                if (work != null || work[0] != "NotWorking")
                {
                    Iswork.Visibility = Visibility.Visible;
                    Iswork.Content = TryFindResource("IsWork").ToString() + work[1] + " - " + work[2];
                }
            }
            else if (parts[parts.Length - 1] == "WP")
            {

                Position.Content = parts[8];
                Company.Content = parts[9];
                var work = Helper.Http.GetRequest("http://localhost:8080/IsWork/" + parts[0]);
                if (work != null && work.ToString() != "NotWorking" && work.ToString() != "404")
                {
                    work.Split('|');
                    Iswork.Visibility = Visibility.Visible;
                    Iswork.Content = TryFindResource("IsWork").ToString() + work[1] + " - " + work[2];
                }
                Birth.Content = Convert.ToDateTime(parts[5]).ToShortDateString();
                Email.Content = parts[6];
            } else
            {
                UserName.Content = parts[3] + " " + parts[4];
                Birth.Content = Convert.ToDateTime(parts[5]).ToShortDateString();
                Email.Content = parts[6];
            }

            if (parts[parts.Length - 1] != "W" && parts[parts.Length - 1] != "WP")
            {
                UserCompany.Visibility = Visibility.Hidden;
                BTN_IHaveCode.Visibility = Visibility.Visible;
                PositionLB.Visibility = Visibility.Hidden;
                CompanyLBL.Visibility = Visibility.Hidden;
            }
            else
            {
                UserCompany.Visibility = Visibility.Visible;
                BTN_IHaveCode.Visibility = Visibility.Hidden;
            }

            var task = Helper.Http.GetRequest("http://localhost:8080/TaskCount/" + parts[0]);
            var mess = Helper.Http.GetRequest("http://localhost:8080/MessageCount/" + parts[0]);

            if (task == "No tasks")
            {
                TaskCount.Visibility = Visibility.Hidden;
            }
            else
            {
                TaskCount.Content = task + " " + TaskCount.Content;
                TaskCount.Visibility = Visibility.Visible;
            }


            if (mess == "No messages")
            {
                MessagesCount.Visibility = Visibility.Hidden;
            }
            else
            {
                MessagesCount.Content = mess + " " + MessagesCount.Content;
                MessagesCount.Visibility = Visibility.Visible;
            }

            try
            {
                UserA.Source = Images.GetMyAvatar();
            }
            catch { }
            this.UpdateLayout();
        }

        private void UserCompany_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Global.FROM = "Company";
                new Company().Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }
    }
}
