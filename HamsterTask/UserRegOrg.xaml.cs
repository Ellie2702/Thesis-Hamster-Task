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
    /// Логика взаимодействия для UserRegOrg.xaml
    /// </summary>
    public partial class UserRegOrg : Window
    {
        public UserRegOrg()
        {
            InitializeComponent();
        }

        private void BTNBackToMain_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void BTNlLoginReg_Click(object sender, RoutedEventArgs e)
        {
            Global.Guid = Helper.Http.GetRequest("http://localhost:8080/RegOrgU/" + Global.RegOrgInfo  + Login.Text + "/" + Pass.Password + "/" + Position.Text + "/");
            new UserPanel().Show();
            this.Close();
        }
    }
}
