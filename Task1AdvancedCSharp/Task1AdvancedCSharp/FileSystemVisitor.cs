using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Task1AdvancedCSharp
{
    public class FileSystemVisitor
    {
        public string RootDirectory { get; set; }
        
        private Predicate<string> filter;

        public FileSystemVisitor() { }
        /// <summary>
        /// .ctor with filter delegate
        /// </summary>
        /// <param name="filter">Should return true, if exclude file or directory</param>
        public FileSystemVisitor(Predicate<string> filter)
        {
            this.filter = filter;
        }

        public IEnumerator<string> GetEnumerator()
        {
            var directories = Directory.GetDirectories(RootDirectory);
            var files = Directory.GetFiles(RootDirectory);

            //Searching in directories
            foreach (var dir in directories)
            {
                //if the dir is excluded, do not search in this dir
                if (filter == null || !filter(dir))
                {
                    yield return dir;

                    FileSystemVisitor fsv;
                    if (filter != null)
                    {
                        fsv = new FileSystemVisitor(filter);
                    }
                    else
                    {
                        fsv = new FileSystemVisitor();
                    }
                    fsv.RootDirectory = dir;

                    //Searching in subdirectories
                    foreach (var item in fsv)
                    {
                        if (filter == null || !filter(item))
                        {
                            yield return item;
                        }
                    }
                }
            }

            //Return files
            foreach (var file in files)
            {
                if (filter == null || !filter(file))
                {
                    yield return file;
                }
            }
        }
    }
}
