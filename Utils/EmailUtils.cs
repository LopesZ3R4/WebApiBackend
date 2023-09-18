// .\utils\EmailUtils.cs
using System.Text.RegularExpressions;
namespace Utils
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)*@[a-zA-Z0-9]+(\.[a-zA-Z]{1,3}){1,2}$";
            Regex rgx = new Regex(pattern);
            
            return rgx.IsMatch(email);
        }
    }
}