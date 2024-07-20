using System.Text.RegularExpressions;

namespace Application.Helpers
{
    public static class ScriptContentValidator
    {
        public static bool NotContainScript(string input)
        {
            if (string.IsNullOrEmpty(input)) return true;

            string pattern = @"<.*?>|&.*?;"; // Example pattern for basic script tags and HTML entities
            return !Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }
    }
}
