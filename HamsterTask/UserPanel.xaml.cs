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
            MessageBox.Show(Global.Guid);
            string[] parts = Global.Guid.Split('|');
            UserName.Content = parts[3] + " " + parts[4];
            if(parts[parts.Length - 1] == "W")
            {
                Position.Content = parts[7];
                Company.Content = parts[8];
            } else if(parts[parts.Length - 1] == "WP")
            {
                
                Position.Content = parts[8];
                Company.Content = parts[9];
            }
                Birth.Content = parts[5];
                Email.Content = parts[6];

            if (parts[parts.Length - 1] != "W" && parts[parts.Length - 1] != "WP")
            {
                UserCompany.Visibility = Visibility.Hidden;
                BTN_IHaveCode.Visibility = Visibility.Visible;
            }
            else
            {
                UserCompany.Visibility = Visibility.Visible;
                BTN_IHaveCode.Visibility = Visibility.Hidden;
            }

            var task = Helper.Http.GetRequest("http://localhost:8080/GetTaskCount/" + parts[0] + "/");
            var mess = Helper.Http.GetRequest("http://localhost:8080/GetTaskCount/" + parts[0] + "/");

            if (task == "No tasks")
            {
                TaskCount.Visibility = Visibility.Hidden;
            }
            else TaskCount.Content = task + " " + TaskCount.Content;

            if (mess == "No messages")
            {
                MessagesCount.Visibility = Visibility.Hidden;
            }
            else MessagesCount.Content = mess + " " + MessagesCount.Content;

            string[] avatar = Helper.Http.GetRequest("http://localhost:8080/GetAvatarUser/" + parts[0] + "/").Split('|');

            if(avatar[0] != "No Avatar")
            {
                string[] temp = Directory.GetFiles(@"\UserFolder\Images\", avatar[1] + ".png", SearchOption.AllDirectories);
                if(temp != null)
                {
                    
                    UserA.Source = BitmapFrame.Create(new Uri("/UserFolder/Images/" + avatar[1] + ".png"));
                } else
                {
                    Encoding encoding = Encoding.Default;

                    byte[] Img = encoding.GetBytes(avatar[0]);
                    MemoryStream ms = new MemoryStream(Img);
                    Bitmap image = new Bitmap(ms);
                    image.Save(@"\UserFolder\Images\" + avatar[1] + ".png");
                    UserA.Source = BitmapFrame.Create(new Uri("/UserFolder/Images/" + avatar[1] + ".png"));
                }
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Global.Guid = String.Empty;
            new MainWindow().Show();
            this.Close();
        }

        private void UserTask_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
