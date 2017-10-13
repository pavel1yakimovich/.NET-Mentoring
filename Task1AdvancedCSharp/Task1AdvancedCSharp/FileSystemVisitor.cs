using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Task1AdvancedCSharp
{
    public class FsvArgs : EventArgs
    {
    }

    public class FileSystemVisitor
    {
        public string RootDirectory { get; set; }
        
        private Predicate<string> filter;

        public event EventHandler<FsvArgs> SearchStarted;
        public event EventHandler<FsvArgs> SearchFinished;

        protected void OnStart(FsvArgs args)
        {
            SearchStarted?.Invoke(this, args);
        }

        protected void OnFinish(FsvArgs args)
        {
            SearchFinished?.Invoke(this, args);
        }

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
            OnStart(new FsvArgs());

            var directories = Directory.GetDirectories(RootDirectory);

            //Searching in directories
            foreach (var dir in directories)
            {
                //if the dir is excluded, do not search in this dir
                if (ReturnItem(dir))
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
                        if (ReturnItem(item))
                        {
                            yield return item;
                        }
                    }
                }
            }

            var files = Directory.GetFiles(RootDirectory);

            //Return files
            foreach (var file in files)
            {
                if (ReturnItem(file))
                {
                    yield return file;
                }
            }

            OnFinish(new FsvArgs());
        }
        #region private methods

        private bool ReturnItem(string item)
        {
            return this.filter == null || !this.filter(item);
        }

        #endregion
    }
}
