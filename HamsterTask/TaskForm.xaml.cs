﻿using System;
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
    /// Логика взаимодействия для TaskForm.xaml
    /// </summary>
    public partial class TaskForm : Window
    {
        public TaskForm()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void RemTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                if (MessageBox.Show(TryFindResource("MessRemTask").ToString(), TryFindResource("MessRemTaskHead").ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var res = Helper.Http.GetRequest("http://localhost:8080/DeleteTask/" + parts[0] + "/" + Global.GlobTaskID);
                    if(res == "Deleted!")
                    {
                        MessageBox.Show(TryFindResource("MessRemTaskRes").ToString());
                        HamsterTask.Tasks.resave = true;
                    } else
                    {
                        MessageBox.Show(TryFindResource("MessRemTaskResFail").ToString());
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(TryFindResource("MessRemTaskResFail").ToString());
                this.Close();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                if(Global.FROM == "Company")
                {
                    if (Global.t == "false")
                    {
                        Edit.Visibility = Visibility.Hidden;
                        RemTask.Visibility = Visibility.Hidden;
                    }
                }

                var res = Helper.Http.GetRequest("http://localhost:8080/GetTask/" + parts[0] + "/" + Global.GlobTaskID).Split('|');

                TaskName.Content = res[1];
                TaskDesk.Text = res[2];
                Exec.Content += " " + res[5];
                Own.Content += " " + res[4];
                Deadline.Content += " " + res[3];

                if(Convert.ToDateTime(res[3]) <= DateTime.Today)
                {
                    TaskFailed.Visibility = Visibility.Visible;
                    IsDone.Visibility = Visibility.Hidden;
                }

                if(res[6] == "True")
                {
                    IsDone.Visibility = Visibility.Hidden;
                    Done.Visibility = Visibility.Visible;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }

        private void IsDone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                if (MessageBox.Show(TryFindResource("TaskIsDone").ToString(), TryFindResource("TaskIsDoneHead").ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var res = Helper.Http.GetRequest("http://localhost:8080/TaskIsDone/" + parts[0] + "/" + Global.GlobTaskID);
                    if (res == "IsDone")
                    {
                        IsDone.Visibility = Visibility.Hidden;
                        Done.Visibility = Visibility.Visible;
                    }
                    else MessageBox.Show(TryFindResource("TaskIsDoneWarn").ToString());
                    HamsterTask.Tasks.resave = true;
                }
            }
            catch
            {

            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EditTask().Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                new UserPanel().Show();
                this.Close();
            }
        }
    }
}
