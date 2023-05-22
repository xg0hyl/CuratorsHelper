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
using System.IO;
using System.Threading;
using System.Drawing;
using System.Windows.Interop;
using System.Diagnostics;

namespace CuratorsHelper
{
    /// <summary>
    /// Логика взаимодействия для MainMenuCurators.xaml
    /// </summary>
    /// 
    public partial class MainMenuCurators : Window
    {
        bool profClick = false;
        bool HandHome = false; bool HandCloud = false; bool HandGroups = false; bool HandProfile = false;
        byte[] byteImg;
        List<WebSites> listSites;

        private Bitmap GreyImg(System.Drawing.Image Image, float num)
        {
            Bitmap input = new Bitmap(Image);
            Bitmap output = new Bitmap(input.Width, input.Height);
            for (int j = 0; j < input.Height; j++)
                for (int i = 0; i < input.Width; i++)
                {
                    UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                    float R = (float)((pixel & 0x00FF0000) >> 16); 
                    float G = (float)((pixel & 0x0000FF00) >> 8); 
                    float B = (float)(pixel & 0x000000FF); 
                    R = G = B = (R + G + B) / num;
                    UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                    output.SetPixel(i, j, System.Drawing.Color.FromArgb((int)newPixel));
                }
            return output;
        }

        public MainMenuCurators()
        {
            InitializeComponent();

            List<Curators> currentCurator = null;
            List<rukovodstvo> currentRuk = null;
            Curators Curator = new Curators();
            rukovodstvo ruk = new rukovodstvo();
            string[] name = null;
            if (UserId.Role == "Психолог")
            {
                currentRuk = CuratorsHelperEntities.GetContext().rukovodstvo.ToList();
                ruk = currentRuk.Single(p => p.id_pass == UserId.ID);
                DataContext = ruk;
                name = ruk.FIO.Split(' ');
                byteImg = ruk.photo;
            }
            if (UserId.Role == "Соц. педагог")
            {
                currentRuk = CuratorsHelperEntities.GetContext().rukovodstvo.ToList();
                ruk = currentRuk.Single(p => p.id_pass == UserId.ID);
                DataContext = ruk;
                name = ruk.FIO.Split(' ');
                byteImg = ruk.photo;
            }
            if (UserId.Role == "Зам. по восп. работе")
            {
                currentRuk = CuratorsHelperEntities.GetContext().rukovodstvo.ToList();
                ruk = currentRuk.Single(p => p.id_pass == UserId.ID);
                DataContext = ruk;
                name = ruk.FIO.Split(' ');
                byteImg = ruk.photo;
            }
            if (UserId.Role == null)
            {
                currentCurator = CuratorsHelperEntities.GetContext().Curators.ToList();
                Curator = currentCurator.Single(p => p.id_pass == UserId.ID);
                DataContext = Curator;
                name = Curator.FIO.Split(' ');
                byteImg = Curator.photo;
            }

            ProfileText.Text = name[1];

            if (byteImg != null)
            {
                MemoryStream ms = new MemoryStream(byteImg);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                Profile_ellipse.Source = Imaging.CreateBitmapSourceFromHBitmap(GreyImg(img, 3).GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            
            listSites = CuratorsHelperEntities.GetContext().WebSites.ToList();
            listSites = listSites.Where(p => p.id_curator == Curator.id_curator).ToList();
            WebSitesList.ItemsSource = listSites;


            HomeStack.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            textHome.Foreground = System.Windows.Media.Brushes.White;
            HomeImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/bi_house-fill_WHITE.png", UriKind.Relative));

            HandHome = true;
            GroupsBack();
            CloudBack();
            ProfileBack();
           /* mainFrame.Content = new ViberPage();*/


            var page = new ViberPage();
            page.SitesUpdated += Page_SitesUpdated;
            mainFrame.Content = page;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitStack_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            exitStack.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));


                using (StreamWriter writer = new StreamWriter("rememberMe.txt", false))
                {
                    writer.WriteLine("");
                }
                UserId.Role = null;
                Window back = new MainWindow();
                back.Show();
                this.Close();
        }

