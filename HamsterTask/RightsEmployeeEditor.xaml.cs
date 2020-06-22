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
    /// Логика взаимодействия для RightsEmployeeEditor.xaml
    /// </summary>
    public partial class RightsEmployeeEditor : Window
    {
        public RightsEmployeeEditor()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }
        string[] parts = Global.Guid.Split('|');
        private void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/SetRights/" + parts[0] + "/" + EmpMail.Text + "/" + TaskA.IsChecked.ToString() + "/" + DepA.IsChecked.ToString() + "/" + ProjA.IsChecked.ToString() + "/" + CodeA.IsChecked.ToString() + "/" + SchA.IsChecked.ToString());
                if(res == "Ok!")
                {
                    MessageBox.Show(TryFindResource("Save").ToString());
                } else
                {
                    MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }

        private void BTNExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void pastemail_Click(object sender, RoutedEventArgs e)
        {
            EmpMail.Text = Global.userMail;
        }
    }
}
