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
    /// Логика взаимодействия для Modal_OtherFamily.xaml
    /// </summary>
    public partial class Modal_OtherFamily : Window
    {
        List<string> type = new List<string>();
        bool IsClosing = false;
        int id;
        static string HandDo;
        List<Brothers_sisters> listPar;
        static Brothers_sisters family = new Brothers_sisters();

        public static string GetHand()
        {
            return HandDo;
        }

        public static Brothers_sisters GetFamily()
        {
            return family;
        }
        public Modal_OtherFamily(int id)
        {
            InitializeComponent();


            this.id = id;

            type.Add("Учится");
            type.Add("Работает");

            TypeCombo.ItemsSource = type;

            listPar = CuratorsHelperEntities.GetContext().Brothers_sisters.ToList();
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
            if (!string.IsNullOrEmpty(FIO_text.Text) && TypeCombo.SelectedIndex != -1 && Date_text.SelectedDate != null)
            {
                WarningNull.Visibility = Visibility.Hidden;
                IsClosing = true;
                if (HandDo == "Change")
                {
                    family = (Brothers_sisters)DataContext;
                    family.status = TypeCombo.SelectedItem.ToString();
                    family.date_born = Date_text.SelectedDate;
                }
                else if (OtherCombo.SelectedIndex != 0)
                {
                    family.id_student = id;
                    family.whois = OtherCombo.Text;
                    family.FIO = FIO_text.Text;
                    family.status = TypeCombo.SelectedItem.ToString();
                    family.date_born = Date_text.SelectedDate;
                }
                this.DialogResult = true;
            }
            else
            {
                WarningNull.Visibility = Visibility.Visible;
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!IsClosing) Close();
        }

        private void ParentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HandDo == "Change")
            {
                if (ParentCombo.SelectedIndex != 0)
                {
                    List<Brothers_sisters> currentFamily = CuratorsHelperEntities.GetContext().Brothers_sisters.ToList();
                    currentFamily = currentFamily.Where(p => p.id_student == id).ToList();

                    currentFamily = currentFamily.Where(p => p.FIO == ParentCombo.SelectedValue.ToString()).ToList();
                    DataContext = currentFamily[0];
                    TypeCombo.SelectedItem = currentFamily[0].status;
                }
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            HandDo = "Change";
            WhatDo.IsOpen = false;
            List<Brothers_sisters> currentFamily = CuratorsHelperEntities.GetContext().Brothers_sisters.ToList();
            currentFamily = currentFamily.Where(p => p.id_student == id).ToList();
            currentFamily.Insert(0, new Brothers_sisters
            {
                FIO = "Выберите родственника"
            });
            ParentCombo.ItemsSource = currentFamily;
            ParentCombo.SelectedIndex = 0;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            HandDo = "Add";
            WhatDo.IsOpen = false;
            List<string> otherList = new List<string> {"Выберите родственника", "Брат", "Сестра" };
            OtherCombo.ItemsSource = otherList;
            OtherCombo.SelectedIndex = 0;
            OtherCombo.Visibility = Visibility.Visible;
            ParentCombo.Visibility = Visibility.Hidden;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HandDo = "Del";
            if (OtherCombo.SelectedIndex != 0)
            {
                IsClosing = true;
                family = (Brothers_sisters)DataContext;
                family.id_student = id;
                family.whois = OtherCombo.Text;
                family.FIO = FIO_text.Text;
                family.status = TypeCombo.SelectedItem.ToString();
                family.date_born = Date_text.SelectedDate;

                this.DialogResult = true;
            }
            else
            {
                WarningNull.Visibility = Visibility.Visible;
            }
        }
    }
}
