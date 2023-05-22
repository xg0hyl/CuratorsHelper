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
    /// Логика взаимодействия для AchivmentStudentWindow.xaml
    /// </summary>
    public partial class AchivmentStudentWindow : Window
    {
        int id; bool hand = false;
        Achivment_student achivment_Student = new Achivment_student();
        public AchivmentStudentWindow()
        {
            InitializeComponent();
        }

        public AchivmentStudentWindow(int id)
        {
            InitializeComponent();

            this.id = id;

            UpdateAchivment();
        }

        private void UpdateAchivment()
        {
            var currentAchiv = CuratorsHelperEntities.GetContext().Achivment_student.ToList();
            currentAchiv = currentAchiv.Where(p => p.id_student == id).ToList();
            PersonalList.ItemsSource = currentAchiv;
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
            Window parentWindow = Tag as Window;
            if (parentWindow != null)
            {
                parentWindow.Visibility = Visibility.Visible;
            }
            Close();
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

        private void Ok_Btn_Click(object sender, RoutedEventArgs e)
        {
            achivment_Student.date = Date_Text.SelectedDate;
            achivment_Student.id_student = id;
            achivment_Student.form = Result_Text.Text;
            achivment_Student.text_achivment = Work_Text.Text;

            if (!hand) CuratorsHelperEntities.GetContext().Achivment_student.Add(achivment_Student);

            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();
                Date_Text.SelectedDate = null;
                Work_Text.Clear();
                Result_Text.Clear();
                hand = false;
                UpdateAchivment();
            }
            catch
            {
                MyMessage.Show("Что-то пошло не так");
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            hand = true;

            achivment_Student = (Achivment_student)PersonalList.SelectedItem;
            Work_Text.Text = achivment_Student.text_achivment;
            Result_Text.Text = achivment_Student.form;
            Date_Text.SelectedDate = achivment_Student.date;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Achivment_student ach = (Achivment_student)PersonalList.SelectedItem;
            CuratorsHelperEntities.GetContext().Achivment_student.Remove(ach);
            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();
                UpdateAchivment();
            }
            catch
            {
                MyMessage.Show("Что-то пошло не так");
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Window print = new AchivmentPrint(id);
            print.Show();
            Window win = GetWindow(this);
            print.Tag = win;
            this.Visibility = Visibility.Hidden;
        }
    }
}
