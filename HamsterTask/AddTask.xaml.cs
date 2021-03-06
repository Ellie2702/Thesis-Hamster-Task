﻿using System;
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
    public partial class AddTask : Window
    {
        public AddTask()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            new Tasks().Show();
            this.Close();
        }
        string[] parts = Global.Guid.Split('|');
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
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
                }
            }
            catch {
                MessageBox.Show(TryFindResource("Fail").ToString());
            }
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                string title = TaskName.Text;
                string descript = TaskDesc.Text;
                string deadline = TaskDeadline.SelectedDate.ToString();
                if(deadline == null)
                {
                    MessageBox.Show(TryFindResource("DateNull").ToString());
                } else
                {
                    string data = string.Empty;
                    switch (Global.FROM)
                    {
                        
                        case "UserPanel":
                            if (Global.FROM1 == "Project")
                            {
                                data = Helper.Http.GetRequest("http://localhost:8080/CreateTask/" + parts[0] + "/UP/" + title + "/" + WebUtility.UrlEncode(descript) + "/" + deadline + "/" + Global.GlobProjectID);
                                if (data == "Task is added")
                                {
                                    MessageBox.Show(TryFindResource("TaskIsAdded").ToString());
                                    HamsterTask.Tasks.resave = true;
                                }
                                else MessageBox.Show(TryFindResource("TaskIsNotAdded").ToString());
                            }
                            else
                            {
                                data = Helper.Http.GetRequest("http://localhost:8080/CreateTask/" + parts[0] + "/U/" + title + "/" + WebUtility.UrlEncode(descript) + "/" + deadline);
                                if (data == "Task is added")
                                {
                                    MessageBox.Show(TryFindResource("TaskIsAdded").ToString());
                                    HamsterTask.Tasks.resave = true;
                                }
                                else MessageBox.Show(TryFindResource("TaskIsNotAdded").ToString());
                            }
                            break;
                        case "Company":
                            if (Global.FROM1 == "Project")
                            {
                                if (Me.IsChecked == true)
                                {
                                    data = Helper.Http.GetRequest("http://localhost:8080/CreateTask/" + parts[0] + "/CP/" + title + "/" + WebUtility.UrlEncode(descript) + "/" + deadline + "/" + parts[5] + "/" + Global.GlobProjectID);
                                }
                                else
                                    data = Helper.Http.GetRequest("http://localhost:8080/CreateTask/" + parts[0] + "/CP/" + title + "/" + WebUtility.UrlEncode(descript) + "/" + deadline + "/" + MailExec.Text + "/" + Global.GlobProjectID);
                                if (data == "Task is added")
                                {
                                    MessageBox.Show(TryFindResource("TaskIsAdded").ToString());
                                    HamsterTask.Tasks.resave = true;
                                }
                                else MessageBox.Show(TryFindResource("TaskIsNotAdded").ToString());
                            }
                            else
                            {
                                if (Me.IsChecked == true)
                                {
                                    data = Helper.Http.GetRequest("http://localhost:8080/CreateTask/" + parts[0] + "/C/" + title + "/" + WebUtility.UrlEncode(descript) + "/" + deadline + "/" + parts[5]);
                                }
                                else
                                    data = Helper.Http.GetRequest("http://localhost:8080/CreateTask/" + parts[0] + "/C/" + title + "/" + WebUtility.UrlEncode(descript) + "/" + deadline + "/" + MailExec.Text);
                                if (data == "Task is added")
                                {
                                    MessageBox.Show(TryFindResource("TaskIsAdded").ToString());
                                    HamsterTask.Tasks.resave = true;
                                }
                                else MessageBox.Show(TryFindResource("TaskIsNotAdded").ToString());
                            }
                            break;
                    }

                    
                }
            }
            catch (Exception Ex)
            {
               
            }
        }

        private void pastemail_Click(object sender, RoutedEventArgs e)
        {
            MailExec.Text = Global.userMail;
        }
    }
}
