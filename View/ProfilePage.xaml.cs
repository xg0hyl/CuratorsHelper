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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CuratorsHelper
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        DispatcherTimer timer;

        double gridHeight;
        bool hidden = false;

        Curators cur = new Curators();
        public ProfilePage()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;
            gridHeight = 80;

            var currentCurator = CuratorsHelperEntities.GetContext().Curators.ToList();
            cur = currentCurator.Single(p => p.id_pass == UserId.ID);
            DataContext = cur;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!hidden)
            {
                MoveGrid.Height += 1;
                if (MoveGrid.Height >= gridHeight)
                {
                    timer.Stop();
                    hidden = true;
                }
            }
            else
            {
                MoveGrid.Height -= 1;
                if (MoveGrid.Height <= 0)
                {
                    timer.Stop();
                    hidden = false;
                }
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Window settings = new View.ProfileSettings();
            Window parentWindow = Window.GetWindow(this);
            this.IsVisibleChanged += ProfilePage_IsVisibleChanged;
            parentWindow.Visibility = Visibility.Hidden;
            settings.Tag = parentWindow;
            settings.Show();
        }

        private void ProfilePage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DataContext = null;
            var currentCurator = CuratorsHelperEntities.GetContext().Curators.ToList();
            cur = currentCurator.Single(p => p.id_pass == UserId.ID);
            DataContext = cur;
        }
    }
}
