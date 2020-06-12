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
    /// Логика взаимодействия для Company.xaml
    /// </summary>
    public partial class Company : Window
    {
        public Company()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                var task = Helper.Http.GetRequest("http://localhost:8080/GetDepartaments/" + parts[0]);

            }
            catch
            {

            }
        }
    }
}
