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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Global.Guid = string.Empty;
            new MainWindow().Show();
            this.Close();
        }

        private void Messages_Click(object sender, RoutedEventArgs e)
        {
            new UserMessage().Show();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            new RemoveUser().Show();
        }

        private void DeleteCompany_Click(object sender, RoutedEventArgs e)
        {
            new Companys().Show();
        }

        private void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            new AddAdmin().Show();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var parts = Global.Guid.Split('|');
                AdminName.Content += ", " + parts[3] + " " + parts[4];
            } catch
            {
                AdminName.Content += ", Admin";
            }

        }
    }
}
