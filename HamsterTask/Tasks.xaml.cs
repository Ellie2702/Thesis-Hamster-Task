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
        }

        public int k = 1;
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
                } else
                {
                   
                    int i = 0;
                    int z = 1;
                    int p = Convert.ToInt32(Math.Ceiling((double)Convert.ToInt32(dataT) / (double)6.0));

                    if(k == 1 && k < p)
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

                    if(p > 1)
                    {
                        i = (6 * k) - 6;
                        while (z > 7)
                        {
                            var task = Helper.Http.GetRequest("http://localhost:8080/GetTask/" + parts[0] + "/" + i.ToString()).Split('|');
                            TaskControl temp = new TaskControl();

                            temp.TaskID.Content = task[0];
                            temp.TaskName.Content = task[1];
                            temp.TaskDescript.Content = task[2];
                            temp.UserExecutor.Content = task[5];
                            temp.deadLine.Content = task[4];

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
                    }
                    else
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
                    }
                    
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
                new AddTask().Show();
                this.Close();

            } else if (TabProject.IsSelected)
            {
                new AddProject().Show();
                this.Close();
            }
        }
    }
}
