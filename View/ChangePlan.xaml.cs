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
    /// Логика взаимодействия для ChangePlan.xaml
    /// </summary>
    public partial class ChangePlan : Window
    {
        public int hand;
        Plan_work _newPlan = new Plan_work();
        public ChangePlan()
        {
            InitializeComponent();
            InizalizeCombo();
            comboMounth.SelectedIndex = DateTime.Now.Month-1;
            textDate.SelectedDate = DateTime.Now.Date;
            hand = 0;
            NumGroup();
        }
        public ChangePlan(Plan_work content)
        {
            InitializeComponent();
            InizalizeCombo();
            hand = 1;
            NumGroup();
            _newPlan = content;
            DataContext = _newPlan;
            comboMounth.SelectedItem = content.Mounth1;
            comboType.SelectedItem = content.Type_plan_work;
            if (content.check_end == "отмеченно")
            {
                Check.IsChecked = true;
            }
        }

        public void NumGroup()
        {
            var currentCurator = CuratorsHelperEntities.GetContext().Curators.ToList();
            Curators curator = currentCurator.Single(p => p.id_pass == UserId.ID);

            NumGroupText.Content += curator.Groups.name;
        }

        public void InizalizeCombo()
        {
            var currentMounth = CuratorsHelperEntities.GetContext().Mounth.ToList();
            var currentType = CuratorsHelperEntities.GetContext().Type_plan_work.ToList();
            comboMounth.ItemsSource = currentMounth;
            comboType.ItemsSource = currentType;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (hand == 0)
            {
                _newPlan.mounth = comboMounth.SelectedIndex + 1;
                _newPlan.id_type = comboType.SelectedIndex + 1;

                _newPlan.form_work = textWork.Text;
                Curators cur = CuratorsHelperEntities.GetContext().Curators.FirstOrDefault(p => p.id_pass == UserId.ID);
                _newPlan.id_group = cur.id_group;

                DateTime date = (DateTime)textDate.SelectedDate;

                if (date.Month == comboMounth.SelectedIndex + 1)
                {
                    _newPlan.date = textDate.SelectedDate;
                }
                else errors.Append("Месяца не совпадают\n");


                if (Check.IsChecked == true)
                {
                    _newPlan.check_end = "отмеченно";
                }
                else _newPlan.check_end = "не отмеченно";

                if (errors.Length == 0)
                {
                    CuratorsHelperEntities.GetContext().Plan_work.Add(_newPlan);
                    try
                    {
                        CuratorsHelperEntities.GetContext().SaveChanges();
                        MyMessage.Show("Успешно добавлено");
                        Window parentWindow = Tag as Window;
                        if (parentWindow != null)
                        {
                            parentWindow.Visibility = Visibility.Visible;
                        }
                        Close();
                    }
                    catch (Exception msg)
                    {
                        MyMessage.Show(msg.ToString());
                    }
                }
                else MyMessage.Show(errors.ToString());
            }
            else
            {

                _newPlan.mounth = comboMounth.SelectedIndex + 1;
                _newPlan.id_type = comboType.SelectedIndex + 1;


                _newPlan.form_work = textWork.Text;


                DateTime date = (DateTime)textDate.SelectedDate;


                if (date.Month == comboMounth.SelectedIndex + 1)
                {
                    _newPlan.date = textDate.SelectedDate;
                }
                else errors.Append("Месяца не совпадают\n");


                if (Check.IsChecked == true)
                {
                    _newPlan.check_end = "отмеченно";
                }
                else _newPlan.check_end = "не отмеченно";



                if (errors.Length == 0)
                {
                    try
                    {
                        CuratorsHelperEntities.GetContext().SaveChanges();
                        MyMessage.Show("Успешно добавлено");
                        Window parentWindow = Tag as Window;
                        if (parentWindow != null)
                        {
                            parentWindow.Visibility = Visibility.Visible;
                        }
                        Close();
                    }
                    catch (Exception msg)
                    {
                        MyMessage.Show(msg.ToString());
                    }
                }
                else MyMessage.Show(errors.ToString());
            }
            

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = Tag as Window;
            Close();
            if (parentWindow != null)
            {
                
                parentWindow.Visibility = Visibility.Visible;
            }

        }

        private void FrameworkElement_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void collapseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
