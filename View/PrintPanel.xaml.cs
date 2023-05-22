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
    /// Логика взаимодействия для PrintPanel.xaml
    /// </summary>
    public partial class PrintPanel : Window
    {
        int id;
        public PrintPanel(int id)
        {
            InitializeComponent();
            this.id = id;
            student currentStudent = CuratorsHelperEntities.GetContext().student.SingleOrDefault(p => p.id_student == id);

            DataContext = currentStudent;

            Passport currentPassport = CuratorsHelperEntities.GetContext().Passport.SingleOrDefault(p => p.id_student == id);
            num_passport.Text = currentPassport.num_passport;
            person_issueText.Text = currentPassport.person_issue;
            DateTime date = new DateTime();
            date = (DateTime)currentPassport.date_issue;
            date_issueText.Text = date.Date.ToString("D");


            Hostel currentHostel = CuratorsHelperEntities.GetContext().Hostel.SingleOrDefault(p => p.id_student == id);
            hostelText.Text = currentHostel.room;

            WriteParent(); 
            WriteFamily();

           
        }

        private void WriteParent()
        {
            var currentParents = CuratorsHelperEntities.GetContext().Parents.ToList();

            currentParents = currentParents.Where(p => p.id_student == id).ToList();


            var Mother = currentParents.Where(p => p.parent == "Мать").ToList();
            if (Mother.Count != 0)
            {
                DateTime dateMother = new DateTime();
                if (Mother[0].date_bord != null)
                    dateMother = (DateTime)Mother[0].date_bord;
                MotherText.Text = Mother[0].parent + ":\n" + Mother[0].FIO + ", " + dateMother.Date.ToString("dd.MM.yyyy") + ",\n" + Mother[0].adres + ", " + Mother[0].phone + ", " + Mother[0].job_place + ", " + Mother[0].job;
            }

            var Father = currentParents.Where(p => p.parent == "Отец").ToList();
            if (Father.Count != 0)
            {
                DateTime dateFather = new DateTime();

                if (Father[0].date_bord != null)
                    dateFather = (DateTime)Father[0].date_bord;

                FatherText.Text = Father[0].parent + ":\n" + Father[0].FIO + ", " + dateFather.Date.ToString("dd.MM.yyyy") + ",\n" + Father[0].adres + ", " + Father[0].phone + ", " + Father[0].job_place + ", " + Father[0].job;
            }

            var Opekun = currentParents.Where(p => p.parent == "Опекун").ToList();
            if (Opekun.Count != 0)
            {
                DateTime dateOpekun = new DateTime();

                if (Opekun[0].date_bord != null)
                    dateOpekun = (DateTime)Opekun[0].date_bord;

                MotherText.Text = Opekun[0].parent + ":\n" + Opekun[0].FIO + ", " + dateOpekun.Date.ToString("dd.MM.yyyy") + ",\n" + Opekun[0].adres + ", " + Opekun[0].phone + ", " + Opekun[0].job_place + ", " + Opekun[0].job;
            }
        }

        private void WriteFamily()
        {
            var currentOtherFamily = CuratorsHelperEntities.GetContext().Brothers_sisters.ToList();
            currentOtherFamily = currentOtherFamily.Where(p => p.id_student == id).ToList();
            DateTime OtherDate = new DateTime();
            for (int i = 0; i < currentOtherFamily.Count; i++)
            {
                if (currentOtherFamily[i].date_born != null)
                    OtherDate = (DateTime)currentOtherFamily[i].date_born;
                if (currentOtherFamily[i].whois == "Брат")
                    BrotherText.Text += currentOtherFamily[i].whois + ":\n" + currentOtherFamily[i].FIO + ", " + OtherDate.Date.ToString("dd.MM.yyyy") + ", " + currentOtherFamily[i].status + "\n";
                if (currentOtherFamily[i].whois == "Сестра")
                    SisterText.Text += currentOtherFamily[i].whois + ":\n" + currentOtherFamily[i].FIO + ", " + OtherDate.Date.ToString("dd.MM.yyyy") + ", " + currentOtherFamily[i].status + "\n";
            }
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
            Window back = new MainMenuCurators();
            back.Show();
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

        private void docView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                ContextMenu contextMenu = new ContextMenu();
                MenuItem copyMenuItem = new MenuItem();
                copyMenuItem.Header = "Copy";
                copyMenuItem.Command = ApplicationCommands.Copy;
                contextMenu.Items.Add(copyMenuItem);
                contextMenu.IsOpen = true;
            }
        }
    }
}
