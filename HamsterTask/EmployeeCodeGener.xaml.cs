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
    /// Логика взаимодействия для EmployeeCodeGener.xaml
    /// </summary>
    public partial class EmployeeCodeGener : Window
    {
        public EmployeeCodeGener()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Mail.Text != null)
                {
                    var res = Helper.Http.GetRequest("http://localhost:8080/GenerateEmployeeCode/" + parts[0] + "/" + Mail.Text);
                    if (res == "Code is created")
                    {
                        MessageBox.Show(TryFindResource("CodeisAdded").ToString());
                    } else
                    {
                        MessageBox.Show(TryFindResource("CodeNotAdded").ToString());
                    }
                } else MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }
    }
}
