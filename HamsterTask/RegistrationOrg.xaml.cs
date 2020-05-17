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
    /// Логика взаимодействия для RegistrationOrg.xaml
    /// </summary>
    public partial class RegistrationOrg : Window
    {
        public RegistrationOrg()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void BtnContinueNoReg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnContinueReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            } catch
            {

            }
        }
    }
}
