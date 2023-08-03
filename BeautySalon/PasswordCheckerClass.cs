using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon
{
    public class PasswordCheckerClass
    {
        public static bool ValidatePassword(string password)
        {
            if (password.Length < 5 || password.Length > 9)
                return false;
            if (!password.Any(Char.IsLower))
                return false;
            if (password.All(Char.IsUpper))
                return false;
            if (password.All(Char.IsDigit))
                return false;

            return true;
        }
    }
}
