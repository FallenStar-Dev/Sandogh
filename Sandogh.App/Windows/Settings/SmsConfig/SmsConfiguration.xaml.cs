using Sandogh.Bussiness.SendMessage;

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

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for SmsConfiguration.xaml
    /// </summary>
    public partial class SmsConfiguration : Window
    {
        public SmsConfiguration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendSms sms = new SendSms("ebb92cc757ac922c5af2cc8", "Sepehr007");
            sms.Send("09160330976", "تست");
        }
    }
}
