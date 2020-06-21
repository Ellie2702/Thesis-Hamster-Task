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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HamsterTask
{
    /// <summary>
    /// Логика взаимодействия для ProjectControll.xaml
    /// </summary>
    public partial class ProjectControll : UserControl
    {
        public ProjectControll()
        {
            InitializeComponent();
        }

        private void ViewProj_Click(object sender, RoutedEventArgs e)
        {
            Global.GlobProjectID = ProjectID.Content.ToString();
            new ProjectForm().Show();
        }
    }
}
