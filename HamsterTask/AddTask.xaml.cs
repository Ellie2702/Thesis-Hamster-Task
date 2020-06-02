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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                List<Global.Emps> emps = new List<Global.Emps>();
                if (Global.FROM != null)
                {
                    switch (Global.FROM)
                    {
                        case "UserPanel":
                            TaskExec.Visibility = Visibility.Hidden;
                            TaskExecSel.Visibility = Visibility.Hidden;
                            break;
                        case "Company":
                            Global.Emps emps1 = new Global.Emps();
                            
                            var data = Helper.Http.GetRequest("http://localhost:8080/ChooseExec/" + parts[0]).Split('|');
                            string[] temp;
                            for (int i = 0; i < data.Length; i++)
                            {
                                temp = data[i].Split('/');
                                emps1.id = Convert.ToInt32(temp[0]);
                                emps1.name = temp[1];
                                emps.Add(emps1);
                            }
                            
                            TaskExec.ItemsSource = emps;
                            TaskExec.DisplayMemberPath = emps1.name;
                            TaskExec.SelectedIndex = emps1.id;
                            TaskExec.UpdateLayout();
                            break;
                    }
                }
            }
            catch {
                MessageBox.Show(TryFindResource("Fail").ToString());
            }
        }
    }
}
