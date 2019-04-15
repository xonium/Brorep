namespace Brorep.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceInputUrlMinusSignWithSpace(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return input.Replace("-", " ");
        }
    }
}
