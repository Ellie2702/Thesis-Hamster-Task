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
    /// Логика взаимодействия для AddAdmin.xaml
    /// </summary>
    public partial class AddAdmin : Window
    {
        public AddAdmin()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void pastemail_Click(object sender, RoutedEventArgs e)
        {
            posName.Text = Global.userMail;
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/newAdmin/" + posName.Text);
                if(res == "Ok!")
                {
                    MessageBox.Show(TryFindResource("newAd").ToString());
                }
            } catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }
    }
}
