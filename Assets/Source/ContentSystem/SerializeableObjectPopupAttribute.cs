using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class SerializeableObjectPopupAttribute : PropertyAttribute
    {
        public string Path;

        public SerializeableObjectPopupAttribute(string path)
        {
            Path = path;
        }
    }
}
