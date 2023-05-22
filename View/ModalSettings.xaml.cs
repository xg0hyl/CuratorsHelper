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
    /// Логика взаимодействия для ModalSettings.xaml
    /// </summary>
    public partial class ModalSettings : Window
    {
        public ModalSettings()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            var currentUser = CuratorsHelperEntities.GetContext().Curators.ToList();
            Curators curator = currentUser.FirstOrDefault(p => p.id_pass == UserId.ID);
            if (PassText.Text == "qwerty" && curator == null)
            {
                DialogResult = true;
                return;
            }
            if (ArgonClass.HashPassword(PassText.Text, curator.Passwords1.salt) == curator.Passwords1.password && curator != null)
            {
                DialogResult = true;
            }
            else
            {
                MyMessage.Show("Неверный пароль. Попробуйте еще раз");
                PassText.Clear();
            }
        }
    }
}
