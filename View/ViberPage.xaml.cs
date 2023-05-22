using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CuratorsHelper
{
    /// <summary>
    /// Логика взаимодействия для ViberPage.xaml
    /// </summary>
    public partial class ViberPage : Page
    {

        public event EventHandler SitesUpdated;
        public event EventHandler StudentUpdated;
        WindowCollection windows = Application.Current.Windows;
        OpenFileDialog ofdPicture = new OpenFileDialog();
        student st;
        Parents parent;
        Brothers_sisters otherFam;
        Hobbies hobby;
        Curators curator;
        Passport passport;
        student lastSt;

        public ViberPage()
        {
            InitializeComponent();
            var currentCurator = CuratorsHelperEntities.GetContext().Curators.ToList();
            Curators curPlan = currentCurator.FirstOrDefault(p => p.id_pass == UserId.ID);

            var currentGroup = CuratorsHelperEntities.GetContext().Groups.ToList();
            currentGroup = currentGroup.Where(p => p.name != "None").OrderBy(p => p.name).ToList();

            var currentUserGroup = currentGroup.FirstOrDefault(p => p.name == curPlan.Groups.name);
            currentGroup.Remove(currentUserGroup);

            currentGroup.Insert(0, new Groups
            {
                name = "Все группы"
            });

            if (currentUserGroup != null)
            {
                currentGroup.Insert(1, currentUserGroup);
            }

            GroupsCombo.ItemsSource = currentGroup;
            GroupsCombo.SelectedIndex = 0;
           
            var currentStudents = CuratorsHelperEntities.GetContext().student.ToList();
            StudentView.ItemsSource = currentStudents.Where(p => p.expelled != "Отчислен");

            try
            {
                var currentSites = CuratorsHelperEntities.GetContext().WebSites.ToList();
                currentSites = currentSites.Where(p => p.Curators.id_pass == UserId.ID).ToList();
                SitesList.ItemsSource = currentSites;
                StudentUpdated += ViberPage_StudentUpdated;
            }
            catch (Exception msg)
            {
                MyMessage.Show("Что-то пошло не так");
            }
           
            var moun = CuratorsHelperEntities.GetContext().Mounth.ToList();
            comboMounth.ItemsSource = moun;
            comboMounth.SelectedIndex = DateTime.Now.Month-1;

            

            NumGroupText.Content += curPlan.Groups.name;
        }



        private void ViberPage_StudentUpdated(object sender, EventArgs e)
        {
            ComboChanged();
        }

        private void studentsBtn_Click(object sender, RoutedEventArgs e)
        {
            StudentsGrid.Visibility = Visibility.Visible;
            Buttons.Visibility = Visibility.Hidden;
        }

        private void CuratorsBtn_Click(object sender, RoutedEventArgs e)
        {
            Buttons.Visibility = Visibility.Hidden;
            PlanGrid.Visibility = Visibility.Visible;
        }

        private void SitesBtn_Click(object sender, RoutedEventArgs e)
        {
            Buttons.Visibility = Visibility.Hidden;
            SitesGrid.Visibility = Visibility.Visible;
        }

  
        private void ComboChanged()
        {
            var currentStudents = CuratorsHelperEntities.GetContext().student.ToList();

            var listCurators = CuratorsHelperEntities.GetContext().Curators.ToList();
            curator = listCurators.FirstOrDefault(p => p.id_pass == UserId.ID);

            if (GroupsCombo.SelectedIndex != 0)
                currentStudents = currentStudents.Where(p => p.Groups.id_group == (int?)GroupsCombo.SelectedValue && p.expelled != "Отчислен").ToList();

            StudentView.ItemsSource = currentStudents;

            if (curator.id_group == (int?)GroupsCombo.SelectedValue)
            {
                AddStudent.Visibility = Visibility.Visible;
                curator.Groups.vis = Visibility.Visible;
            }
            else
            {
                AddStudent.Visibility = Visibility.Hidden;
                foreach (var item in currentStudents)
                {
                    item.Groups.vis = Visibility.Hidden;
                }
            }

        }

        private void GroupsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboChanged();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            student st = (sender as TextBlock).DataContext as student;
            Window stWin = new View.StudentsWindow(st);
            Window parentWindow = Window.GetWindow(this);
            View.StudentsWindow.StUpdated += OnStudentWindowClosed;
            parentWindow.Visibility = Visibility.Hidden;
            stWin.Tag = parentWindow;
            stWin.Show();
        }

        private void OnStudentWindowClosed(object sender, EventArgs e)
        {
            var save = GroupsCombo.SelectedValue;
            GroupsCombo.SelectedIndex = 0;
            GroupsCombo.SelectedValue = save;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StudentsGrid.Visibility = Visibility.Hidden;
            PlanGrid.Visibility = Visibility.Hidden;
            SitesGrid.Visibility = Visibility.Hidden;
            Buttons.Visibility = Visibility.Visible;
        }


        WebSites site = new WebSites();
        bool SiteHand;

        private void SitesAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SiteNameText.Text) && !string.IsNullOrEmpty(UrlText.Text))
            {
                site.website = SiteNameText.Text;
                site.url = UrlText.Text;
                Curators cur = CuratorsHelperEntities.GetContext().Curators.FirstOrDefault(p => p.id_pass == UserId.ID);
                site.id_curator = cur.id_curator;

                if (!SiteHand) CuratorsHelperEntities.GetContext().WebSites.Add(site);

                try
                {
                    CuratorsHelperEntities.GetContext().SaveChanges();
                    SiteNameText.Clear();
                    UrlText.Clear();
                    var currentSites = CuratorsHelperEntities.GetContext().WebSites.ToList();
                    currentSites = currentSites.Where(p => p.Curators.id_pass == UserId.ID).ToList();
                    SitesList.ItemsSource = currentSites;
                    SiteHand = false;

                    SitesUpdated?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception msg)
                {
                    MyMessage.Show("Что-то пошло не так");
                }
            }
            else
            {
                MyMessage.Show("Заполните все поля");
                SiteNameText.Clear();
                UrlText.Clear();
            }
            


        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WebSites selectedSite = (WebSites)SitesList.SelectedItem;
            CuratorsHelperEntities.GetContext().WebSites.Remove(selectedSite);
            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();
                var currentSites = CuratorsHelperEntities.GetContext().WebSites.ToList();
                currentSites = currentSites.Where(p => p.Curators.id_pass == UserId.ID).ToList();
                SitesList.ItemsSource = currentSites;
                SitesUpdated?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception msg)
            {
                MyMessage.Show("Что-то пошло не так");
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SiteHand = true;
            if (SiteHand)
            {
                site = (WebSites)SitesList.SelectedItem;
                SiteNameText.Text = site.website;
                UrlText.Text = site.url;

            }
        }


        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            AddStudentPopup.IsOpen = true;
            st = new student();
        }


        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            SaveStudent.Visibility = Visibility.Visible;
            AddStudentPopup.IsOpen = false;
            WarningStudentText.Visibility = Visibility.Hidden;
            StudentUpdated?.Invoke(this, EventArgs.Empty);
            foreach (var item in StudentAddGrid.Children)
            {
                if (item is TextBox)
                {
                    (item as TextBox).Clear();
                    (item as TextBox).BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222"));
                }
            }
            DateStText.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222"));
            PassportText.Text = null;
            CountParentSt.Text = "0";
            CountOtherSt.Text = "0";
            DateStText.SelectedDate = null;
            StCreateHand = false;
        }


        bool StCreateHand;

        private void Image_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            bool sthand = true;
            foreach (var item in StudentAddGrid.Children)
            {
                if (item is TextBox && (item as TextBox).Name != "RoomStText" && (item as TextBox).Name != "HobbiesText")
                {
                    if (string.IsNullOrEmpty((item as TextBox).Text))
                    {
                        sthand = false;
                        (item as TextBox).BorderBrush = Brushes.Red;
                    }
                    else
                    {
                        (item as TextBox).BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222")); 
                    }
                }
            }
            if (DateStText.SelectedDate == null)
            {
                sthand = false;
                DateStText.BorderBrush = Brushes.Red;
            }
            else
            {
                DateStText.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222"));
            }
            if (sthand)
            {
                SaveStudent.Visibility = Visibility.Hidden;
                WarningStudentText.Visibility = Visibility.Hidden;

                st.FIO = FioStText.Text;
                st.date_born = DateStText.SelectedDate;
                st.adres = AdresStText.Text;
                st.school = SchoolStText.Text;
                st.national = NationalStText.Text;
                st.phone = PhoneStText.Text;
                st.health = HealthStText.Text;
                st.relationship = ReletionshipStText.Text;
                st.id_group = curator.id_group;
                st.hobby = HobbiesText.Text;
                if (!string.IsNullOrEmpty(ofdPicture.FileName))
                {
                    byte[] img = File.ReadAllBytes(ofdPicture.FileName);
                    st.photo = img;
                }

                try
                {
                    CuratorsHelperEntities.GetContext().student.Add(st);
                    CuratorsHelperEntities.GetContext().SaveChanges();
                    var curSt = CuratorsHelperEntities.GetContext().student.ToList();
                    lastSt = curSt.LastOrDefault();
                    Hostel hos = new Hostel();
                    hos.id_student = lastSt.id_student;
                    hos.room = RoomStText.Text;
                    StCreateHand = true;
                    try
                    {
                        CuratorsHelperEntities.GetContext().Hostel.Add(hos);
                        CuratorsHelperEntities.GetContext().SaveChanges();
                    }
                    catch (Exception msg)
                    {
                        AddStudentPopup.IsOpen = false;
                        MyMessage.Show("Что-то пошло не так");
                        AddStudentPopup.IsOpen = true;
                    }
                }
                catch (Exception msg)
                {
                    AddStudentPopup.IsOpen = false;
                    MyMessage.Show("Что-то пошло не так");
                    AddStudentPopup.IsOpen = true;
                }
            }
            else
            {
                WarningStudentText.Visibility = Visibility.Visible;
            }
        }

        private void AddParentStBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!StCreateHand)
            {
                AddStudentPopup.IsOpen = false;
                MyMessage.Show("Сначала добавьте основные данные");
                AddStudentPopup.IsOpen = true;
            }
            else
            {
                ParentPopup.IsOpen = true;

                parent = new Parents();
                List<string> parentList = new List<string> { "Мать", "Отец", "Опекун" };
                ParentCombo.ItemsSource = parentList;
            }
        }

        private void CancelParentBtn_Click(object sender, RoutedEventArgs e)
        {
            WarningParentText.Visibility = Visibility.Hidden;
            ParentPopup.IsOpen = false;
            DateBornParText.SelectedDate = null;
            ParentCombo.SelectedIndex = -1;
            foreach (var item in AddParentGrid.Children)
            {
                if (item is TextBox)
                {
                    (item as TextBox).Clear();
                }
            }

        }


        private void SaveParentBtn_Click(object sender, RoutedEventArgs e)
        {
            bool handParent = true;
            foreach (var item in AddParentGrid.Children)
            {
                if (item is TextBox)
                {
                    if (string.IsNullOrEmpty((item as TextBox).Text)) handParent = false;
                }
            }
            if (DateBornParText.SelectedDate == null || ParentCombo.SelectedIndex == -1) handParent = false;

            if (handParent)
            {
                WarningParentText.Visibility = Visibility.Hidden;

                parent.parent = ParentCombo.SelectedValue.ToString();
                parent.FIO = FioParText.Text;
                parent.adres = AdresParText.Text;
                parent.phone = PhoneParText.Text;
                parent.job_place = JobPlaceParText.Text;
                parent.job = JobParText.Text;
                parent.date_bord = DateBornParText.SelectedDate;
                parent.id_student = lastSt.id_student;
                try
                {
                    CuratorsHelperEntities.GetContext().Parents.Add(parent);
                    CuratorsHelperEntities.GetContext().SaveChanges();
                    CountParentSt.Text = (int.Parse(CountParentSt.Text) + 1).ToString(); 
                    ParentPopup.IsOpen = false;
                    DateBornParText.SelectedDate = null;
                    ParentCombo.SelectedIndex = -1;
                    foreach (var item in AddParentGrid.Children)
                    {
                        if (item is TextBox)
                        {
                            (item as TextBox).Clear();
                        }
                    }

                }
                catch (Exception msg)
                {
                    MyMessage.Show("Что-то пошло не так");
                }
            }
            else
            {
                WarningParentText.Visibility = Visibility.Visible;
            }
        }
        private void PassportStBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!StCreateHand)
            {
                AddStudentPopup.IsOpen = false;
                MyMessage.Show("Сначала добавьте основные данные");
                AddStudentPopup.IsOpen = true;
            }
            else
            {
                PasportPopup.IsOpen = true;
                passport = new Passport();
            }
        }

        private void CancelPasportBtn_Click(object sender, RoutedEventArgs e)
        {
            PasportPopup.IsOpen = false;
            WarningPasportText.Visibility = Visibility.Hidden;
            DatePassportText.SelectedDate = null;
            NumPassportText.Clear();
            PersonPassportText.Clear();
        }

        private void SavePasportBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DatePassportText.SelectedDate != null && !string.IsNullOrEmpty(NumPassportText.Text) && !string.IsNullOrEmpty(PersonPassportText.Text))
            {
                WarningPasportText.Visibility = Visibility.Hidden;
                passport.id_student = lastSt.id_student;
                passport.num_passport = NumPassportText.Text;
                passport.person_issue = PersonPassportText.Text;
                passport.date_issue = DatePassportText.SelectedDate;

                try
                {
                    CuratorsHelperEntities.GetContext().Passport.Add(passport);
                    CuratorsHelperEntities.GetContext().SaveChanges();
                    PassportText.Text += NumPassportText.Text + ", " + PersonPassportText.Text + ", " + DatePassportText.Text;
                    DatePassportText.SelectedDate = null;
                    NumPassportText.Clear();
                    PersonPassportText.Clear();
                    PasportPopup.IsOpen = false;
                }
                catch (Exception msg)
                {
                    MyMessage.Show("Что-то пошло не так");
                }

            }
            else
            {
                WarningPasportText.Visibility = Visibility.Visible;
            }
        }

        private void AddOtherStBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!StCreateHand)
            {
                AddStudentPopup.IsOpen = false;
                MyMessage.Show("Сначала добавьте основные данные");
                AddStudentPopup.IsOpen = true;
            }
            else
            {
                OtherPopup.IsOpen = true;
                otherFam = new Brothers_sisters();
                List<string> whoList = new List<string> { "Брат", "Сестра"};
                List<string> statusList = new List<string> { "Учится", "Работает"};
                StatusOtherText.ItemsSource = statusList;
                WhoOtherText.ItemsSource = whoList;
            }
        }

        private void CancelOtherBtn_Click(object sender, RoutedEventArgs e)
        {
            OtherPopup.IsOpen = false;
            WarningOtherText.Visibility = Visibility.Hidden;
            WhoOtherText.SelectedIndex = -1;
            StatusOtherText.SelectedIndex = -1;
            FIOOtherText.Clear();
            DateOtherText.SelectedDate = null;
        }

        private void SaveOtherBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WhoOtherText.SelectedIndex != -1 && DateOtherText.SelectedDate != null && StatusOtherText.SelectedIndex != -1 && !string.IsNullOrEmpty(FIOOtherText.Text))
            {
                WarningOtherText.Visibility = Visibility.Hidden;
                otherFam.id_student = lastSt.id_student;
                otherFam.FIO = FIOOtherText.Text;
                otherFam.whois = WhoOtherText.Text;
                otherFam.status = StatusOtherText.Text;
                otherFam.date_born = DateOtherText.SelectedDate;

                try
                {
                    CuratorsHelperEntities.GetContext().Brothers_sisters.Add(otherFam);
                    CuratorsHelperEntities.GetContext().SaveChanges();
                    WhoOtherText.SelectedIndex = -1;
                    StatusOtherText.SelectedIndex = -1;
                    FIOOtherText.Clear();
                    DateOtherText.SelectedDate = null;
                    CountOtherSt.Text = (int.Parse(CountOtherSt.Text) + 1).ToString();
                    OtherPopup.IsOpen = false;
                }
                catch (Exception msg)
                {
                    MyMessage.Show("Что-то пошло не так");
                }

            }
            else
            {
                WarningOtherText.Visibility = Visibility.Visible;
            }
        }

        private void PhoneStText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            MaskPhone.Preview(sender as TextBox, e);
        }

        private void PhoneStText_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaskPhone.Changed(sender as TextBox, e);
        }


        /*Groups*/

        public void MountChanged()
        {
            var currentReport = CuratorsHelperEntities.GetContext().Plan_work.ToList();
            var curCurator = CuratorsHelperEntities.GetContext().Curators.ToList();
            Curators curPlan = curCurator.FirstOrDefault(p => p.id_pass == UserId.ID);
            currentReport = currentReport.Where(p => p.mounth == (int?)comboMounth.SelectedValue && p.id_group == curPlan.id_group).ToList();

            currentReport = currentReport.OrderByDescending(p => p.id_type).ToList();

            ReportList.ItemsSource = currentReport;
        }

        private void comboMounth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MountChanged();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window change = new View.ChangePlan();
            change.Show();
            this.IsVisibleChanged += ViberPage_IsVisibleChanged;
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Visibility = Visibility.Hidden;
            change.Tag = parentWindow;
        }

        private void ViberPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MountChanged();
        }

        private void ListViewItem_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            Plan_work selected = (Plan_work)ReportList.SelectedItem;
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Visibility = Visibility.Hidden;
            Window change = new View.ChangePlan(selected);
            change.Tag = parentWindow;
            change.Show();

        }

        private void otchet_Click(object sender, RoutedEventArgs e)
        {
            Window report = new View.ReportPlan();
            report.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Visibility = Visibility.Hidden;
            report.Tag = parentWindow;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Plan_work selectedPlan = (Plan_work)ReportList.SelectedItem;
            CuratorsHelperEntities.GetContext().Plan_work.Remove(selectedPlan);
            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();
                MountChanged();
            }
            catch
            {
                MyMessage.Show("Что-то пошло не так");
            }
        }

        private void StudentPhoto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ofdPicture.Filter =
               "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;
            AddStudentPopup.IsOpen = false;
            if (ofdPicture.ShowDialog() == true)
            {
                StudentPhoto.Source = new BitmapImage(new Uri(ofdPicture.FileName));
            }
            AddStudentPopup.IsOpen = true;
        }

        private void RoomStText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int number) && e.Text != "(" && e.Text != ")" && e.Text != "-")
            {
                e.Handled = true;
                return;
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Window print = new View.PlanPrint((int)comboMounth.SelectedValue);
            print.Show();
            Window win = Window.GetWindow(this);
            print.Tag = win;
            win.Visibility = Visibility.Hidden;
        }
    }
}
