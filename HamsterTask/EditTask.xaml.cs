using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        public EditTask()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            new Tasks().Show();
            this.Close();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');;
                if (Global.FROM != null)
                {
                    switch (Global.FROM)
                    {
                        case "UserPanel":
                            MailExec.Visibility = Visibility.Hidden;
                            TaskExecSel.Visibility = Visibility.Hidden;
                            pastemail.Visibility = Visibility.Hidden;
                            break;
                        case "Company":
                            MailExec.Visibility = Visibility.Visible;
                            TaskExecSel.Visibility = Visibility.Visible;
                            pastemail.Visibility = Visibility.Visible;

                            break;
                    }
                    var data = Helper.Http.GetRequest("http://localhost:8080/GetEditTask/" + parts[0] + "/" + Global.GlobTaskID).Split('|');
                    if(data[0] != null && data[0] != "No!")
                    {
                        TaskName.Text = data[0];
                        TaskDesc.Text = data[1];
                        TaskDeadline.SelectedDate = Convert.ToDateTime(data[2]);
                        if(MailExec.Visibility == Visibility.Visible)
                        {
                            MailExec.Text = data[4];
                        } else
                        {
                            Me.IsChecked = true;
                        }
                    } else 
                    {
                        MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(TryFindResource("Fail").ToString());
            }
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                string title = TaskName.Text;
                string descript = TaskDesc.Text;
                string deadline = TaskDeadline.SelectedDate.ToString();
                if(deadline == null)
                {
                    MessageBox.Show(TryFindResource("DateNull").ToString());
                } else
                {

                    switch (Global.FROM)
                    {
                        case "UserPanel":
                            string data = Helper.Http.GetRequest("http://localhost:8080/EditTask/" + parts[0] + "/U/" + "/" + Global.GlobTaskID + "/"+ title + "/" + WebUtility.UrlEncode(descript) + "/" + deadline);
                            if (data == "Ok!")
                            {
                                MessageBox.Show(TryFindResource("ChangesIsSaved").ToString());
                                HamsterTask.Tasks.resave = true;
                            }
                            else MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                            break;
                        case "Company":
                            string d = string.Empty;
                            if (Me.IsChecked == false)
                            {
                                d = Helper.Http.GetRequest("http://localhost:8080/EditTask/" + parts[0] + "/C/" + "/" + Global.GlobTaskID + "/" + title + "/" + WebUtility.UrlEncode(descript) + "/" + deadline + "/" + MailExec.Text);
                            } else
                            {
                                d = Helper.Http.GetRequest("http://localhost:8080/EditTask/" + parts[0] + "/C/" + "/" + Global.GlobTaskID + "/" + title + "/" + WebUtility.UrlEncode(descript) + "/" + deadline + "/" + parts[5]);
                            }
                            if (d == "Ok!")
                            {
                                MessageBox.Show(TryFindResource("ChangesIsSaved").ToString());
                                HamsterTask.Tasks.resave = true;
                            }
                            else MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                            break;
                    }
                    new TaskForm().Show();
                    this.Close();
                    
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                new UserPanel().Show();
                this.Close();
            }
        }

        private void pastemail_Click(object sender, RoutedEventArgs e)
        {
            MailExec.Text = Global.userMail;
        }
    }
}
