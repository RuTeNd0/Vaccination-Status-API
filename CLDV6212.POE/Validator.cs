using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLDV6212.POE
{
    internal class Validator
    {

        public static bool CheckID(string ID)
        {
            string digitsOnly = new string(ID.Where(char.IsDigit).ToArray());

            return digitsOnly.Length == 13;
        }

        public static bool CheckPassport(string ID)
        {
            if (ID.Any(char.IsLetter))
            {
                string digitsOnly = new string(ID.Where(char.IsDigit).ToArray());

                return digitsOnly.Length == 8;
            }

            return false;
        }


    }
}
