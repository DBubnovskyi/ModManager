﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModsProcessor;
using ModsProcessor.Models;

namespace ConsoleApp
{
    internal class Program
    {
        const string modPath    = @"D:\Projects\Pet\ModManager\Mods";
        const string targetPath = @"D:\Projects\Pet\ModManager\Target";
        static void Main()
        {
            var f = new FileProcessor();
            var m = f.LoadMods(modPath);
            foreach (var mod in m)
            {
                Console.WriteLine($"{mod.ModInfo?.DisplayName} {mod.IsInstalled(targetPath)}");
            }
            Console.ReadKey();
        }
    }
}
