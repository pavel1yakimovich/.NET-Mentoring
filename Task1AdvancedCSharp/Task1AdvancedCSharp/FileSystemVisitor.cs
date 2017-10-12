using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Task1AdvancedCSharp
{
    public class FileSystemVisitor
    {
        public string RootDirectory { get; set; }

        public IEnumerator<string> GetEnumerator()
        {
            var directories = Directory.GetDirectories(RootDirectory);
            var files = Directory.GetFiles(RootDirectory);
            foreach (var dir in directories)
            {
                yield return dir;
                var fsv = new FileSystemVisitor
                {
                    RootDirectory = dir
                };
                foreach (var item in fsv)
                {
                    yield return item;
                }
            }
            foreach (var file in files)
            {
                yield return file;
            }
        }
    }
}
