using HamsterTask.Helper;
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
    /// Логика взаимодействия для Company.xaml
    /// </summary>
    public partial class Company : Window
    {
        public Company()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Departaments.Items.Clear();
                Projects.Children.Clear();
                EmpsComp.Children.Clear();
                Departaments.UpdateLayout();
                Projects.UpdateLayout();
                Information.UpdateLayout();
                EmpsComp.UpdateLayout();
                string[] parts = Global.Guid.Split('|');
                var dep = Helper.Http.GetRequest("http://localhost:8080/GetDepartamentsCount/" + parts[0]);
                if(dep != "No!" && dep != null && dep != "0")
                {
                    for(int i = 0; i < Convert.ToInt32(dep); i++)
                    {
                        var res = Helper.Http.GetRequest("http://localhost:8080/GetDepartment/" + parts[0] + "/" + i.ToString());
                        var control = new DepartamentControl();
                        control.depName.Text = res.ToString();
                        control.Margin = new Thickness(0, 5, 0, 5);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);

                        Departaments.Items.Add(control);
                        Departaments.UpdateLayout();
                    }


                } else
                {
                    Departaments.Items.Add(new Label { Content = TryFindResource("NoDeps"), FontSize = 15 });
                    Departaments.UpdateLayout();
                }

                if (Departaments.SelectedIndex != -1)
                {
                    EmpsComp.Children.Clear();
                    EmpsComp.UpdateLayout();
                    int i = Departaments.SelectedIndex;
                    var r = Helper.Http.GetRequest("http://localhost:8080/GetDepartmentInfoCount/" + parts[0] + "/" + i.ToString());

                    if (r != "No!" && r != "0" && r != null)
                    {
                        for (int k = 0; k < Convert.ToInt32(r); i++)
                        {
                            var res = Helper.Http.GetRequest("http://localhost:8080/GetDepartmentEmp/" + parts[0] + "/" + i.ToString()).Split('|');
                            var control = new EmpControl();
                            control.EmpName.Content = res[0];
                            control.Position.Content += " " + res[1];
                            control.Email.Content += " " + res[2];
                            control.EmpID.Content = res[3];
                            control.Margin = new Thickness(0, 5, 0, 5);
                            control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                            control.BorderThickness = new Thickness(2);

                            EmpsComp.Children.Add(control);
                            EmpsComp.UpdateLayout();

                        }
                    } else
                    {
                        EmpsComp.Children.Add(new Label { Content = TryFindResource("NoEmps"), FontSize = 15 });
                        EmpsComp.UpdateLayout();
                    }

                } else
                {
                    EmpsComp.Children.Clear();
                    EmpsComp.UpdateLayout();
                    var r = Helper.Http.GetRequest("http://localhost:8080/GetCompanyEmpsCount/" + parts[0]);
                    if (r != "No!" && r != "0" && r != null)
                    {
                        for(int i = 0; i < Convert.ToInt32(r); i++)
                        {
                            var res = Helper.Http.GetRequest("http://localhost:8080/GetCompanyEmps/" + parts[0] + "/" + i.ToString()).Split('|');
                            var control = new EmpControl();
                            control.EmpName.Content = res[0];
                            control.Position.Content += " " + res[1];
                            control.Email.Content += " " + res[2];
                            control.EmpID.Content = res[3];
                            control.Margin = new Thickness(0, 5, 0, 5);
                            control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                            control.BorderThickness = new Thickness(2);

                            EmpsComp.Children.Add(control);
                            EmpsComp.UpdateLayout();
                        }
                    } else
                    {
                        EmpsComp.Children.Add(new Label { Content = TryFindResource("NoEmps"), FontSize = 15 });
                        EmpsComp.UpdateLayout();
                    }
                }

                var temp = Helper.Http.GetRequest("http://localhost:8080/GetCompanyProjCount/" + parts[0]);
                if (temp != "No!" && temp != "0" && temp != null)
                {
                    for (int i = 0; i < Convert.ToInt32(temp); i++)
                    {
                        var r = Helper.Http.GetRequest("http://localhost:8080/GetCompanyProj/" + parts[0] + "/" + i.ToString()).Split('|');
                        var control = new ProjectControll();
                        control.ProjectName.Content = r[0];
                        control.descript.Text = r[1];
                        control.Deadline.Content = Environment.NewLine + r[3];
                        control.ProjectID.Content = r[4];
                        control.Margin = new Thickness(5, 5, 5, 5);
                        control.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                        control.BorderThickness = new Thickness(2);
                        Projects.Children.Add(control);
                        Projects.UpdateLayout();
                    }


                } else
                {
                    Projects.Children.Add(new Label { Content = TryFindResource("NoneProj") });
                }

            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }


        private void BTNExit_Click(object sender, RoutedEventArgs e)
        {
            new UserPanel().Show();
            this.Close();
        }

        private void UploadLogo_Click(object sender, RoutedEventArgs e)
        {
            Images.UploadImageCompany();
            LogoImg.Source = Images.GetCompanyLogo();
        }

        private void AddDepartament_Click(object sender, RoutedEventArgs e)
        {
            new NewDep().Show();
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            new AddProject().Show();
            this.Close();
        }

        private void Newemp_Click(object sender, RoutedEventArgs e)
        {
            new EmployeeCodeGener().Show();
        }

        private void remcomp_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        private void rememp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
