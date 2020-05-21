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
    /// Логика взаимодействия для Tasks.xaml
    /// </summary>
    public partial class Tasks : Window
    {
        public Tasks()
        {
            InitializeComponent();
        }

        private void ProjectsHeader_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] parts = Global.Guid.Split('|');
                var data = Helper.Http.GetRequest("http://localhost:8080/GetProjects/" + parts[1]);
                if (data == null)
                {
                    NoneProj.Visibility = Visibility.Visible;
                }
                else
                {
                    NoneProj.Visibility = Visibility.Hidden;

                }


            }
            catch
            {

            }
        }
    }
}
