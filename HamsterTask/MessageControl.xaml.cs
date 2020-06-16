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
    /// Логика взаимодействия для MessageControl.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {
        public MessageControl()
        {
            InitializeComponent();
            Global.LanguageSwitchControll(this);
        }

        string[] parts = Global.Guid.Split('|');
        private void ViewMessage_Click(object sender, RoutedEventArgs e)
        {
            Global.GlobMessID = MessageID.Content.ToString();
            if(Check.Content == "false")
            {
                var a = Helper.Http.GetRequest("http://localhost:8080/MessageCheck/" + parts[0] + "/" + Global.GlobMessID);
            }
            new Message().Show();
        }
    }
}
