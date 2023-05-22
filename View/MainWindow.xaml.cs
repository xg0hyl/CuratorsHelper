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
using CuratorsHelper;
using System.IO;

namespace CuratorsHelper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = "rememberMe.txt";
        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(path))
            {

                string[] file = File.ReadAllLines(path);
                if (file.Length != 1)
                {
                    var currentPass = CuratorsHelperEntities.GetContext().Passwords.ToList();
                    rememberMe.IsChecked = true;
                    currentPass = currentPass.Where(p => p.login.Contains(file[0])).ToList();
                    if (currentPass.Count != 0)
                    {
                        foreach (var item in currentPass)
                        {
                            if (file[1] == item.password && item.login == file[0] && item.id_num == Convert.ToInt32(file[2]))
                            {
                                UserId.ID = item.id_num;
                                var currentRuk = CuratorsHelperEntities.GetContext().rukovodstvo.ToList();
                                rukovodstvo ruk = currentRuk.SingleOrDefault(p => p.id_pass == item.id_num);
                                if (ruk != null) UserId.Role = ruk.roles.name;
                                Window main = new MainMenuCurators();
                                main.Show();
                                this.Close();
                            }
                        }
                    }
                }

            }
            else
            {
                File.Create(path);
            }
            
        }


        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            if (ShowPassword.IsChecked == true)
            {
                textPass.FontFamily = new FontFamily("Segoe UI");
                
            }
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ShowPassword.IsChecked == false)
            {
                textPass.FontFamily = new FontFamily("Password");
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            bool hand = false;
            if (!string.IsNullOrEmpty(textLogin.Text) || !string.IsNullOrEmpty(textPass.Text))
            {
                if (textLogin.Text == "admin" && textPass.Text == "qwerty")
                {
                    Window adm = new View.AdminPanel.AdminMain();
                    adm.Show();
                    Close();
                    return;
                }
                var currentPass = CuratorsHelperEntities.GetContext().Passwords.ToList();
                int id = 0;
                string checkPass = "";
                byte[] checkSalt;
                currentPass = currentPass.Where(p => p.login.Contains(textLogin.Text)).ToList();
                if (currentPass.Count != 0)
                {
                    foreach (var item in currentPass)
                    {
                        if (ArgonClass.HashPassword(textPass.Text, item.salt) == item.password && textLogin.Text == item.login)
                        {
                            hand = true;
                            checkPass = item.password;
                            checkSalt = item.salt;
                            id = item.id_num;
                        }
                    }
                }

                if (hand)
                {
                    if (rememberMe.IsChecked == true)
                    {
                        using (StreamWriter writer = new StreamWriter(path, false))
                        {
                            writer.WriteLineAsync(textLogin.Text + "\n" + checkPass + "\n" + id);
                        }
                    }
                    UserId.ID = id;
                    var currentRuk = CuratorsHelperEntities.GetContext().rukovodstvo.ToList();
                    rukovodstvo ruk = new rukovodstvo();


                    foreach (var item in currentRuk)
                        if (item.id_pass == UserId.ID)
                        {
                            UserId.Role = item.roles.name;
                            break;
                        }
                        else UserId.Role = null;

                    Window main = new MainMenuCurators();
                    main.Show();
                    this.Close();
                }
                else MyMessage.Show("Неверный логин или пароль");
            }
            else MyMessage.Show("Заполните все поля");

            /*
                        Passwords pass = new Passwords();
                        string BPass;
                        byte[] salt;
                        salt = ArgonClass.CreateSalt();
                        BPass = ArgonClass.HashPassword(textPass.Text, salt);
                        pass.salt = salt;
                        pass.password = BPass;
                        pass.login = textLogin.Text;
                        CuratorsHelperEntities.GetContext().Passwords.Add(pass);
                        try
                        {
                            CuratorsHelperEntities.GetContext().SaveChanges();
                        }
                        catch
                        {
                            MyMessage.Show("not added");
                        }*/
        }

        private void collapseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
