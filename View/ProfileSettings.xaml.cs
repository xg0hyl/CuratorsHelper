using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CuratorsHelper.View
{
    /// <summary>
    /// Логика взаимодействия для ProfileSettings.xaml
    /// </summary>
    public partial class ProfileSettings : Window
    {

        Curators curator = new Curators();
        byte[] img;
        DispatcherTimer timer;
        bool cursor = false;


        public ProfileSettings()
        {
            InitializeComponent();
            var currentCurator = CuratorsHelperEntities.GetContext().Curators.ToList();
            curator = currentCurator.Single(p => p.id_pass == UserId.ID);
            DataContext = curator;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += Timer_Tick;
        }

        private void GroupText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            MaskPhone.Preview(GroupText, e);
        }

        private void GroupText_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaskPhone.Changed(GroupText, e);
        }

  
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (cursor)
            {
                ImgBtn.Opacity += 0.1;
                if (ImgBtn.Opacity >= 0.8)
                {
                    timer.Stop();
                }
            }
            else
            {
                ImgBtn.Opacity -= 0.1;
                if (ImgBtn.Opacity <= 0)
                {
                    timer.Stop();
                }
            }
        }

        private void CuratorImg_MouseEnter(object sender, MouseEventArgs e)
        {
            timer.Start();
            cursor = true;
        }

        private void CuratorImg_MouseLeave(object sender, MouseEventArgs e)
        {
            timer.Start();
            cursor = false;
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

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            Window dialog = new ModalSettings();
            bool hand = false;
            if (!string.IsNullOrEmpty(EmailText.Text) || !string.IsNullOrEmpty(DateText.Text) || !string.IsNullOrEmpty(GroupText.Text) || !string.IsNullOrEmpty(PassText.Text) || img != null)
            {
 
                if (dialog.ShowDialog() == true)
                {
                    if (!string.IsNullOrEmpty(EmailText.Text))
                        if (!string.IsNullOrEmpty(EmailText.Text))
                        {
                            string email = EmailText.Text;
                            if (Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$"))
                            {
                                curator.email = email;
                            }
                            else
                            {
                                // Введенный email не соответствует шаблону
                                MyMessage.Show("Email введен неверно");
                                hand = true;
                                EmailText.Clear();
                            }
                        }

                    if (DateText.SelectedDate != null)
                        curator.date_born = DateText.SelectedDate;

                    if (!string.IsNullOrEmpty(GroupText.Text))
                        curator.phone = GroupText.Text;

                    if (!string.IsNullOrEmpty(PassText.Text))
                        curator.Passwords1.password = ArgonClass.HashPassword(PassText.Text, curator.Passwords1.salt);

                    if (img != null)
                        curator.photo = img;

                    if (!hand)
                    {

                        try
                        {
                            CuratorsHelperEntities.GetContext().SaveChanges();
                            Window parentWindow = Tag as Window;
                            if (parentWindow != null)
                            {
                                parentWindow.Visibility = Visibility.Visible;
                            }
                            Close();
                        }
                        catch (Exception msg)
                        {
                            MyMessage.Show("Что-то пошло не так");
                        }
                    }

                }
            }
            else
            {
                Window parentWindow = Tag as Window;
                if (parentWindow != null)
                {
                    parentWindow.Visibility = Visibility.Visible;
                }
                Close();
            }
        }

        private void CuratorImg_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ImgBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
               "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
            {
                img = File.ReadAllBytes(ofdPicture.FileName);
                BitmapImage image = new BitmapImage(new Uri(ofdPicture.FileName, UriKind.Relative));
                CuratorImg.Fill = new ImageBrush(image);
            }
        }


    }
}
