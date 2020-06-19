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
    /// Логика взаимодействия для NewDep.xaml
    /// </summary>
    public partial class NewDep : Window
    {
        public NewDep()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(DepName.Text != null)
                {
                    var res = Helper.Http.GetRequest("http://localhost:8080/AddDepartament/" + parts[0] + "/" +  DepName.Text);
                    if(res != "Ok!")
                    {
                        MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                        this.Close();
                    } else
                    {
                        MessageBox.Show(TryFindResource("DepAdded").ToString());
                    }
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
    }
}
