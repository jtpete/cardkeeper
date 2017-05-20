using System;
using System.Collections.Generic;
using System.Text;

namespace cardkeeper.Helpers
{
    static class Converter
    {


        public static double ConvertStringToDouble(string value)
        {
            double dec;
            if (double.TryParse(value as string, out dec))
                return Math.Round(dec, 2);
            else
                return 0;
        }
    }
}
