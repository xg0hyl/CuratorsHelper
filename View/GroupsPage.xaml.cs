using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace CuratorsHelper
{
    /// <summary>
    /// Логика взаимодействия для GroupsPage.xaml
    /// </summary>
    public partial class GroupsPage : Page
    {

        public GroupsPage()
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

            var currentCurators = CuratorsHelperEntities.GetContext().Curators.ToList();
            CuratorView.ItemsSource = currentCurators;
        }

        private void CuratorsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentCurators = CuratorsHelperEntities.GetContext().Curators.ToList();

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
    }
}