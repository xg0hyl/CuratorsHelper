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

namespace CuratorsHelper
{
    /// <summary>
    /// Логика взаимодействия для MyMessage.xaml
    /// </summary>
    public partial class MyMessage : Window
    {
        public MyMessage()
        {
            InitializeComponent();
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public static void Show(string msg)
        {
            MyMessage mess = new MyMessage();
            mess.MsgText.Text = msg;
            mess.ShowDialog();
        }
    }
}
