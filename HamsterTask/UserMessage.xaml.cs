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
    /// Логика взаимодействия для UserMessage.xaml
    /// </summary>
    public partial class UserMessage : Window
    {
        public UserMessage()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void pastemail_Click(object sender, RoutedEventArgs e)
        {
            MailTo.Text = Global.userMail;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new UserPanel().Show();
            this.Close();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TitleSend.Text != null && MessDesc.Text != null)
                {
                    string res = Helper.Http.GetRequest("http://localhost:8080/SendMessage/" + parts[0] + "/" + MailTo.Text + "/" + TitleSend.Text + "/" + MessDesc.Text);
                    if (res == "Ok!")
                    {
                        MessageBox.Show(TryFindResource("MessageIsSend").ToString());
                    }
                    else
                    {
                        MessageBox.Show(TryFindResource("MessageNotSend").ToString());
                    }
                } else MessageBox.Show(TryFindResource("MessageNotFull").ToString());
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }

        private void Mess_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            try
            {
                Mess.Children.Clear();
                string resNoN = Helper.Http.GetRequest("http://localhost:8080/UserMessagesNotCheckCount/" + parts[0]);
                string check = Helper.Http.GetRequest("http://localhost:8080/UserMessagesCheckCount/" + parts[0]);

                if (resNoN != "No!")
                {
                    int non = Convert.ToInt32(resNoN);
                    for (int i = 0; i < non; i++)
                    {
                        var a = Helper.Http.GetRequest("http://localhost:8080/UserMessagesNotCheck/" + parts[0] + "/" + i.ToString()).Split('|');
                        var control = new MessageControl();
                        control.Title.Content = a[1];
                        control.MessageID.Content = a[0];
                        control.Check.Content = "false";
                        control.From.Content += a[3];
                        control.Date.Content += a[2];
                        control.Margin = new Thickness(0, 5, 0, 5);
                        control.Background = new LinearGradientBrush(Colors.Aqua, Colors.Wheat, 90);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);
                        control.ViewMessage.Click += ViewMessage_Click;
                        Mess.Children.Add(control);

                    }
                    Mess.Children.Remove(nomess);
                    Mess.UpdateLayout();
                }

                if (check != "No!")
                {
                    int non = Convert.ToInt32(check);
                    for (int i = 0; i < non; i++)
                    {
                        var a = Helper.Http.GetRequest("http://localhost:8080/UserMessagesCheck/" + parts[0] + "/" + i.ToString()).Split('|');
                        var control = new MessageControl();
                        control.Title.Content = a[1];
                        control.Check.Content = "true";
                        control.MessageID.Content = a[0];
                        control.From.Content += a[3];
                        control.Date.Content += a[2];
                        control.Margin = new Thickness(0, 10, 0, 5);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);
                        Mess.Children.Add(control);

                    }
                    Mess.Children.Remove(nomess);
                    Mess.UpdateLayout();
                }

                if(check == "No!" && resNoN == "No!")
                {
                    nomess.Visibility = Visibility.Visible;
                }



            }
            catch 
            {
                nomess.Visibility = Visibility.Visible;
            }
        }

        private void ViewMessage_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
