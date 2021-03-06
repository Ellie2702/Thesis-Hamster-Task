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
    /// Логика взаимодействия для Emp.xaml
    /// </summary>
    public partial class Emp : Window
    {
        public Emp()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void CopyMail_Click(object sender, RoutedEventArgs e)
        {
            Global.userMail = EmpWork.Content.ToString();
        }
        
        private void EditPos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new SetPosition().Show();
            } catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }

        private void RemEmp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MessageBox.Show(TryFindResource("RemEmp").ToString()) == MessageBoxResult.OK)
                {
                    var res = Helper.Http.GetRequest("http://localhost:8080/RemEmp/" + parts[0] + "/" + Global.GlobEmpID);
                    if(res == "Ok!")
                    {
                        MessageBox.Show(TryFindResource("DelEmp").ToString());
                        this.Close();
                    } else
                    {
                        MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                    }
                }
            } catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/GetEmp/" + parts[0] + "/" + Global.GlobEmpID).Split('|');
                if (res[0] != null && res[0] != "No!")
                {
                    EmpName.Content += " " + res[0];
                    EmpPos.Content += " " + res[1];
                    EmpWork.Content += " " + res[2];
                    if (res[3] != "null")
                    {
                        Department.Content += " " + res[3];
                    } else
                    {
                        Department.Content += " " + TryFindResource("DepNotSet").ToString();
                    }
                }
                else
                {
                    MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                }
            } catch(Exception ex)
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }
    }
}
