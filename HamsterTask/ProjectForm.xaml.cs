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
    /// Логика взаимодействия для ProjectForm.xaml
    /// </summary>
    public partial class ProjectForm : Window
    {
        public ProjectForm()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }


        string[] parts = Global.Guid.Split('|');
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

   
        private void RemTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (MessageBox.Show(TryFindResource("RemProj").ToString()) == MessageBoxResult.OK){
                    var proj = Helper.Http.GetRequest("http://localhost:8080/RemoveProject/" + parts[0] + "/" + Global.GlobProjectID);
                    if (proj != "No")
                    {
                        MessageBox.Show(TryFindResource("RemovedProj").ToString());
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }

        private void AddThing_Click(object sender, RoutedEventArgs e)
        {
            new AddTask().Show();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var proj = Helper.Http.GetRequest("http://localhost:8080/GetProject/" + parts[0] + "/" + Global.GlobProjectID).Split('|');
                var taskcountnotdone = Helper.Http.GetRequest("http://localhost:8080/GetProjectTaskCountNotDone/" + parts[0] + "/" + Global.GlobProjectID);
                var taskcountdone = Helper.Http.GetRequest("http://localhost:8080/GetProjectTaskCountDone/" + parts[0] + "/" + Global.GlobProjectID);
                var taskcountend = Helper.Http.GetRequest("http://localhost:8080/GetProjectTaskCountEnd/" + parts[0] + "/" + Global.GlobProjectID);

                ProjName.Content = proj[0];
                ProjDesk.Text = proj[1];
                Own.Content += " " + proj[2];
                Deadline.Content += " " + proj[3];

                if (taskcountnotdone != "No!" || taskcountnotdone != "0")
                {
                    for (int i = 0; i < Convert.ToInt32(taskcountnotdone); i++)
                    {
                        var data = Helper.Http.GetRequest("http://localhost:8080/GetProjectTaskNotDone/" + parts[0] + "/" + i.ToString()).Split('|');
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
                        projstack.Children.Add(control);
                        projstack.UpdateLayout();
                      
                    }
                }

                if (taskcountdone != "No!" || taskcountdone != "0")
                {
                    for (int i = Convert.ToInt32(taskcountnotdone); i < Convert.ToInt32(taskcountnotdone) + Convert.ToInt32(taskcountdone); i++)
                    {
                        var data = Helper.Http.GetRequest("http://localhost:8080/GetProjectTaskDone/" + parts[0] + "/" + (i - Convert.ToInt32(taskcountnotdone)).ToString()).Split('|');
                        var control = new TaskControl();
                        control.TaskName.Content = data[0];
                        control.TaskExecutor.Content += " " + data[1];
                        control.deadLine.Content += " " + data[2];
                        control.TaskID.Content = data[4];
                        control.Owner.Content += " " + data[3];
                        control.Margin = new Thickness(0, 5, 0, 5);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);
                        projstack.Children.Add(control);
                        projstack.UpdateLayout();
                    }
                }

                if (taskcountend != "No!" || taskcountend != "0")
                {
                    for (int i = Convert.ToInt32(taskcountnotdone) + Convert.ToInt32(taskcountdone); i < Convert.ToInt32(taskcountnotdone) + Convert.ToInt32(taskcountdone) + Convert.ToInt32(taskcountend); i++)
                    {
                        var data = Helper.Http.GetRequest("http://localhost:8080/GetProjectTaskEnd/" + parts[0] + "/" + (i - Convert.ToInt32(taskcountnotdone) - Convert.ToInt32(taskcountdone)).ToString()).Split('|');
                        var control = new TaskControl();
                        control.TaskName.Content = data[0];
                        control.TaskExecutor.Content += " " + data[1];
                        control.deadLine.Content += " " + data[2];
                        control.TaskID.Content = data[4];
                        control.Owner.Content += " " + data[3];
                        control.Background = new LinearGradientBrush(Colors.LightSlateGray, Colors.Wheat, 90);
                        control.Margin = new Thickness(0, 5, 0, 5);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);
                        projstack.Children.Add(control);
                        projstack.UpdateLayout();
                    }
                }


                if (taskcountend == "No!" && taskcountdone == "No!" && taskcountnotdone == "No!")
                {
                    projstack.Children.Add(new Label { Content = TryFindResource("NoneTask").ToString() });
                }

            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }
    }
}
