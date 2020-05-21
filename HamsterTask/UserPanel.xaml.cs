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
    /// Логика взаимодействия для UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        public UserPanel()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Global.Guid);
            string[] parts = Global.Guid.Split('|');
            UserName.Content = parts[3] + " " + parts[4];
            if(parts[parts.Length - 1] == "W")
            {
                Position.Content = parts[7];
                Company.Content = parts[8];
            } else if(parts[parts.Length - 1] == "WP")
            {
                
                Position.Content = parts[8];
                Company.Content = parts[9];
            }
                Birth.Content = parts[5];
                Email.Content = parts[6];
            
            if(parts[parts.Length - 1] != "W" || parts[parts.Length - 1] != "WP")
            {
                UserCompany.Visibility = Visibility.Hidden;
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Global.Guid = String.Empty;
            new MainWindow().Show();
            this.Close();
        }

        private void UserTask_Click(object sender, RoutedEventArgs e)
        {
            new Tasks().Show();
            this.Close();
        }
    }
}
