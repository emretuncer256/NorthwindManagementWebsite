using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.MVC.UI.Helper
{
    public static class Helper
    {
        public static string RandomBSColor()
        {
            Random random = new Random();
            int val = random.Next(1, 9);
            switch (val)
            {
                case 1:
                    return "primary";
                case 2:
                    return "secondary";
                case 3:
                    return "success";
                case 4:
                    return "danger";
                case 5:
                    return "warning";
                case 6:
                    return "info";
                case 7:
                    return "light";
                case 8:
                    return "dark";
                default:
                    return "primary";
            }
        }
        public static string StringShorter(string text, int maxVal, bool threeDots = true)
        {
            if (text.Length > maxVal)
            {
                return threeDots == true
                    ? text.Substring(0, maxVal) + "..."
                    : text.Substring(0, maxVal);
            }
            return text;
        }
    }
}
