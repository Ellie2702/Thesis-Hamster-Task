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
            Global.LanguageSwitch(this);
        }

        private void BTNBackToMain_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void BTNlLoginReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Global.Guid = Helper.Http.GetRequest("http://localhost:8080/RegOrgU/" + Global.RegOrgInfo + Login.Text + "/" + Pass.Password + "/" + Position.Text + "/");
                if (Global.Guid != "No!" || Global.Guid != "WrongKeys")
                {
                    new UserPanel().Show();
                    this.Close();
                }
                else MessageBox.Show(TryFindResource("WrongData").ToString());
            }
            catch
            {
                MessageBox.Show(TryFindResource("WrongData").ToString());
            }
        }
    }
}
