using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsProcessor.Helpers
{
    public static class StringExtensions
    {
        public static string GetInQuotesByKey(this string input, string key) => input.Contains(key) ? input.GetInQuotes() : null;
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

        public static bool CopyFolderTo(this string sourcePath, string targetPath)
        {
            if (!Directory.Exists(sourcePath))
            {
                return false;
            }

            try
            {
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
