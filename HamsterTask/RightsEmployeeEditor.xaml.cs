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
    /// Логика взаимодействия для RightsEmployeeEditor.xaml
    /// </summary>
    public partial class RightsEmployeeEditor : Window
    {
        public RightsEmployeeEditor()
        {
            InitializeComponent();
        }

        private void BTNSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pastemail_Click(object sender, RoutedEventArgs e)
        {
            EmpMail.Text = Global.userMail;
        }
    }
}
