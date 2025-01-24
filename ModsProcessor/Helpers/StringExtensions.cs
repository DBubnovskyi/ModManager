using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsProcessor.Helpers
{
    public static class StringExtensions
    {
        public static string GetInQuotes(this string input)
        {
            int firstQuoteIndex = input.IndexOf('\"');
            if (firstQuoteIndex == -1)
                return string.Empty;

            int secondQuoteIndex = input.IndexOf('\"', firstQuoteIndex + 1);
            if (secondQuoteIndex == -1)
                return string.Empty;

            return input.Substring(firstQuoteIndex + 1, secondQuoteIndex - firstQuoteIndex - 1);
        }

        public static string GetInQuotesByKey(this string input, string key) => input.Contains(key) ? input.GetInQuotes() : null;
    }
}
