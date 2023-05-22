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

namespace CuratorsHelper.viewOther
{
    /// <summary>
    /// Логика взаимодействия для GroupsPsyho.xaml
    /// </summary>
    public partial class GroupsPsyho : Page
    {
        public GroupsPsyho()
        {
            InitializeComponent();
            var currentGroup = CuratorsHelperEntities.GetContext().Groups.ToList();
            currentGroup.Insert(0, new Groups
            {
                name = "Все группы"
            });

            GroupsCombo.ItemsSource = currentGroup;
            GroupsCombo.SelectedIndex = 0;

            var currentStudents = CuratorsHelperEntities.GetContext().student.ToList();
            StudentView.ItemsSource = currentStudents;

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void GroupsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentStudents = CuratorsHelperEntities.GetContext().student.ToList();

            if (GroupsCombo.SelectedIndex != 0)
                currentStudents = currentStudents.Where(p => p.Groups.id_group == (int?)GroupsCombo.SelectedValue).ToList();

            StudentView.ItemsSource = currentStudents;
        }
    }
}
