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
    /// Логика взаимодействия для RemoveUser.xaml
    /// </summary>
    public partial class RemoveUser : Window
    {
        public RemoveUser()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/RemUser/" + mail.Text);
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }
    }
}
