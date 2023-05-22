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
    /// Логика взаимодействия для StudentWorkPrint.xaml
    /// </summary>
    public partial class StudentWorkPrint : Window
    {
        int id;
        public StudentWorkPrint(bool hand, int id)
        {
            InitializeComponent();
            this.id = id;
            var currentWork2 = CuratorsHelperEntities.GetContext().Works_student.ToList();
            currentWork2 = currentWork2.Where(p => p.id_student == id).ToList();
            PersonalList2.ItemsSource = currentWork2;

            var curSt = CuratorsHelperEntities.GetContext().student.ToList();
            student st = curSt.FirstOrDefault(p => p.id_student == id);

            if (hand)
            {
                TitleText.Text += " учащимся " + st.FIO;
                UpdateStudentWork();

            }
            else
            {
                TitleText.Text += " родителеми учащегося " + st.FIO;
                UpdateParentsWork();
            }
        }

        private void UpdateStudentWork()
        {
            var currentWork = CuratorsHelperEntities.GetContext().Works_student.ToList();
            currentWork = currentWork.Where(p => p.id_student == id).ToList();
            PersonalList2.ItemsSource = currentWork;
        }

        private void UpdateParentsWork()
        {
            var currentWork = CuratorsHelperEntities.GetContext().Works_parent.ToList();
            currentWork = currentWork.Where(p => p.id_student == id).ToList();
            PersonalList2.ItemsSource = currentWork;
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
