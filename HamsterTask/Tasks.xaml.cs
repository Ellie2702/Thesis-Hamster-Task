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

        public int k = 1;
        public int l = 1;
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                var dataT = Helper.Http.GetRequest("http://localhost:8080/GetTasks/" + parts[0]);
                
                if (dataT == "0")
                {
                    NoneTask.Visibility = Visibility.Visible;
                    LastPageTask.Visibility = Visibility.Hidden;
                    NexPageTask.Visibility = Visibility.Hidden;
                    Task1.Visibility = Visibility.Hidden;
                    Task2.Visibility = Visibility.Hidden;
                    Task3.Visibility = Visibility.Hidden;
                    Task4.Visibility = Visibility.Hidden;
                    Task5.Visibility = Visibility.Hidden;
                    Task6.Visibility = Visibility.Hidden;
                    TabTask.UpdateLayout();
                } else
                {
                    int tasksCount = Convert.ToInt32(dataT);
                    int i = 0;
                    int z = 1;
                    int p = Convert.ToInt32(Math.Ceiling((double)Convert.ToInt32(dataT) / (double)6.0)); // count of pages

                    if(k == 1 && k < p) // set visibility of next/back buttons
                    {
                        LastPageTask.Visibility = Visibility.Hidden;
                        NexPageTask.Visibility = Visibility.Visible;
                    }

                    if (k == 1 && k == p)
                    {
                        LastPageTask.Visibility = Visibility.Hidden;
                        NexPageTask.Visibility = Visibility.Hidden;
                    }

                    if (1 < k && k < p)
                    {
                        LastPageTask.Visibility = Visibility.Visible;
                        NexPageTask.Visibility = Visibility.Visible;
                    }

                    if(k > 1 && k == p)
                    {
                        LastPageTask.Visibility = Visibility.Visible;
                        NexPageTask.Visibility = Visibility.Hidden;
                    }
                    TabTask.UpdateLayout();

                    if (p > 0)
                    {
                        i = 6 * (k - 1); // number of first task on page
                        while (z < Math.Min(7, tasksCount - i+1))
                        {
                            var task = Helper.Http.GetRequest("http://localhost:8080/GetTask/" + parts[0] + "/" + i.ToString()).Split('|');
                            TaskControl temp = new TaskControl();
                            switch (z)
                            {
                                case 1:
                                    temp = Task1;
                                    break;
                                case 2:
                                    temp = Task2;
                                    break;
                                case 3:
                                    temp = Task3;
                                    break;
                                case 4:
                                    temp = Task4;
                                    break;
                                case 5:
                                    temp = Task5;
                                    break;
                                case 6:
                                    temp = Task6;
                                    break;
                            }
                            temp.Visibility = Visibility.Visible;
                            temp.TaskID.Content = task[0];
                            temp.TaskName.Content = task[1];
                            temp.TaskDescript.Content = task[2];
                            temp.UserExecutor.Content = task[5];
                            temp.deadLine.Content = task[4];
                            if(task[6].ToLower() == "true")
                            {
                                temp.Done.Visibility = Visibility.Visible;
                            }
                            
                            TabTask.UpdateLayout();
                            z++;
                            i++;
                        }

                        while (z < 7)
                        {
                            switch (z)
                            {
                                case 1:
                                    Task1.Visibility = Visibility.Hidden;
                                    break;
                                case 2:
                                    Task2.Visibility = Visibility.Hidden;
                                    break;
                                case 3:
                                    Task3.Visibility = Visibility.Hidden;
                                    break;
                                case 4:
                                    Task4.Visibility = Visibility.Hidden;
                                    break;
                                case 5:
                                    Task5.Visibility = Visibility.Hidden;
                                    break;
                                case 6:
                                    Task6.Visibility = Visibility.Hidden;
                                    break;
                            }
                            z++;
                        }
                    }
                    /*else
                    {
                        while (z > Convert.ToInt32(dataT) + 1)
                        {
                            var task = Helper.Http.GetRequest("http://localhost:8080/GetTask/" + parts[0] + "/" + i.ToString()).Split('|');
                            TaskControl temp = new TaskControl();

                            temp.TaskID.Content = task[0];
                            temp.TaskName.Content = task[1];
                            temp.TaskDescript.Content = task[2];
                            temp.UserExecutor.Content = task[5];
                            temp.deadLine.Content = task[4];
                            if(task[6] == "true")
                            {
                                temp.Done.Visibility = Visibility.Visible;
                            }


                            switch (z)
                            {
                                case 1:
                                    Task1 = temp;
                                    break;
                                case 2:
                                    Task2 = temp;
                                    break;
                                case 3:
                                    Task3 = temp;
                                    break;
                                case 4:
                                    Task4 = temp;
                                    break;
                                case 5:
                                    Task5 = temp;
                                    break;
                                case 6:
                                    Task6 = temp;
                                    break;
                            }
                            TabTask.UpdateLayout();
                            z++;
                            i++;
                        }
                    }*/
                    
                }
            }
            catch
            {
                
            }
       
        }

        private void LastPageTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                k++;
                TabTask.UpdateLayout();
            }
            catch
            {

            }
        }

        private void NexPageTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                k--;
                TabTask.UpdateLayout();
            }
            catch
            {

            }
        }

        private void AddThing_Click(object sender, RoutedEventArgs e)
        {
            if (TabTask.IsSelected)
            {
                Global.FROM = "UserPanel";
                new AddTask().Show();
                this.Close();

            } else if (TabProject.IsSelected)
            {
                Global.FROM = "UserPanel";
                new AddProject().Show();
                this.Close();
            }
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                var dataT = Helper.Http.GetRequest("http://localhost:8080/GetUserProjectsC/" + parts[0]);

                if (dataT == "0" || dataT == null)
                {
                    NoneProj.Visibility = Visibility.Visible;
                    LastPageProj.Visibility = Visibility.Hidden;
                    NexPageProj.Visibility = Visibility.Hidden;
                    Proj1.Visibility = Visibility.Hidden;
                    Proj2.Visibility = Visibility.Hidden;
                    Proj3.Visibility = Visibility.Hidden;
                    Proj4.Visibility = Visibility.Hidden;
                    TabProject.UpdateLayout();
                } else
                {
                    int i = 0;
                    int z = 1;
                    int p = Convert.ToInt32(Math.Ceiling((double)Convert.ToInt32(dataT) / (double)4.0));

                    if (l == 1 && l < p)
                    {
                        LastPageProj.Visibility = Visibility.Hidden;
                        NexPageProj.Visibility = Visibility.Visible;
                    }

                    if (l == 1 && l == p)
                    {
                        LastPageProj.Visibility = Visibility.Hidden;
                        NexPageProj.Visibility = Visibility.Hidden;
                    }

                    if (1 < l && l < p)
                    {
                        LastPageProj.Visibility = Visibility.Visible;
                        NexPageProj.Visibility = Visibility.Visible;
                    }

                    if (l > 1 && l == p)
                    {
                        LastPageProj.Visibility = Visibility.Visible;
                        NexPageProj.Visibility = Visibility.Hidden;
                    }

                    TabProject.UpdateLayout();
                    if (p > 1)
                    {
                        i = (4 * l) - 4;
                        while (z < 5)
                        {
                            var proj = Helper.Http.GetRequest("http://localhost:8080/GetUserProjects/" + parts[0] + "/" + i.ToString()).Split('|');
                            ProjectControll temp = new ProjectControll();

                            temp.ProjectID.Content = proj[0];
                            temp.ProjectDesc.Text = proj[2];
                            temp.ProjectName.Content = proj[1];
                            temp.DateDeadline.Content = proj[3];
                            temp.TasksDone.Content += " " + proj[4];

                           
                            switch (z)
                            {
                                case 1:
                                    Proj1 = temp;
                                    break;
                                case 2:
                                    Proj2 = temp;
                                    break;
                                case 3:
                                    Proj3 = temp;
                                    break;
                                case 4:
                                    Proj4 = temp;
                                    break;
                            }
                            TabProject.UpdateLayout();
                            z++;
                            i++;
                        }
                    }
                    else
                    {
                        while (z > Convert.ToInt32(dataT) + 1)
                        {
                            var proj = Helper.Http.GetRequest("http://localhost:8080/GetUserProjects/" + parts[0] + "/" + i.ToString()).Split('|');

                            ProjectControll temp = new ProjectControll();

                            temp.ProjectID.Content = proj[0];
                            temp.ProjectDesc.Text = proj[2];
                            temp.ProjectName.Content = proj[1];
                            temp.DateDeadline.Content = proj[3];
                            temp.TasksDone.Content += " " + proj[4];


                            switch (z)
                            {
                                case 1:
                                    Proj1 = temp;
                                    break;
                                case 2:
                                    Proj2 = temp;
                                    break;
                                case 3:
                                    Proj3 = temp;
                                    break;
                                case 4:
                                    Proj4 = temp;
                                    break;
                            }
                            TabProject.UpdateLayout();
                            z++;
                            i++;
                        }
                    }
                }

            }
            catch
            {

            }
        }

        private void NexPageProj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                l++;
                TabProject.UpdateLayout();
            }
            catch { }
        }

        private void LastPageProj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                l--;
                TabProject.UpdateLayout();
            }
            catch { }
        }
    }
}
