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
    /// Логика взаимодействия для RegistrationUser.xaml
    /// </summary>
    public partial class RegistrationUser : Window
    {
        public RegistrationUser()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void RegistrationUserBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RegPhone.Text == string.Empty)
                {
                    
                        Global.Guid = Helper.Http.GetRequest("http://localhost:8080/reguserA/" + RegLogin.Text + "/" + RegPass.Password + "/" + RegName.Text + "/" + RegSurName.Text + "/" + RegMail.Text + "/" + RegBirth.SelectedDate);
                        new UserPanel().Show();
                        this.Close();
                }
                else
                {
                   
                        Global.Guid = Helper.Http.GetRequest("http://localhost:8080/reguserFull/" + RegLogin.Text + "/" + RegPass.Password + "/" + RegName.Text + "/" + RegSurName.Text + "/" + RegMail.Text + "/" + RegPhone.Text + "/" + RegBirth.SelectedDate);
                        new UserPanel().Show();
                        this.Close();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BTNBackToMain_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
