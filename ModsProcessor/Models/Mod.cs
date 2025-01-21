using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsProcessor.Models
{
    public class Mod
    {
        public ModInfo ModInfo { get; set; }
        public string DirPath { get; set; }
        public string ReadMe { get; set; }
        public bool IsInstalled { get; set; }

        public static Mod ParseEnteryLua(string enteryLua)
        {
            Mod mod = new Mod();
            return mod;
        }
    }
}
