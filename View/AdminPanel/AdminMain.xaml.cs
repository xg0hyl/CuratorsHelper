using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace CuratorsHelper.View.AdminPanel
{
    /// <summary>
    /// Логика взаимодействия для AdminMain.xaml
    /// </summary>
    public partial class AdminMain : Window
    {
        OpenFileDialog ofdPicture = new OpenFileDialog();
        Curators selectCur;
        public AdminMain()
        {
            InitializeComponent();

            var currentGroup = CuratorsHelperEntities.GetContext().Groups.ToList();
            currentGroup = currentGroup.Where(p => p.name != "None")
                .OrderBy(p => p.name).ToList();
            currentGroup.Insert(0, new Groups
            {
                name = "Все группы"
            });
            CuratorsCombo.ItemsSource = currentGroup;
            CuratorsCombo.SelectedIndex = 0;
            GroupsStCombo.ItemsSource = currentGroup;
            GroupsStCombo.SelectedIndex = 0;

            var currentCurators = CuratorsHelperEntities.GetContext().Curators.ToList();
            CuratorView.ItemsSource = currentCurators;

            var currentGrList = CuratorsHelperEntities.GetContext().Groups.ToList();
            ListGroups.ItemsSource = currentGrList.Where(p => p.name != "None")
                .OrderBy(p => p.name);

            var currentStudents = CuratorsHelperEntities.GetContext().student.ToList();
            StudentView.ItemsSource = currentStudents.Where(p => p.expelled != "Отчислен");

            ListCyclic.ItemsSource = CuratorsHelperEntities.GetContext().cyclics.ToList();

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
            Window back = new MainWindow();
            back.Show();
            Close();
        }


        private void CuratorsBtn_Click(object sender, RoutedEventArgs e)
        {
            CuratorsGrid.Visibility = Visibility.Visible;
            ButtonsStack.Visibility = Visibility.Hidden;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ButtonsStack.Visibility = Visibility.Visible;
            CuratorsGrid.Visibility = Visibility.Hidden;
            StudentsGrid.Visibility = Visibility.Hidden;
            CyclicGrid.Visibility = Visibility.Hidden;
            GroupsGrid.Visibility = Visibility.Hidden;


        }

        private void CuratorsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentCurators = CuratorsHelperEntities.GetContext().Curators.ToList();
            CuratorsFindText.Clear();
            if (CuratorsCombo.SelectedIndex != 0)
                currentCurators = currentCurators.Where(p => p.id_group == (int?)CuratorsCombo.SelectedValue).ToList();

            CuratorView.ItemsSource = currentCurators;
        }

        private void CuratorsFindText_TextChanged(object sender, TextChangedEventArgs e)
        {
            CuratorsCombo.SelectedIndex = 0;
            var currentCur = CuratorsHelperEntities.GetContext().Curators
                .Where(p => p.FIO.StartsWith(CuratorsFindText.Text)).ToList();
            CuratorView.ItemsSource = currentCur;
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            CuratorsPopup.IsOpen = true;
            AddHand = true;
            SaveStudent.Visibility = Visibility.Visible;
            DeleteCurator.Visibility = Visibility.Hidden;
            SaveCurator.Visibility = Visibility.Hidden;
            StudentPhoto.Source = new BitmapImage(new Uri("/images/nonePhoto.png", UriKind.Relative));
            GroupCurText.ItemsSource = CuratorsHelperEntities.GetContext().Groups.ToList();
            CuclicCurText.ItemsSource = CuratorsHelperEntities.GetContext().cyclics.ToList();
            PasswordCurTextBlock.Visibility = Visibility.Visible;
            PassCurText.Visibility = Visibility.Visible;
            NextGroupBtn.Visibility = Visibility.Hidden;
        }
        bool AddHand;
        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            CuratorsPopup.IsOpen = false;
            CuratorsPopup.DataContext = null;
            GroupCurText.SelectedIndex = -1;
            CuclicCurText.SelectedIndex = -1;
            WarningTextCur.Visibility = Visibility.Hidden;
            NextGroupBtn.IsEnabled = true;
        }

        public void AddCur()
        {
            bool isnull = false;
            foreach (var item in AddGridCur.Children)
            {
                if (item is TextBox)
                {
                    if (string.IsNullOrEmpty((item as TextBox).Text)) isnull = true;
                }
            }
            if (DateCurText.SelectedDate == null) isnull = true;
            if (GroupCurText.SelectedIndex == -1) isnull = true;
            if (CuclicCurText.SelectedIndex == -1) isnull = true;

            if (!isnull)
            {
                WarningTextCur.Visibility = Visibility.Hidden;
                Curators cur = new Curators();
                cur.FIO = FioCurText.Text;
                cur.phone = PhoneCurText.Text;
                cur.date_born = DateCurText.SelectedDate;
                cur.education = EdCurText.Text;

                if (Regex.IsMatch(EmailCurText.Text, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$"))
                    cur.email = EmailCurText.Text;
                else
                {
                    CuratorsPopup.IsOpen = false;
                    MyMessage.Show("Email введен неверно");
                    CuratorsPopup.IsOpen = true;
                    return;
                }

                cur.id_group = (int)GroupCurText.SelectedValue;
                cur.id_cyclic = (int)CuclicCurText.SelectedValue;

                if (!string.IsNullOrEmpty(ofdPicture.FileName))
                {
                    byte[] img = File.ReadAllBytes(ofdPicture.FileName);
                    cur.photo = img;
                }

                Passwords pass = new Passwords();
                var curPass = CuratorsHelperEntities.GetContext().Passwords
                    .Where(p => p.login == LogCurText.Text).ToList();
                if (curPass.Count == 0)
                {
                    pass.login = LogCurText.Text;
                }
                else
                {
                    CuratorsPopup.IsOpen = false;
                    MyMessage.Show("Введенный логин уже существует");
                    CuratorsPopup.IsOpen = true;
                    return;
                }

                string BPass;
                byte[] salt;
                salt = ArgonClass.CreateSalt();
                BPass = ArgonClass.HashPassword(PassCurText.Text, salt);
                pass.salt = salt;
                pass.password = BPass;
                CuratorsHelperEntities.GetContext().Passwords.Add(pass);
                try
                {
                    CuratorsHelperEntities.GetContext().SaveChanges();
                    var curentPass = CuratorsHelperEntities.GetContext().Passwords.ToList();
                    var curP = curentPass.Last();
                    cur.id_pass = curP.id_num;
                    CuratorsHelperEntities.GetContext().Curators.Add(cur);
                    try
                    {
                        CuratorsHelperEntities.GetContext().SaveChanges();
                        foreach (var item in AddGridCur.Children)
                        {
                            if (item is TextBox)
                            {
                                (item as TextBox).Clear();
                            }
                        }
                        DateCurText.SelectedDate = null;
                        CuratorsCombo.SelectedIndex = -1;
                        CuratorsCombo.SelectedIndex = 0;
                        CuratorsPopup.IsOpen = false;
                    }
                    catch (Exception msg)
                    {
                        CuratorsPopup.IsOpen = false;
                        MyMessage.Show("Что-то пошло не так");
                        CuratorsPopup.IsOpen = true;
                    }

                }
                catch
                {
                    CuratorsPopup.IsOpen = false;
                    MyMessage.Show("Что-то пошло не так");
                    CuratorsPopup.IsOpen = true;
                }

            }
            else WarningTextCur.Visibility = Visibility.Visible;
        }

        private void Image_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            if (AddHand) AddCur();
        }

        private void StudentPhoto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;
            CuratorsPopup.IsOpen = false;
            if (ofdPicture.ShowDialog() == true)
            {
                StudentPhoto.Source = new BitmapImage(new Uri(ofdPicture.FileName));
            }
            CuratorsPopup.IsOpen = true;
        }

        private void PhoneCurText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            MaskPhone.Preview(sender as TextBox, e);
        }

        private void PhoneCurText_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaskPhone.Changed(sender as TextBox, e);
        }

        private void ShowMoreText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CuratorsPopup.IsOpen = true;
            AddHand = false;
            GroupCurText.ItemsSource = CuratorsHelperEntities.GetContext().Groups.ToList();
            CuclicCurText.ItemsSource = CuratorsHelperEntities.GetContext().cyclics.ToList();
            CuratorsPopup.DataContext = (sender as TextBlock).DataContext as Curators;
            selectCur = (sender as TextBlock).DataContext as Curators;
            SaveCurator.Visibility = Visibility.Visible;
            DeleteCurator.Visibility = Visibility.Visible;
            SaveStudent.Visibility = Visibility.Hidden;
            if (selectCur.Groups.name == "None")
            {
                NextGroupBtn.IsEnabled = false;
            }
            PasswordCurTextBlock.Visibility = Visibility.Hidden;
            PassCurText.Visibility = Visibility.Hidden;
            NextGroupBtn.Visibility = Visibility.Visible;
                
        }

        private void SaveCurator_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(ofdPicture.FileName))
            {
                byte[] img = File.ReadAllBytes(ofdPicture.FileName);
                selectCur.photo = img;
            }

            CuratorsHelperEntities.GetContext().Entry(selectCur).State = System.Data.Entity.EntityState.Modified;
            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();


                CuratorsPopup.IsOpen = false;
                CuratorsCombo.SelectedIndex = -1;
                CuratorsCombo.SelectedIndex = 0;
            }
            catch (Exception msg)
            {
                CuratorsPopup.IsOpen = false;
                MyMessage.Show("Что-то пошло не так");
                CuratorsPopup.IsOpen = true;
            }
        }

        private void DeleteCurator_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeleteCuratorPopup.IsOpen = true;
        }

        private void BtnDeleteCurator_Click(object sender, RoutedEventArgs e)
        {
            int idPass = (int)selectCur.id_pass;
            CuratorsHelperEntities.GetContext().Curators.Remove(selectCur);
            try
            {
                Passwords curPass = CuratorsHelperEntities.GetContext().Passwords
                    .FirstOrDefault(p => p.id_num == idPass);
                CuratorsHelperEntities.GetContext().Passwords.Remove(curPass);
                CuratorsHelperEntities.GetContext().SaveChanges();
                DeleteCuratorPopup.IsOpen = false;
                CuratorsPopup.IsOpen = false;
                CuratorsCombo.SelectedIndex = -1;
                CuratorsCombo.SelectedIndex = 0;
                    
            }
            catch (Exception msg)
            {
                CuratorsPopup.IsOpen = false;
                MyMessage.Show("Что-то пошло не так");
                CuratorsPopup.IsOpen = true;
            }
        }

        private void BtnCancelCurator_Click(object sender, RoutedEventArgs e)
        {
            DeleteCuratorPopup.IsOpen = false;
        }


        private void StudentBtn_Click(object sender, RoutedEventArgs e)
        {
            StudentsGrid.Visibility = Visibility.Visible;
            ButtonsStack.Visibility = Visibility.Hidden;
        }

        private void GroupsStCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentStudents = CuratorsHelperEntities.GetContext().student.ToList();

            if (GroupsStCombo.SelectedIndex != 0)
                currentStudents = currentStudents.Where(p => p.Groups.id_group == (int?)GroupsStCombo.SelectedValue).ToList();
            if (ExpelledStudentShow.IsChecked == true)
                currentStudents = currentStudents.Where(p => p.expelled == "Отчислен").ToList();
            else currentStudents = currentStudents.Where(p => p.expelled != "Отчислен").ToList();
            StudentView.ItemsSource = currentStudents;
        }

        private void ExpelledStudentShow_Checked(object sender, RoutedEventArgs e)
        {
            var currentStudents = CuratorsHelperEntities.GetContext().student.ToList();

            if (GroupsStCombo.SelectedIndex != 0)
                currentStudents = currentStudents.Where(p => p.Groups.id_group == (int?)GroupsStCombo.SelectedValue).ToList();
            currentStudents = currentStudents.Where(p => p.expelled == "Отчислен").ToList();
            StudentView.ItemsSource = currentStudents;
        }

        private void ExpelledStudentShow_Unchecked(object sender, RoutedEventArgs e)
        {
            var currentStudents = CuratorsHelperEntities.GetContext().student.ToList();

            if (GroupsStCombo.SelectedIndex != 0)
                currentStudents = currentStudents.Where(p => p.Groups.id_group == (int?)GroupsStCombo.SelectedValue).ToList();
            currentStudents = currentStudents.Where(p => p.expelled != "Отчислен").ToList();
            StudentView.ItemsSource = currentStudents;
        }

        private void ShowMoreText_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            StudentPopup.IsOpen = true;
            student selectStudent = (sender as TextBlock).DataContext as student;
            StudentPopup.DataContext = selectStudent;
            Passport passport = new Passport();
            passport = CuratorsHelperEntities.GetContext().Passport
                .FirstOrDefault(p => p.id_student == selectStudent.id_student);
            DateTime datePassport = new DateTime();
            if (passport != null)
            {
                datePassport = (DateTime)passport.date_issue;
                PassportStText.Text = passport.num_passport + ", " + passport.person_issue + ", " + datePassport.Date.ToString("dd.MM.yyyy");
            }
            else PassportStText.Text = "Отсутствуют";

            var currentHostel = CuratorsHelperEntities.GetContext().Hostel.ToList();
            HostelStText.DataContext = currentHostel.FirstOrDefault(p => p.id_student == selectStudent.id_student);

            WriteParent(selectStudent);
            WriteFamily(selectStudent);
        }

        private void WriteParent(student selectStudent)
        {
            var currentParents = CuratorsHelperEntities.GetContext().Parents.ToList();

            currentParents = currentParents.Where(p => p.id_student == selectStudent.id_student).ToList();
            MotherStText.Clear();
            FatherStText.Clear();

            var Mother = currentParents.FirstOrDefault(p => p.parent == "Мать");
            if (Mother != null)
            {
                DateTime dateMother = new DateTime();
                if (Mother.date_bord != null)
                    dateMother = (DateTime)Mother.date_bord;
                MotherStText.Text = Mother.parent + ": " + Mother.FIO + ", " + dateMother.Date.ToString("dd.MM.yyyy") + ", " +
                    Mother.adres + ", " + Mother.phone + ", " + Mother.job_place + ", " + Mother.job;
            }

            var Father = currentParents.FirstOrDefault(p => p.parent == "Отец");
            if (Father != null)
            {
                DateTime dateFather = new DateTime();

                if (Father.date_bord != null)
                    dateFather = (DateTime)Father.date_bord;

                FatherStText.Text = Father.parent + ": " + Father.FIO + ", " + dateFather.Date.ToString("dd.MM.yyyy") + ", " +
                    Father.adres + ", " + Father.phone + ", " + Father.job_place + ", " + Father.job;
            }

            var Opekun = currentParents.FirstOrDefault(p => p.parent == "Опекун");
            if (Opekun != null)
            {
                DateTime dateOpekun = new DateTime();

                if (Opekun.date_bord != null)
                    dateOpekun = (DateTime)Opekun.date_bord;

                MotherStText.Text = Opekun.parent + ": " + Opekun.FIO + ", " + dateOpekun.Date.ToString("dd.MM.yyyy") + ", " +
                    Opekun.adres + ", " + Opekun.phone + ", " + Opekun.job_place + ", " + Opekun.job;
            }

            if (Mother == null && Opekun == null) MotherStText.Visibility = Visibility.Hidden;
            else MotherStText.Visibility = Visibility.Visible;
            if (Father == null) FatherStText.Visibility = Visibility.Hidden;
            else FatherStText.Visibility = Visibility.Visible;
        }

        private void WriteFamily(student selectStudent)
        {
            var currentOtherFamily = CuratorsHelperEntities.GetContext().Brothers_sisters.ToList();
            currentOtherFamily = currentOtherFamily.Where(p => p.id_student == selectStudent.id_student).ToList();
            DateTime OtherDate = new DateTime();

            BrothersStText.Clear();
            SistersStText.Clear();

            int countBrother = 0; int countSisters = 0;
            foreach (var item in currentOtherFamily)
            {
                if (item.whois == "Брат") countBrother++;
                if (item.whois == "Сестра") countSisters++;
            }
            if (countBrother == 0)
            {
                BrothersStText.Visibility = Visibility.Hidden;
            }
            else BrothersStText.Visibility = Visibility.Visible;
            if (countSisters == 0)
            {
                SistersStText.Visibility = Visibility.Hidden;
            }
            else SistersStText.Visibility = Visibility.Visible;

            for (int i = 0; i < currentOtherFamily.Count; i++)
            {
                if (currentOtherFamily[i].date_born != null)
                    OtherDate = (DateTime)currentOtherFamily[i].date_born;
                if (currentOtherFamily[i].whois == "Брат")
                    if (i == 0)
                        BrothersStText.Text += currentOtherFamily[i].whois + ": " + currentOtherFamily[i].FIO + ", " + OtherDate.Date.ToString("dd.MM.yyyy") + ", " + currentOtherFamily[i].status;
                    else
                        BrothersStText.Text += "\n" + currentOtherFamily[i].whois + ": " + currentOtherFamily[i].FIO + ", " + OtherDate.Date.ToString("dd.MM.yyyy") + ", " + currentOtherFamily[i].status;
                if (currentOtherFamily[i].whois == "Сестра")
                    if (i == 0)
                        SistersStText.Text += currentOtherFamily[i].whois + ": " + currentOtherFamily[i].FIO + ", " + OtherDate.Date.ToString("dd.MM.yyyy") + ", " + currentOtherFamily[i].status;
                    else 
                        SistersStText.Text += "\n" + currentOtherFamily[i].whois + ": " + currentOtherFamily[i].FIO + ", " + OtherDate.Date.ToString("dd.MM.yyyy") + ", " + currentOtherFamily[i].status;
            }
 
        }


        private void Image_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            StudentPopup.IsOpen = false;
        }

        private void GroupsBtn_Click(object sender, RoutedEventArgs e)
        {
            ButtonsStack.Visibility = Visibility.Hidden;
            GroupsGrid.Visibility = Visibility.Visible;
        }

        bool hand = false; Groups group;
        private void AddGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(GroupNameText.Text))
            {
                if (!hand)
                {
                    group = new Groups();
                    group.name = GroupNameText.Text;
                    CuratorsHelperEntities.GetContext().Groups.Add(group);
                }
                else
                {
                    if (ListGroups.SelectedValue != null)
                    {
                        group.name = GroupNameText.Text;
                    }
                }
                
                try
                {
                    CuratorsHelperEntities.GetContext().SaveChanges();
                    hand = false;

                    var currentGroup = CuratorsHelperEntities.GetContext().Groups.ToList().Where(p => p.name != "None");
                    ListGroups.ItemsSource = currentGroup.OrderBy(p => p.name).ToList();
                    var currentSortGroup = currentGroup.OrderBy(p => p.name).ToList();

                    currentSortGroup.Insert(0, new Groups
                    {
                        name = "Все группы"
                    });
                    CuratorsCombo.ItemsSource = currentSortGroup;
                    CuratorsCombo.SelectedIndex = 0;
                    GroupsStCombo.ItemsSource = currentSortGroup;
                    GroupsStCombo.SelectedIndex = 0;
                    GroupNameText.Clear();
                }
                catch (Exception msg)
                {
                    MyMessage.Show("Что-то пошло не так");
                }
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            hand = true;
            group = (Groups)ListGroups.SelectedItem;
            GroupNameText.Text = group.name;
        }

        Groups delGRoup;
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ListGroups.SelectedItem != null)
            {
                DelGroups.IsOpen = true;
                delGRoup = (Groups)ListGroups.SelectedItem;
                WarningPopupGroup.Text += delGRoup.name + "?";
            }
        }

        private void DeletePopupGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CuratorsHelperEntities.GetContext().Groups.Remove(delGRoup);
                CuratorsHelperEntities.GetContext().SaveChanges();
                DelGroups.IsOpen = false;
                WarningPopupGroup.Text = "Вы уверены, что хотите удалить ";

                var currentGroup = CuratorsHelperEntities.GetContext().Groups.ToList().Where(p => p.name != "None");
                ListGroups.ItemsSource = currentGroup.OrderBy(p => p.name).ToList();
                var currentSortGroup = currentGroup.OrderBy(p => p.name).ToList();

                currentSortGroup.Insert(0, new Groups
                {
                    name = "Все группы"
                });
                CuratorsCombo.ItemsSource = currentSortGroup;
                CuratorsCombo.SelectedIndex = 0;
                GroupsStCombo.ItemsSource = currentSortGroup;
                GroupsStCombo.SelectedIndex = 0;

            }
            catch (Exception msg)
            {
                DelGroups.IsOpen = false;
                MyMessage.Show("Что-то пошло не так");
                DelGroups.IsOpen = true;
            }
        }

        private void CancelPopupGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            DelGroups.IsOpen = false;
            WarningPopupGroup.Text = "Вы уверены, что хотите удалить ";
        }

        private void CuclicBtn_Click(object sender, RoutedEventArgs e)
        {
            CyclicGrid.Visibility = Visibility.Visible;
            ButtonsStack.Visibility = Visibility.Hidden;
        }

        cyclics cycl; bool HandCycl = false;
        private void AddCyclicBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isnull = false;
            foreach (var item in AddCyclicStack.Children)
            {
                if (item is TextBox)
                {
                    if (string.IsNullOrEmpty((item as TextBox).Text)) isnull = true;
                }
            }

            if (!isnull)
            {
                if (!HandCycl)
                {
                    cycl = new cyclics();
                    cycl.name = CyclicNameText.Text;
                    cycl.cabinet = int.Parse(CyclicCabinetText.Text);
                    cycl.FIO_predsedatel = CyclicFIOText.Text;
                    cycl.phone_predsedatel = CyclicPhoneText.Text;
                    CuratorsHelperEntities.GetContext().cyclics.Add(cycl);
                }

                try
                {
                    CuratorsHelperEntities.GetContext().SaveChanges();
                    ListCyclic.ItemsSource = CuratorsHelperEntities.GetContext().cyclics.ToList();
                    HandCycl = false;
                    foreach (var item in AddCyclicStack.Children)
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
        }

        private void ListViewItem_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            HandCycl = true;
            AddCyclicStack.DataContext = ListCyclic.SelectedItem;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (ListCyclic.SelectedValue != null)
                DelCyclic.IsOpen = true;
        }

        private void DeletePopupCyclicBtn_Click(object sender, RoutedEventArgs e)
        {
            CuratorsHelperEntities.GetContext().cyclics.Remove((cyclics)ListCyclic.SelectedItem);
            try
            {
                CuratorsHelperEntities.GetContext().SaveChanges();
                ListCyclic.ItemsSource = CuratorsHelperEntities.GetContext().cyclics.ToList();
                DelCyclic.IsOpen = false;
            }
            catch (Exception msg)
            {
                MyMessage.Show("Что-то пошло не так");
            }
        }

        private void CancelPopupGroupBtn_Click_1(object sender, RoutedEventArgs e)
        {
            DelCyclic.IsOpen = false;
        }

        private void CyclicCabinetText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
                return;
            }
        }

        private void CyclicPhoneText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            MaskPhone.Preview(sender as TextBox, e);
        }

        private void CyclicPhoneText_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaskPhone.Changed(sender as TextBox, e);
        }

        private void CalncelCyclicBtn_Click(object sender, RoutedEventArgs e)
        {
            HandCycl = false;
            foreach (var item in AddCyclicStack.Children)
            {
                if (item is TextBox)
                {
                    (item as TextBox).Clear();
                }
            }
        }

        private void CancelGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            hand = false;
            GroupNameText.Clear();
        }
        private void NextGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            Curators thisCur = (Curators)CuratorsPopup.DataContext;
            var listStudents = CuratorsHelperEntities.GetContext().student.ToList();
            listStudents = listStudents.Where(p => p.id_group == thisCur.id_group && p.expelled != "Отчислен").ToList();
            Groups Group = (Groups)GroupCurText.SelectedItem;
            string nameGroup = Group.name;
            var allGroups = CuratorsHelperEntities.GetContext().Groups.ToList();
            if (thisCur.Groups.name == "None") return;
            string[] nameGR = nameGroup.Split('-');

            string[] numGr = nameGR[1].ToCharArray().Select(c => c.ToString()).ToArray(); ;
            numGr[0] = (int.Parse(numGr[0]) + 1).ToString();

            string newGroup = nameGR[0] + "-";
            foreach (var item in numGr)
                newGroup += item;

            Groups findGroup = allGroups.FirstOrDefault(p => p.name == newGroup);
           

            if (findGroup != null)
            {
                CuratorsPopup.IsOpen = false;
                Window modal = new ModalSettings();
                if (modal.ShowDialog() == true)
                {
                    thisCur.id_group = findGroup.id_group;
                    foreach (var item in listStudents)
                    {
                        item.id_group = findGroup.id_group;
                    }
                    try
                    {
                        CuratorsHelperEntities.GetContext().SaveChanges();
                        GroupCurText.SelectedItem = findGroup;
                        CuratorsCombo.SelectedIndex = -1;
                        CuratorsCombo.SelectedIndex = 0;
                        GroupsStCombo.SelectedIndex = -1;
                        GroupsStCombo.SelectedIndex = 0;
                    }
                    catch (Exception msg)
                    {
                        MyMessage.Show("Что-то пошло не так");
                    }
                }
                CuratorsPopup.IsOpen = true;

            }
            else
            {
                CuratorsPopup.IsOpen = false;
                Window modal = new ModalSettings();
                if (modal.ShowDialog() == true)
                {
                    findGroup = allGroups.FirstOrDefault(p => p.name == "None");
                    thisCur.id_group = findGroup.id_group;
                    foreach (var item in listStudents)
                    {
                        item.expelled = "Отчислен";
                    }
                    try
                    {
                        CuratorsHelperEntities.GetContext().SaveChanges();
                        GroupCurText.SelectedItem = findGroup;
                        CuratorsCombo.SelectedIndex = -1;
                        CuratorsCombo.SelectedIndex = 0;
                        GroupsStCombo.SelectedIndex = -1;
                        GroupsStCombo.SelectedIndex = 0;
                    }
                    catch (Exception msg)
                    {
                        MyMessage.Show("Что-то пошло не так");
                    }
                }
                CuratorsPopup.IsOpen = true;
            }
            NextGroupBtn.IsEnabled = false;
        }
    }
}
