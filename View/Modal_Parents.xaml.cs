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

namespace CuratorsHelper.View
{
    /// <summary>
    /// Логика взаимодействия для Modal_Parents.xaml
    /// </summary>
    public partial class Modal_Parents : Window
    {
        bool IsClosing = false;
        static Parents par = new Parents();
        string parnt;
        int id;
        static string HandDo;
        List<Parents> listPar;
        public static Parents GetParent()
        {
            return par;
        }

        public static string GetHand()
        {
            return HandDo;
        }

        public Modal_Parents(int id, string parnt)
        {
            InitializeComponent();

            this.id = id;
            this.parnt = parnt;

            listPar = CuratorsHelperEntities.GetContext().Parents.ToList();
            listPar = listPar.Where(p => p.id_student == id).ToList();
            if (listPar.Count == 0)
                ChangeBtn.Visibility = Visibility.Hidden;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            IsClosing = true;
            this.Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isnull = false;
            foreach (var item in GridParen.Children)
            {
                if (item is TextBox)
                {
                    if (string.IsNullOrEmpty((item as TextBox).Text))
                    {
                        isnull = true;
                        (item as TextBox).BorderBrush = Brushes.Red;
                    }
                    else
                    {
                        (item as TextBox).BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222"));
                    }
                }
            }
            if (Date_text.SelectedDate == null)
            {
                isnull = true;
                Date_text.BorderBrush = Brushes.Red;
            }
            else Date_text.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222"));


            if (!isnull)
            {
                IsClosing = true;

                par.id_student = id;
                par.parent = ParentText.Text;
                par.FIO = FIO_text.Text;
                par.adres = Adres_text.Text;
                par.phone = Phone_text.Text;
                par.job = Job_text.Text;
                par.job_place = Place_text.Text;
                par.date_bord = Date_text.SelectedDate;
                this.DialogResult = true;
            }

        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!IsClosing) Close();
        }

        private void Phone_text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            MaskPhone.Preview(Phone_text, e);
        }

        private void Phone_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaskPhone.Changed(Phone_text, e);

        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            HandDo = "Change";
            var currentParent = CuratorsHelperEntities.GetContext().Parents.ToList();
            currentParent = currentParent.Where(p => p.id_student == id && (p.parent == parnt || p.parent == "Опекун")).ToList();
            DataContext = currentParent[0];
            par = currentParent[0];
            WhatDo.IsOpen = false;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            HandDo = "Add";
            HoDo.IsOpen = true;
            WhatDo.IsOpen = false;
            int countOk = 0;
            foreach (var item in listPar)
            {
                if (item.parent == "Мать")
                {
                    MotherBtn.Visibility = Visibility.Hidden;
                    OtchimBtn.Visibility = Visibility.Hidden;
                    countOk++;
                }
                else if (item.parent == "Отец")
                {
                    FatherBtn.Visibility = Visibility.Hidden;
                    OtchimBtn.Visibility = Visibility.Hidden;
                    countOk++;
                }
                else if (item.parent == "Опекун")
                {
                    MotherBtn.Visibility = Visibility.Hidden;
                    FatherBtn.Visibility = Visibility.Hidden;
                    WarrningParText.Visibility = Visibility.Visible;
                    OtchimBtn.Visibility = Visibility.Hidden;
                }
            }
            if (countOk == 2)
            {
                WarrningParText.Visibility = Visibility.Visible;
            }
        }

        private void MotherBtn_Click(object sender, RoutedEventArgs e)
        {
            HoDo.IsOpen = false;
            ParentText.Text = "Мать";
        }

        private void FatherBtn_Click(object sender, RoutedEventArgs e)
        {
            HoDo.IsOpen = false;
            ParentText.Text = "Отец";
        }

        private void OtchimBtn_Click(object sender, RoutedEventArgs e)
        {
            HoDo.IsOpen = false;
            ParentText.Text = "Опекун";
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                HandDo = "Del";
                par.id_student = id;
                par.parent = ParentText.Text;
                par.FIO = FIO_text.Text;
                par.adres = Adres_text.Text;
                par.phone = Phone_text.Text;
                par.job = Job_text.Text;
                par.job_place = Place_text.Text;
                par.date_bord = Date_text.SelectedDate;

                IsClosing = true;
                DialogResult = true;
            }
            catch (Exception msg)
            {
                MyMessage.Show("Что-то пошло не так");
            }
        }
    }
}
