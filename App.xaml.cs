using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CuratorsHelper
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    /// 

    class UserId
    {
        static int id;
        static string role;
        public static int ID
        {
            get { return id; }
            set { id = value; }
        }

        public static string Role
        {
            get { return role; }
            set { role = value; }
        }
    }

    public partial class App : Application
    {
    }
}
