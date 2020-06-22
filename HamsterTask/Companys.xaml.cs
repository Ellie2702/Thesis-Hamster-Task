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
    /// Логика взаимодействия для Companys.xaml
    /// </summary>
    public partial class Companys : Window
    {
        public Companys()
        {
            InitializeComponent();
            Global.LanguageSwitch(this);
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/RemCompany/" + posName.Text);
            }
            catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = Helper.Http.GetRequest("http://localhost:8080/GetCompanysCount/");
                if(res != null && res != "No!")
                {
                    for(int i = 0; i < Convert.ToInt32(res); i++)
                    {
                        var r = Helper.Http.GetRequest("http://localhost:8080/GetCompanys/" + i.ToString()).Split('|');
                        if(r[0] != null && r[0] != "No!")
                        {
                            comp.Items.Add(new Label { Content = r[1] + "|" + r[0] + "-" + "(" + r[2] + ")" + TryFindResource("empCount").ToString() });
                        }
                    }
                }
            } catch
            {
                MessageBox.Show(TryFindResource("SomethingBroke").ToString());
                this.Close();
            }
        }
    }
}
