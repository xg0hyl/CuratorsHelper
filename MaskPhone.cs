using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CuratorsHelper
{
    public class MaskPhone
    {
        public static void Preview(TextBox GroupText, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
                return;
            }

            var text = GroupText.Text;

            if (text.Length == 0)
            {
                GroupText.Text = "+375 ";
                GroupText.CaretIndex = 5;
                e.Handled = true;
                return;
            }

            if (text.Length == 5 && e.Text[0] != ' ' && e.Text[0] != '(' && e.Text[0] != ')')
            {
                GroupText.Text = "+375 (" + e.Text;
                GroupText.CaretIndex = 9;
                e.Handled = true;
                return;
            }

            if (text.Length == 8 && e.Text[0] != ' ' && e.Text[0] != '(' && e.Text[0] != ')')
            {
                GroupText.Text = text + ") " + e.Text;
                GroupText.CaretIndex = 11;
                e.Handled = true;
                return;
            }

            if (text.Length == 13 && e.Text[0] != '-')
            {
                GroupText.Text = text + "-" + e.Text;
                GroupText.CaretIndex = 15;
                e.Handled = true;
                return;
            }

            if (text.Length == 16 && e.Text[0] != '-')
            {
                GroupText.Text = text + "-" + e.Text;
                GroupText.CaretIndex = 18;
                e.Handled = true;
                return;
            }

            if (text.Length >= 19)
            {
                e.Handled = true;
                return;
            }
        }

        public static void Changed(TextBox GroupText, TextChangedEventArgs e)
        {
            var text = GroupText.Text;

            if (text.StartsWith("375") && text.Length <= 17)
            {
                var formattedText = "+375";

                if (text.Length >= 4)
                {
                    formattedText += " (" + text.Substring(3, 2) + ")";
                }

                if (text.Length >= 7)
                {
                    formattedText += " " + text.Substring(6, 2);
                }

                if (text.Length >= 10)
                {
                    formattedText += "-" + text.Substring(9, 3);
                }

                if (text.Length >= 14)
                {
                    formattedText += "-" + text.Substring(13, 2);
                }

                GroupText.Text = formattedText;
                GroupText.CaretIndex = GroupText.Text.Length;
            }
        }
    }

}