        private void HomeBack()
        {
            HandHome = false;
            HomeStack.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(205,205,205));
            textHome.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            HomeImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/bi_house-fill.png", UriKind.Relative));
        }

        private void GroupsBack()
        {
            HandGroups = false;
            GroupsStack.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(205, 205, 205));
            GroupsText.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            GroupsImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/groups.png", UriKind.Relative));
        }

        private void CloudBack()
        {
            HandCloud = false;
            CloudStack.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(205, 205, 205));
            CloudText.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            CloudImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/cloud.png", UriKind.Relative));
        }

        private void ProfileBack()
        {
            HandProfile = false;
            MyProfile.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(205, 205, 205));
            ProfileText.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            BorderIMG.BorderThickness = new Thickness(0);
            profClick = false;
        }

        private void HomeStack_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            HomeStack.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            textHome.Foreground = System.Windows.Media.Brushes.White;
            HomeImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/bi_house-fill_WHITE.png", UriKind.Relative));

            HandHome = true;
            GroupsBack();
            CloudBack();
            ProfileBack();

            var page = new ViberPage();
            page.SitesUpdated += Page_SitesUpdated;
            mainFrame.Content = page;
        }

        private void GroupsStack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GroupsStack.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            GroupsText.Foreground = System.Windows.Media.Brushes.White;
            GroupsImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/groups_WHITE.png", UriKind.Relative));

            if (UserId.Role == "Психолог") mainFrame.Content = new viewOther.GroupsPsyho();
            if (UserId.Role == null) mainFrame.Content = new GroupsPage();

            HandGroups = true;
            HomeBack();
            CloudBack();
            ProfileBack();
        }

        private void CloudStack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloudStack.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            CloudText.Foreground = System.Windows.Media.Brushes.White;
            CloudImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/cloud_WHITE.png", UriKind.Relative));

            HandCloud = true;
            HomeBack();
            GroupsBack();
            ProfileBack();

            mainFrame.Content = new CloudPage();
        }

        private void ProfileStack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyProfile.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            ProfileText.Foreground = System.Windows.Media.Brushes.White;

            HandProfile = true;
            CloudBack();
            HomeBack();
            GroupsBack();

            mainFrame.Content = new ProfilePage();

            profClick = true;
        }

        private void collapseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Profile_ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!profClick)
            {
                /*                BorderIMG.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(205, 205, 205));*/
                BorderIMG.BorderThickness = new Thickness(2);

            }

        }

        private void Profile_ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!profClick) BorderIMG.BorderThickness = new Thickness(0);
        }

        private void HomeImg_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!HandHome) HomeImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/bi_house-fill_MOUSE.png", UriKind.Relative));
        }

        private void HomeImg_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!HandHome) HomeImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/bi_house-fill.png", UriKind.Relative));
        }

        private void GroupsImg_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!HandGroups) GroupsImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/groups_MOUSE.png", UriKind.Relative));
        }

        private void GroupsImg_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!HandGroups) GroupsImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/groups.png", UriKind.Relative));
        }

        private void CloudImg_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!HandCloud) CloudImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/cloud_MOUSE.png", UriKind.Relative));
        }

        private void CloudImg_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!HandCloud) CloudImg.Source = new BitmapImage(new Uri("/CuratorsHelper;component/images/cloud.png", UriKind.Relative));
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://uoggmk.by") { UseShellExecute = true });
        }

        private void Page_SitesUpdated(object sender, EventArgs e)
        {
            listSites = CuratorsHelperEntities.GetContext().WebSites.ToList();
            listSites = listSites.Where(p => p.Curators.id_pass == UserId.ID).ToList();
            WebSitesList.ItemsSource = listSites;
        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                WebSites site = (WebSites)WebSitesList.SelectedItem;
                Process.Start(new ProcessStartInfo(site.url) { UseShellExecute = true });
            }
            catch (Exception)
            {
                MyMessage.Show("Ссылка на сайт неверна или устарела");
            }
            

        }
    }
}
