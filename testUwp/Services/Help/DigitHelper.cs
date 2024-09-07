using System.Text.RegularExpressions;

namespace testUwp.Services.Help
{
    public class DigitHelper
    {
        private static string _pattern = "^[0-9]+(?:\\,[0-9]*)?$";

        public static bool IsDigit(string value)
        {
            var reg = new Regex(_pattern);

            return reg.IsMatch(value);
        }

        public static string MakeDigit(string value)
        {
            while (!double.TryParse(value, out double _) && value.Length > 0)
            {
                value = value.Remove(value.Length - 1, 1);
            }

            return value;
        }
    }
}
