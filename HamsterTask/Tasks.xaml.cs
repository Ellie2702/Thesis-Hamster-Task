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

        private void ProjectsHeader_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                var dataP = Helper.Http.GetRequest("http://localhost:8080/GetProjects/" + parts[1]);
                var dataT = Helper.Http.GetRequest("http://localhost:8080/GetTasks/" + parts[1]);
                if (dataP == "0")
                {
                    NoneProj.Visibility = Visibility.Visible;
                }
                else
                {
                    NoneProj.Visibility = Visibility.Hidden;
                }


                if (dataT == "0")
                {
                    NoneTask.Visibility = Visibility.Visible;
                }
                else
                {
                    NoneTask.Visibility = Visibility.Hidden;
                }

                if(dataT != null)
                {
                    ScrollViewer scroll = new ScrollViewer();
                    StackPanel stack = new StackPanel();
                    for(int i = 0; i < Convert.ToInt32(dataT); i++)
                    {
                        var task = Helper.Http.GetRequest("http://localhost:8080/GetTask/" + parts[1] + "/" + i.ToString()).Split('|');
                        stack.Children.Add(new Task(task[0], task[1], task[2], task[3]));
                        stack.UpdateLayout();
                        //TaskHeader.Content = new StackPanel();
                        //  ;
                    }
                    stack.Children.Add(scroll);
                    stack.UpdateLayout();
                    TaskHeader.UpdateLayout();
                }

            }
            catch
            {

            }
        }
    }
}
