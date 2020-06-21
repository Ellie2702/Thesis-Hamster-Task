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
    /// Логика взаимодействия для CompanyCode.xaml
    /// </summary>
    public partial class CompanyCode : Window
    {
        public CompanyCode()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void ActBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/CreateProject/" + parts[0] + "/" + Code.Text);
                if(res == "Set a position")
                {
                    MessageBox.Show(TryFindResource("ActivCodeDone").ToString());
                } else
                {
                    MessageBox.Show(TryFindResource("ActivCodeNotDone").ToString());
                }
            } catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            new UserPanel().Show();
            this.Close();
        }
    }
}
