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
    /// Логика взаимодействия для EditDep.xaml
    /// </summary>
    public partial class EditDep : Window
    {
        public EditDep()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/EditDep/" + parts[0] + "/" + Global.GlobDepID + "/" + posName.Text);
                if(res == "Ok")
                {
                    MessageBox.Show(TryFindResource("SaveChanges").ToString());
                }
                else
                {
                    MessageBox.Show(TryFindResource("ChangesIsSaved").ToString());
                }
            } catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }
    }
}
