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
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        public Message()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/UserMessage/" + parts[0] + "/" + Global.GlobMessID).Split('|');
                if(res != null || res[0] != "No!")
                {
                    Title.Content = res[0];
                    content.Text = res[1];
                    DateSend.Content += res[2];
                    From.Content += " " + res[3];
                    Mail.Content += " " + res[4];
                }

            } catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }

        private void Reply_Click(object sender, RoutedEventArgs e)
        {
            Global.userMail = Mail.Content.ToString().Replace(TryFindResource("LabelEmail").ToString(), "");
            MessageBox.Show(TryFindResource("CopyMess").ToString());
            this.Close();
        }
    }
}
