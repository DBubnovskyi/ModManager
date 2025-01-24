using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModsProcessor.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace ModsProcessor.Models
{
    public class Mod
    {
        public Mod(FileStructure fs)
        {
            ModInfo = new ModInfo();
            var entryLua = FindEntryLua(fs);
            if (entryLua != null)
            {
                ModInfo = ParseEnteryLua(entryLua.Data);
            }
            else
            {
                ModInfo.DisplayName = fs.Name;
            }

            ModInfo.DisplayName = ModInfo.DisplayName ?? fs.Name;
        }

        public ModInfo ModInfo { get; set; }
        public string DirPath { get; set; }
        public string ReadMe { get; set; }
        public FileStructure FileStructure { get; set; }
        public bool IsInstalled { get; set; }

        public ModInfo ParseEnteryLua(string enteryLua)
        {
            var info = new ModInfo();
            if (enteryLua != null)
            {
                string[] lines = enteryLua.Split('\n');
                foreach (var line in lines)
                {
                    info.SelfId = line.GetInQuotesByKey("self_ID") ?? info.SelfId;
                    info.DisplayName = line.GetInQuotesByKey("displayName") ?? info.DisplayName;
                    info.ShortName = line.GetInQuotesByKey("shortName") ?? info.ShortName;
                    info.Version = line.GetInQuotesByKey("version") ?? info.Version;
                    info.FileMenuName = line.GetInQuotesByKey("fileMenuName") ?? info.FileMenuName;
                    info.UpdateId = line.GetInQuotesByKey("update_id") ?? info.UpdateId;
                    info.DevName = line.GetInQuotesByKey("developerName") ?? info.DevName;
                    info.IconName = line.GetInQuotesByKey("image") ?? info.IconName;
                    info.Info = line.GetInQuotesByKey("info") ?? info.Info;
                }
                return info;
            }
            return null;
        }

        public string ParseLine(string line, string key) => line.Contains(key) ? key : line.GetInQuotes();

        public FileStructure FindEntryLua(FileStructure fs)
        {
            foreach (var file in fs.Structure)
            {
                if (file.Name == "entry.lua")
                {
                    return file;
                }
                if (file.Structure != null)
                {
                    var buff = FindEntryLua(file);
                    if (buff != null)
                    {
                        return buff;
                    }
                }
            }
            return null;
        }

        public bool Install()
        {

            return true;
        }

        public bool Uninstall()
        {

            return true;
        }

        public static string GetTextWithinQuotes(string input)
        {
            int firstQuoteIndex = input.IndexOf('\"');
            if (firstQuoteIndex == -1)
                return string.Empty;

            int secondQuoteIndex = input.IndexOf('\"', firstQuoteIndex + 1);
            if (secondQuoteIndex == -1)
                return string.Empty;

            return input.Substring(firstQuoteIndex + 1, secondQuoteIndex - firstQuoteIndex - 1);

        }
    }
}