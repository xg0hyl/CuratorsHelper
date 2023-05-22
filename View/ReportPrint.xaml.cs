using System;
using System.Collections.Generic;
using System.IO;
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

namespace CuratorsHelper.View
{
    /// <summary>
    /// Логика взаимодействия для ReportPrint.xaml
    /// </summary>
    public partial class ReportPrint : Window
    {
        public ReportPrint(int id)
        {
            InitializeComponent();

            var currentMounth = CuratorsHelperEntities.GetContext().Mounth.ToList();
            Mounth m = currentMounth.FirstOrDefault(p => p.id_mounth == id);
            var currentReport = CuratorsHelperEntities.GetContext().Report.ToList();
            currentReport = currentReport.Where(p => p.mounth == id).ToList();
            ReportList.ItemsSource = currentReport;

            ReportList.ItemsSource = currentReport;
            if (currentReport != null)
                NumGroupText.Text += currentReport[0].Groups.name;
            TitleText.Text += m.mounth1;
        }

        private void collapseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window win = Tag as Window;
            win.Visibility = Visibility.Visible;
            this.Close();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("rememberMe.txt", false))
            {
                writer.WriteLine("");
            }
            UserId.Role = null;
            Window back = new MainWindow();
            back.Show();
            this.Close();
        }
    }
}
