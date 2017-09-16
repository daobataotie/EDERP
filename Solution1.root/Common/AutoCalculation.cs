using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class AutoCalculation
    {
        public static int Calculation(string type, int quantity)
        {
            int i = 1;
            if (string.IsNullOrEmpty(type) || quantity < 1)
                return i;

            if (type.ToLower().Contains("ansi") ||type.ToLower().Contains("csa"))
            {
                if (quantity <= 1000)
                    i = 2;
                else if (quantity <= 1500)
                    i = 3;
                else if (quantity <= 2000)
                    i = 4;
                else if (quantity <= 2500)
                    i = 5;
                else if (quantity <= 3000)
                    i = 6;
                else if (quantity <= 3500)
                    i = 7;
                else if (quantity <= 4000)
                    i = 8;
                else if (quantity <= 4500)
                    i = 9;
                else if (quantity <= 5000)
                    i = 10;
                else if (quantity <= 5500)
                    i = 11;
                else if (quantity <= 6000)
                    i = 12;
                else
                    i = 12;
            }
            else if (type.ToLower().Contains("en"))
            {
                if (quantity <= 1500)
                    i = 3;
                else if (quantity <= 2000)
                    i = 4;
                else if (quantity <= 2500)
                    i = 5;
                else if (quantity <= 3000)
                    i = 6;
                else if (quantity <= 3500)
                    i = 7;
                else if (quantity <= 4000)
                    i = 8;
                else if (quantity <= 4500)
                    i = 9;
                else if (quantity <= 5000)
                    i = 10;
                else if (quantity <= 5500)
                    i = 11;
                else if (quantity <= 6000)
                    i = 12;
                else
                    i = 12;
            }
            else if (type.ToLower().Contains("as"))
            {
                if (quantity <= 2000)
                    i = 4;
                else if (quantity <= 2500)
                    i = 5;
                else if (quantity <= 3000)
                    i = 6;
                else if (quantity <= 3500)
                    i = 7;
                else if (quantity <= 4000)
                    i = 8;
                else if (quantity <= 4500)
                    i = 9;
                else if (quantity <= 5000)
                    i = 10;
                else if (quantity <= 5500)
                    i = 11;
                else if (quantity <= 6000)
                    i = 12;
                else
                    i = 12;
            }
            else if (type.ToLower().Contains("jis"))
            {
                if (quantity <= 500)
                    i = 1;
                else if (quantity <= 1000)
                    i = 2;
                else if (quantity <= 1500)
                    i = 3;
                else if (quantity <= 2000)
                    i = 4;
                else if (quantity <= 2500)
                    i = 5;
                else if (quantity <= 3000)
                    i = 6;
                else if (quantity <= 3500)
                    i = 7;
                else if (quantity <= 4000)
                    i = 8;
                else if (quantity <= 4500)
                    i = 9;
                else if (quantity <= 5000)
                    i = 10;
                else if (quantity <= 5500)
                    i = 11;
                else if (quantity <= 6000)
                    i = 12;
                else
                    i = 12;
            }          

            return i;
        }
    }
}
