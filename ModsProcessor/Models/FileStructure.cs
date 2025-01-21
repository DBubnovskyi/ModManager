﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsProcessor.Models
{
    public class FileStructure
    {
        public enum Type
        {
            File,
            Folder
        }

        public FileStructure(){ }
        public FileStructure(string name)
        {
            Name = name;
        }
        public FileStructure(string name, string structure)
        {
            Name = name;
            ParseStructure(structure);
        }

        public string Name { get; set; }
        public Type StructType { get; set; }
        public List<FileStructure> Structure { get; set; } = new List<FileStructure>();

        public void ParseStructure(string structure)
        {
            var parts = structure.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            FileStructure current = this;

            foreach (var part in parts)
            {
                var existing = current.Structure.Find(item => item.Name == part);

                if (existing == null)
                {
                    var newItem = new FileStructure
                    {
                        Name = part,
                        StructType = part.Contains('.') ? Type.File : Type.Folder
                    };

                    current.Structure.Add(newItem);
                    current = newItem;
                }
                else
                {
                    current = existing;
                }
            }
        }

        public override string ToString()
        {
            return ToString(0);
        }

        private string ToString(int indentationLevel)
        {
            var indent = new string(' ', indentationLevel * 2);
            var result = $"{indent}{Name} ({StructType})\n";

            foreach (var item in Structure)
            {
                result += item.ToString(indentationLevel + 1);
            }

            return result;
        }
    }
}
