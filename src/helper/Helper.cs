using System;
using System.Collections.Generic;
using System.Text;

namespace IRSACA.src.helper
{
    class Helper
    {
        public static string FormatPhone(string phoneNumber)
        {
            double phn;
            if (double.TryParse(phoneNumber, out phn)
                && phoneNumber.Length == 10)
            {
                return string.Format("{0:(###) ###-####}", phn);
            }
            else
            {
                return phoneNumber;
            }
        }

        public static string FormatEin(string ein)
        {

            return ein.Insert(2, "-");

        }

        public static string Formatss(string ss)
        {
            return ss.Insert(5, "-").Insert(3, "-");
        }

    }
}
