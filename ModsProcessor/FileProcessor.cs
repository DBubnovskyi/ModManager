using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using ModsProcessor.Models;

namespace ModsProcessor
{
    public class FileProcessor
    {
        public List<Mod> LoadMods(string path)
        {
            List<Mod> mods = new List<Mod>();
            if (Directory.Exists(path))
            {
                string[] directories = Directory.GetDirectories(path);
                string[] zipFiles = Directory.GetFiles(path, "*.zip");
                string[] rarFiles = Directory.GetFiles(path, "*.rar");
                foreach (string dir in directories)
                {
                    var w = new FileStructure(Path.GetFileName(dir));
                    foreach (string file in Directory.GetFiles(dir, "*", SearchOption.AllDirectories))
                    {
                        //Console.WriteLine(file.Replace(path, ""));
                        w.ParseStructure(file.Replace(path, ""));
                    }
                    w = w.Structure[0];
                    Console.WriteLine(w);
                    //mods.Add(new Mod
                    //{
                    //     DirPath = dir,
                    //});
                }
                foreach (string zipfile in zipFiles)
                {

                    using (ZipArchive zip = ZipFile.Open(zipfile, ZipArchiveMode.Read))
                    {
                        //string[] s = new string[zip.Entries.Count];
                        //int i = 0;
                        var w = new FileStructure(Path.GetFileName(zipfile));
                        foreach (ZipArchiveEntry entry in zip.Entries)
                        {
                            w.ParseStructure(entry.FullName);
                            //PrintEntry(entry, "");
                            //s[i] = entry.FullName;
                            //if (entry.Name == "entry.lua")
                            //{
                            //    using (StreamReader reader = new StreamReader(entry.Open()))
                            //    {
                            //        string fileContent = reader.ReadToEnd();
                            //        //Console.WriteLine(fileContent);
                            //    }
                            //}
                            //i++;
                        }
                        Console.WriteLine(w);
                    }
                    //mods.Add(new Mod
                    //{
                    //    DirPath = zipfile,
                    //});
                }
                foreach (string dir in rarFiles)
                {
                    mods.Add(new Mod
                    {
                        DirPath = dir,
                    });
                }
            }

            return mods;
        }

        void ListFilesInZip(string zipPath)
        {
            var path = new DirectoryInfo(zipPath);
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                Console.WriteLine("");
                Console.WriteLine(path.Name);
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (!entry.FullName.EndsWith("/"))
                    {
                        PrintEntry(entry, "");
                    }
                }
            }
        }

        void PrintEntry(ZipArchiveEntry entry, string indent)
        {
            string[] parts = entry.FullName.Split('/');
            for (int i = 0; i < parts.Length - 1; i++)
            {
                Console.Write(indent + "|   ");
            }
            Console.WriteLine(indent + "|-- " + parts[parts.Length - 1]);
        }
    }
}
