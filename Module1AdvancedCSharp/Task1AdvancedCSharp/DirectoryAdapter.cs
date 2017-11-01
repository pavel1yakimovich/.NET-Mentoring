using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task1AdvancedCSharp
{
    public class DirectoryAdapter : IDirectoryAdapter
    {
        public string[] GetDirectories(string path) => Directory.GetDirectories(path);

        public string[] GetFiles(string path) => Directory.GetFiles(path);
    }
}
