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
    /// Логика взаимодействия для Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        public Schedule()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void pastemail_Click(object sender, RoutedEventArgs e)
        {
            EmpMail.Text = Global.userMail;
        }

        private void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var r = Helper.Http.GetRequest("http://localhost:8080/RemDepartment/" + parts[0] + "/" + Global.GlobDepID);
                if(r == "Ok!")
                {
                    MessageBox.Show(TryFindResource("Save").ToString());
                } else
                {
                    MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }
    }
}
