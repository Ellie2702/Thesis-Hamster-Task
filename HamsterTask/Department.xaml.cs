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
    /// Логика взаимодействия для Department.xaml
    /// </summary>
    public partial class Department : Window
    {
        public Department()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EditDep().Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }

        private void RemEmp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var r = Helper.Http.GetRequest("http://localhost:8080/RemDepartment/" + parts[0] + "/" + Global.GlobDepID);
                if(r == "Ok!")
                {
                    MessageBox.Show(TryFindResource("DelDep").ToString());

                } else
                {
                    MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }

        public void load()
        {
            try
            {
                DepName.Content = Global.GlobDepName;
                var r = Helper.Http.GetRequest("http://localhost:8080/GetDepEmpCount/" + parts[0] + "/" + Global.GlobDepID);
                if (r != null && r != "0")
                {
                    EmpsComp.Children.Clear();
                    EmpsComp.UpdateLayout();

                    if (r != "No!" && r != "0" && r != null)
                    {
                        for (int k = 0; k < Convert.ToInt32(r); k++)
                        {
                            var res1 = Helper.Http.GetRequest("http://localhost:8080/GetDepartmentEmp/" + parts[0] + "/" + Global.GlobDepID + "/" + k.ToString()).Split('|');
                            var control = new EmpControl();
                            control.EmpName.Content = res1[0];
                            control.Position.Content += " " + res1[1];
                            control.Email.Content += " " + res1[2];
                            control.EmpID.Content = res1[3];
                            control.Margin = new Thickness(0, 5, 0, 5);
                            control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                            control.BorderThickness = new Thickness(2);

                            EmpsComp.Children.Add(control);
                            EmpsComp.UpdateLayout();

                        }
                    }
                    else
                    {
                        EmpsComp.Children.Add(new Label { Content = TryFindResource("NoEmps"), FontSize = 15 });
                        EmpsComp.UpdateLayout();
                    }

                }
                else
                {
                    EmpsComp.Children.Add(new Label { Content = TryFindResource("NoEmps"), FontSize = 15 });
                    EmpsComp.UpdateLayout();
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void pastemail_Click(object sender, RoutedEventArgs e)
        {
            mail.Text = Global.userMail;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/AddEmpInDepartment/" + parts[0] + "/" + Global.GlobDepID + "/" + mail.Text);
                if(res != null &  res != "No!")
                {
                    MessageBox.Show(TryFindResource("EmpAdded").ToString());
                    load();

                } else
                {
                    MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }
    }
}
