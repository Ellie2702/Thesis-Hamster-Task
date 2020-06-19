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
    /// Логика взаимодействия для Emp.xaml
    /// </summary>
    public partial class Emp : Window
    {
        public Emp()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void CopyMail_Click(object sender, RoutedEventArgs e)
        {
            Global.userMail = EmpWork.Content.ToString();
        }

        private void EditPos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemEmp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
