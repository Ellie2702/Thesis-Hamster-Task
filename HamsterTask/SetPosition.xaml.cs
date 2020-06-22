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
    /// Логика взаимодействия для SetPosition.xaml
    /// </summary>
    public partial class SetPosition : Window
    {
        public SetPosition()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }
        string[] parts = Global.Guid.Split('|');
        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/SetEmpPos/" + parts[0] + "/" + Global.GlobEmpID + "/" + posName.Text);
                if (res == "Ok!")
                {
                    MessageBox.Show(TryFindResource("PosisSeе").ToString());
                } else
                {
                    MessageBox.Show(TryFindResource("PosNotSet").ToString());
                }
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
            }
        }
    }
}
