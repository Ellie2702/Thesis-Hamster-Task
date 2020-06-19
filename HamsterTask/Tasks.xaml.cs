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
    /// Логика взаимодействия для Tasks.xaml
    /// </summary>
    public partial class Tasks : Window
    {
        public Tasks()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');

        private void UpdateAll()
        {
            if(resave == true)
            {
                UpdateTask();
                UpdateProj();
                resave = false;
            }
        }

            private void AddThing_Click(object sender, RoutedEventArgs e)
        {
            if (TabTask.IsSelected)
            {
                Global.FROM = "UserPanel";
                new AddTask().Show();

            }
            else if (TabProject.IsSelected)
            {
                Global.FROM = "UserPanel";
                new AddProject().Show();
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new UserPanel().Show();
            this.Close();
        }



        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateProj();
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            UpdateTask();
        }

        public static bool resave;
        private void UpdateTask()
        {
            try
            {
                TaskStack1.Children.Clear();
                TaskStack2.Children.Clear();
                string taskend = Helper.Http.GetRequest("http://localhost:8080/GetTasksEndCount/" + parts[0]);
                string taskdone = Helper.Http.GetRequest("http://localhost:8080/GetTasksDoneCount/" + parts[0]);
                string tasknotdone = Helper.Http.GetRequest("http://localhost:8080/GetTasksNotDoneCount/" + parts[0]);
//                int boost = 0;
                if (tasknotdone != "No!" || tasknotdone != "0")
                {
                    for (int i = 0; i < Convert.ToInt32(tasknotdone); i++)
                    {
                        var data = Helper.Http.GetRequest("http://localhost:8080/GetTasksNotDone/" + parts[0] + "/" + i.ToString()).Split('|');
                        var control = new TaskControl();
                        control.TaskName.Content = data[0];
                        control.TaskExecutor.Content += " " + data[1];
                        control.deadLine.Content += " " + data[2];
                        control.TaskID.Content = data[4];
                        control.Owner.Content += " " + data[3];
                        control.Margin = new Thickness(0, 5, 0, 5);
                        control.Background = new LinearGradientBrush(Colors.Aqua, Colors.Wheat, 90);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);
                        if (i % 2 != 0)
                        {
                            TaskStack2.Children.Add(control);
                            TaskStack2.UpdateLayout();
                        }
                        else
                        {
                            TaskStack1.Children.Add(control);
                            TaskStack1.UpdateLayout();
                        }
 //                       boost = i;
                    }
                }

                if (taskdone != "No!" || taskdone != "0")
                {
                    for (int i = Convert.ToInt32(tasknotdone); i < Convert.ToInt32(tasknotdone) + Convert.ToInt32(taskdone); i++)
                    {
                        var data = Helper.Http.GetRequest("http://localhost:8080/GetTasksDone/" + parts[0] + "/" + (i - Convert.ToInt32(tasknotdone)).ToString()).Split('|');
                        var control = new TaskControl();
                        control.TaskName.Content = data[0];
                        control.TaskExecutor.Content += " " + data[1];
                        control.deadLine.Content += " " + data[2];
                        control.TaskID.Content = data[4];
                        control.Owner.Content += " " + data[3];
                        control.Margin = new Thickness(0, 5, 0, 5);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);
                        if (i % 2 != 0)
                        {
                            TaskStack2.Children.Add(control);
                            TaskStack2.UpdateLayout();
                        }
                        else
                        {
                            TaskStack1.Children.Add(control);
                            TaskStack1.UpdateLayout();
                        }
                    }
                }

                if (taskend != "No!" || taskend != "0")
                {
                    for (int i = Convert.ToInt32(tasknotdone) + Convert.ToInt32(taskdone); i < Convert.ToInt32(tasknotdone) + Convert.ToInt32(taskdone) + Convert.ToInt32(taskend); i++)
                    {
                        var data = Helper.Http.GetRequest("http://localhost:8080/GetTasksEnd/" + parts[0] + "/" + (i - Convert.ToInt32(tasknotdone) - Convert.ToInt32(taskdone)).ToString()).Split('|');
                        var control = new TaskControl();
                        control.TaskName.Content = data[0];
                        control.TaskExecutor.Content += " " + data[1];
                        control.deadLine.Content += " " + Convert.ToDateTime(data[3]).ToShortDateString();
                        control.TaskID.Content = data[4];
                        control.Owner.Content += " " + data[3];
                        control.Background = new LinearGradientBrush(Colors.LightSlateGray, Colors.Wheat, 90);
                        control.Margin = new Thickness(0, 5, 0, 5);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);
                        if (i % 2 != 0)
                        {
                            TaskStack2.Children.Add(control);
                            TaskStack2.UpdateLayout();
                        }
                        else
                        {
                            TaskStack1.Children.Add(control);
                            TaskStack1.UpdateLayout();
                        }
                    }
                }


                if(taskend == "No!" && taskdone == "No!" && tasknotdone == "No!")
                {
                    TaskStack2.UpdateLayout();
                    TaskStack1.UpdateLayout();
                    NoneTask.Visibility = Visibility.Visible;
                }
            }
            catch (Exception Ex)
            {
                NoneTask.Visibility = Visibility.Visible;
            }
        }

        private void UpdateProj()
        {
            try
            {
                StackProj1.Children.Clear();
                StackProj2.Children.Clear();

                string proj = Helper.Http.GetRequest("http://localhost:8080/GetProjectCount/" + parts[0]);
                if (proj != "No!" && proj != "0")
                {
                    for (int i = 0; i < Convert.ToInt32(proj); i++)
                    {
                        var data = Helper.Http.GetRequest("http://localhost:8080/GetProject/" + parts[0] + "/" + i.ToString()).Split('|');
                        var control = new ProjectControll();
                        control.ProjectName.Content = data[0];
                        control.Deadline.Content += Environment.NewLine + Convert.ToDateTime(data[3]).ToShortDateString();
                        control.descript.Text = data[1];
                        control.ProjectID.Content = data[4];
                        control.Margin = new Thickness(0, 5, 0, 5);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);
                        if (i % 2 != 0)
                        {
                            StackProj2.Children.Add(control);
                            StackProj2.UpdateLayout();
                        }
                        else
                        {
                            StackProj1.Children.Add(control);
                            StackProj1.UpdateLayout();
                        }
                    }
                } else NoneProj.Visibility = Visibility.Visible;

            }
            catch (Exception Ex)
            {
                NoneProj.Visibility = Visibility.Visible;
            }
        }
    }
}
