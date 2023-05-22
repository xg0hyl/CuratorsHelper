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
    /// Логика взаимодействия для PersonalWorksWindow.xaml
    /// </summary>
    public partial class PersonalWorksWindow : Window
    {
        bool hand = false; int id;
        bool handler;

        Works_student works_Student = new Works_student();
        Works_parent works_Parent = new Works_parent();
        public PersonalWorksWindow()
        {
            InitializeComponent();
        }

        public PersonalWorksWindow(bool Handler, int id)
        {
            InitializeComponent();
            Date_Text.SelectedDate = null;
            handler = Handler;
            this.id = id;
            if (Handler)
            {
                MainText.Text += " учащимся";
                UpdateStudentWork();

            }
            else
            {
                MainText.Text += " родителеми";
                UpdateParentsWork();
            }
        }

        private void UpdateStudentWork()
        {
            var currentWork = CuratorsHelperEntities.GetContext().Works_student.ToList();
            currentWork = currentWork.Where(p => p.id_student == id).ToList();
            PersonalList.ItemsSource = currentWork;
        }

        private void UpdateParentsWork()
        {
            var currentWork = CuratorsHelperEntities.GetContext().Works_parent.ToList();
            currentWork = currentWork.Where(p => p.id_student == id).ToList();
            PersonalList.ItemsSource = currentWork;
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
            StringBuilder errors = new StringBuilder();
                if (handler)
                {
                    works_Student.date = Date_Text.SelectedDate;

                    works_Student.text_work = Work_Text.Text;

                    works_Student.result = Result_Text.Text;

                    works_Student.id_student = id;

                    if (!hand) CuratorsHelperEntities.GetContext().Works_student.Add(works_Student);
                }
                else
                { 

                    works_Parent.date = Date_Text.SelectedDate;

                    works_Parent.text_work = Work_Text.Text;

                    works_Parent.result = Result_Text.Text;

                    works_Parent.id_student = id;
                    if (!hand) CuratorsHelperEntities.GetContext().Works_parent.Add(works_Parent);
                }
            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();
                Date_Text.SelectedDate = null;
                Work_Text.Clear();
                Result_Text.Clear();
                DataContext = null;
                if (handler)
                {
                    UpdateStudentWork();
                }
                else UpdateParentsWork();
                hand = false; 
            }
            catch
            {
                MyMessage.Show("Что-то пошло не так");
            }

        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            hand = true;
            if (handler)
            {
                works_Student = (Works_student)PersonalList.SelectedItem;
                Work_Text.Text = works_Student.text_work;
                Result_Text.Text = works_Student.result;
                Date_Text.SelectedDate = works_Student.date;
            }
            else
            {
                works_Parent = (Works_parent)PersonalList.SelectedItem;
                Work_Text.Text = works_Parent.text_work;
                Result_Text.Text = works_Parent.result;
                Date_Text.SelectedDate = works_Parent.date;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (handler)
            {
                Works_student work = (Works_student)PersonalList.SelectedItem;
                CuratorsHelperEntities.GetContext().Works_student.Remove(work);
            }
            else
            {
                Works_parent work = (Works_parent)PersonalList.SelectedItem;
                CuratorsHelperEntities.GetContext().Works_parent.Remove(work);
            }

            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();
                if (handler) UpdateStudentWork();
                else UpdateParentsWork();
            }
            catch
            {
                MyMessage.Show("Что-то пошло не так");
            }

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Window print = new StudentWorkPrint(handler,id);
            print.Show();
            Window win = GetWindow(this);
            print.Tag = win;
            this.Visibility = Visibility.Hidden;
        }
    }
}
