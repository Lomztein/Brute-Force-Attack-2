﻿using Lomztein.BFA2.ContentSystem.Loaders;
using Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentPack : IContentPack
    {
        private const string IGNORE_PREFIX = "IGNORE_"; // Any files prefixed with this will be ignored.
        private readonly string _path;

        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }

        private IContentLoader _contentLoader = new ContentLoader();

        public ContentPack(string path, string name, string author, string description)
        {
            _path = path;
            Name = name;
            Author = author;
            Description = description;
        }

        public object GetContent(string path, Type type)
        {
            return _contentLoader.LoadContent(_path + path, type);
        }

        public object[] GetAllContent(string path, Type type)
        {
            List<object> content = new List<object>();
            string spath = _path + path;

            if (Directory.Exists(spath))
            {
                string[] files = Directory.GetFiles(spath, "*.json");
                foreach (string file in files)
                {
                    if (!Path.GetFileName(file).StartsWith(IGNORE_PREFIX))
                    {
                        try
                        {
                            var loaded = _contentLoader.LoadContent(file, type);
                            content.Add(loaded);
                        }catch (Exception exc)
                        {
                            Log.Error(exc);
                        }
                    }
                }
            }

            return content.ToArray();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
