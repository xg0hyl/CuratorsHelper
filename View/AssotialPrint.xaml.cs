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
    /// Логика взаимодействия для AssotialPrint.xaml
    /// </summary>
    public partial class AssotialPrint : Window
    {
        public AssotialPrint(Facts_assotial facts, int id)
        {
            InitializeComponent();

            var currentAssotial = CuratorsHelperEntities.GetContext().Facts_assotial.ToList();
            currentAssotial = currentAssotial.Where(p => p.id_student == id).ToList();
            AssotialList.ItemsSource = currentAssotial;

            var curSt = CuratorsHelperEntities.GetContext().student.ToList();
            student st = curSt.FirstOrDefault(p => p.id_student == id);

            TitleText.Text += st.FIO;
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
