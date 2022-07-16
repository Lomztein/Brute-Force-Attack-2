using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public static class CustomContentUtils
    {
        public static string CustomContentPath => Path.Combine (Paths.PersistantData, "Content", "Custom");

        internal static bool TryGenerateCustomContentPack()
        {
            if (Directory.Exists(CustomContentPath)) return false;
            ContentPackInfo info = new ContentPackInfo();

            info.Name = "Custom";
            info.Author = "You";
            info.Version = "1.0.0";
            info.Description = "Custom content created by you <3";

            try
            {
                Directory.CreateDirectory(CustomContentPath);
                File.WriteAllText(Path.Combine(CustomContentPath, "About.json"), ObjectPipeline.UnbuildObject(info, true).ToString());
                return true;
            } catch (IOException exc)
            {
                Debug.LogException(exc);
                return false;
            }
        }

        public static string ToAbsolutePath(string contentPath)
        {
            return Path.Combine(CustomContentPath, contentPath);
        }

        public static void WriteFile(string contentPath, string contents)
        {
            string path = ToAbsolutePath(contentPath);
            CreateDirectory(Path.GetDirectoryName(contentPath));
            File.WriteAllText(path, contents);
        }

        public static void CreateDirectory(string contentDirectoryPath)
        {
            Directory.CreateDirectory(ToAbsolutePath(contentDirectoryPath));
        }

        public static string ReadFile(string contentPath)
        {
            return File.ReadAllText(ToAbsolutePath(contentPath));
        }

        public static IEnumerable<string> GetFiles(string contentPath)
        {
            return Directory.GetFiles(ToAbsolutePath(contentPath));
        }
    }
}
