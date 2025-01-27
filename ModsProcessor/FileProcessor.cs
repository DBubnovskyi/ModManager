using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ModsProcessor.Models;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Zip;
using ModsProcessor.Helpers;

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
                string[] archiveFiles = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(file => file.EndsWith(".zip") || file.EndsWith(".rar") || file.EndsWith(".7z"))
                    .ToArray();

                foreach (string dir in directories)
                {
                    var fs = CreateFileStructure(dir);
                    mods.Add(new Mod(fs)
                    {
                        DirPath = dir
                    });
                }

                foreach (string archiveFile in archiveFiles)
                {
                    var fs = CreateFileStructureFromArchive(archiveFile);
                    mods.Add(new Mod(fs)
                    {
                        DirPath = archiveFile
                    });
                }
            }

            return mods;
        }

        private FileStructure CreateFileStructure(string dir)
        {
            string dirName = Path.GetFileName(dir);
            var fs = new FileStructure(dirName);
            foreach (string file in Directory.GetFiles(dir, "*", SearchOption.AllDirectories))
            {
                string relativePath = file.Replace(dir, string.Empty);
                fs.Files.Add(relativePath);
                fs.ParseStructure(relativePath);
            }
            fs.Structure[0].Files = fs.Files;
            return fs.Structure[0];
        }

        private FileStructure CreateFileStructureFromArchive(string archiveFile)
        {
            using (var archive = OpenArchive(archiveFile))
            {
                var fs = new FileStructure(Path.GetFileName(archiveFile));
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        fs.Files.Add(entry.Key);
                        var file = fs.ParseStructure(entry.Key);
                        if (entry.Key.Contains("entry.lua"))
                        {
                            using (var memoryStream = new MemoryStream())
                            using (var entryStream = entry.OpenEntryStream())
                            {
                                entryStream.CopyTo(memoryStream);
                                memoryStream.Seek(0, SeekOrigin.Begin);
                                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                                {
                                    string content = reader.ReadToEnd();
                                    file.Data = content;
                                }
                            }
                        }
                    }
                }
                return fs;
            }
        }

        private dynamic OpenArchive(string archiveFile)
        {
            string extension = Path.GetExtension(archiveFile).ToLower();
            switch (extension)
            {
                case ".zip":
                    return ZipArchive.Open(archiveFile);
                case ".rar":
                    return RarArchive.Open(archiveFile);
                case ".7z":
                    return SevenZipArchive.Open(archiveFile);
                default:
                    throw new NotSupportedException("Unsupported archive format");
            }
        }

    }
}
