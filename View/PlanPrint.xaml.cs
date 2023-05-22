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
    /// Логика взаимодействия для PlanPrint.xaml
    /// </summary>
    public partial class PlanPrint : Window
    {
        public PlanPrint(int id)
        {
            InitializeComponent();


            var currentMounth = CuratorsHelperEntities.GetContext().Mounth.ToList();
            Mounth m = currentMounth.FirstOrDefault(p => p.id_mounth == id);
            var currentReport = CuratorsHelperEntities.GetContext().Plan_work.ToList();
            var curCurator = CuratorsHelperEntities.GetContext().Curators.ToList();
            Curators curPlan = curCurator.FirstOrDefault(p => p.id_pass == UserId.ID);
            currentReport = currentReport.Where(p => p.mounth == id && p.id_group == curPlan.id_group).ToList();

            currentReport = currentReport.OrderByDescending(p => p.id_type).ToList();

            ReportList.ItemsSource = currentReport;
            TitleText.Text += m.mounth1;
            if (currentReport != null)
                NumGroupText.Content += currentReport[0].Groups.name;
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
