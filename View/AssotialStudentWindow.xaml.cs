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
    /// Логика взаимодействия для AssotialStudentWindow.xaml
    /// </summary>
    public partial class AssotialStudentWindow : Window
    {
        int id; bool hand = false;
        Facts_assotial assotial = new Facts_assotial();
        public AssotialStudentWindow()
        {
            InitializeComponent();
        }

        public AssotialStudentWindow(int id)
        {
            InitializeComponent();

            this.id = id;
            UpdateAssotial();
        }

        private void UpdateAssotial()
        {
            var currentAssotial = CuratorsHelperEntities.GetContext().Facts_assotial.ToList();
            currentAssotial = currentAssotial.Where(p => p.id_student == id).ToList();
            AssotialList.ItemsSource = currentAssotial;
        }

        private void collapseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = Tag as Window;
            if (parentWindow != null)
            {
                parentWindow.Visibility = Visibility.Visible;
            }
            Close();
        }

        private void Ok_Btn_Click(object sender, RoutedEventArgs e)
        {
            assotial.date = Date_Text.SelectedDate;
            assotial.id_student = id;
            assotial.measures = Measures_text.Text;
            assotial.result = Result_Text.Text;
            assotial.character = Character_Text.Text;

            if (!hand) CuratorsHelperEntities.GetContext().Facts_assotial.Add(assotial);

            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();
                Measures_text.Clear();
                Date_Text.SelectedDate = null;
                Result_Text.Clear();
                Character_Text.Clear();
                hand = false;
                UpdateAssotial();
            }
            catch
            {
                MyMessage.Show("Что-то пошло не так");
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            hand = true;
            assotial = (Facts_assotial)AssotialList.SelectedItem;
            Measures_text.Text = assotial.measures;
            Result_Text.Text = assotial.result;
            Character_Text.Text = assotial.character;
            Date_Text.SelectedDate = assotial.date;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Facts_assotial facts_Assotial = (Facts_assotial)AssotialList.SelectedItem;
            CuratorsHelperEntities.GetContext().Facts_assotial.Remove(facts_Assotial);
            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();
                UpdateAssotial();
            }
            catch
            {
                MyMessage.Show("Что-то пошло не так");
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Window print = new AssotialPrint((Facts_assotial)AssotialList.SelectedItem, id);
            print.Show();
            Window win = GetWindow(this);
            print.Tag = win;
            this.Visibility = Visibility.Hidden;
        }
    }
}
