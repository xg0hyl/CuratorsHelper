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
    /// Логика взаимодействия для Modal_Hobbies.xaml
    /// </summary>
    public partial class Modal_Hobbies : Window
    {
        bool IsClosing = false;
        int id;
        static Student_Hobbies newStudentHobby = new Student_Hobbies();
        public static Student_Hobbies GetHobby()
        {
            return newStudentHobby;
        }

        public Modal_Hobbies(int id)
        {
            InitializeComponent();

            this.id = id;

            var currentHobbies = CuratorsHelperEntities.GetContext().Hobbies.ToList();
            HobbiesCombo.ItemsSource = currentHobbies;
        }

        private void OkStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            IsClosing = true;

            if (HobbiesCombo.Text != "")
            {

                var currentHobbies = CuratorsHelperEntities.GetContext().Hobbies.ToList();

                newStudentHobby.id_student = id;
                currentHobbies = currentHobbies.Where(p => p.hobby == HobbiesCombo.Text).ToList();
                newStudentHobby.id_hobby = currentHobbies[0].id_hobby;

                this.DialogResult = true;

                
            }
            else MyMessage.Show("Пожалуйста, выберите увлечение");

            IsClosing = false;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            IsClosing = true;
            this.Close();
        }

        private void OkNewBtn_Click(object sender, RoutedEventArgs e)
        {
           /* IsClosing = true;

            if (NewHobbyText.Text != "")
            {
                string newHobby = NewHobbyText.Text.Substring(0, 1).ToUpper() + (NewHobbyText.Text.Length > 1 ? NewHobbyText.Text.Substring(1) : "");

                bool hand = false;

                var currentHobbies = CuratorsHelperEntities.GetContext().Hobbies.ToList();
                foreach (var item in currentHobbies)
                {
                    if (item.hobby == newHobby)
                    {
                        hand = true;
                        break;
                    }
                }

                if (!hand)
                {
                    Hobbies hobby = new Hobbies();
                    hobby.hobby = newHobby;
                    CuratorsHelperEntities.GetContext().Hobbies.Add(hobby);
                    try
                    {
                        CuratorsHelperEntities.GetContext().SaveChanges();
                        NewHobbyText.Clear();
                        var NewcurrentHobbies = CuratorsHelperEntities.GetContext().Hobbies.ToList();
                        HobbiesCombo.ItemsSource = NewcurrentHobbies;
                    }
                    catch
                    {
                        MyMessage.Show("Что-то пошло не так");
                    }
                }
                else MyMessage.Show("Такой тип увлечений уже существует");
            }
            else MyMessage.Show("Пожалуйста, введите новое увлечение");
          

            IsClosing = false;*/
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!IsClosing) Close();
        }


    }
}
