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
    /// Логика взаимодействия для AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        public AddProject()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            switch (Global.FROM)
            {
                case "UserPanel":
                    new Tasks().Show();
                    break;
                case "Company":
                    new Company().Show();
                    break;
            }
            this.Close();
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProjectName.Text != null && TaskDesc != null && TaskDeadline != null)
                {
                    switch (Global.FROM)
                    {
                        case "UserPanel":
                            var res = Helper.Http.GetRequest("http://localhost:8080/CreateProject/" + parts[0] + "/" + "U" + "/" + ProjectName.Text + "/" + TaskDesc.Text + "/" + TaskDeadline.SelectedDate.ToString());
                            if(res == "Ok!")
                            {
                                MessageBox.Show(TryFindResource("ProjectAdded").ToString());
                            }
                            else
                            {
                                MessageBox.Show(TryFindResource("ProjectNotAdded").ToString());
                            }
                            break;
                        case "Company":
                            var r = Helper.Http.GetRequest("http://localhost:8080/CreateProject/" + parts[0] + "/" + "C" + "/" + ProjectName.Text + "/" + TaskDesc.Text + "/" + TaskDeadline.SelectedDate.ToString());
                            if (r == "Ok!")
                            {
                                MessageBox.Show(TryFindResource("ProjectAdded").ToString());
                            }
                            else
                            {
                                MessageBox.Show(TryFindResource("ProjectNotAdded").ToString());
                            }
                            break;
                    }
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
