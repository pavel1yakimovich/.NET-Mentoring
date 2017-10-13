﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Task1AdvancedCSharp
{
    public class FsvArgs : EventArgs
    {
        public string Name { get; private set; }

        public FsvArgs(string name = null)
        {
            this.Name = name;
        }

        public void ExcludeFileOrDirectory()
        {
            FileSystemVisitor.ItemExcluded = true;
        }

        public void Stop()
        {
            FileSystemVisitor.IsStoped = true;
        }
    }

    public class FileSystemVisitor
    {
        public string RootDirectory { get; set; }
        public static bool ItemExcluded = false;
        public static bool IsStoped = false;
        private Predicate<string> filter;

        #region events

        public event EventHandler<FsvArgs> SearchStarted;
        public event EventHandler<FsvArgs> SearchFinished;

        public event EventHandler<FsvArgs> FileFound;
        public event EventHandler<FsvArgs> DirectoryFound;
        public event EventHandler<FsvArgs> FilteredFileFound;
        public event EventHandler<FsvArgs> FilteredDirectoryFound;

        protected void OnStart(FsvArgs args)
        {
            SearchStarted?.Invoke(this, args);
        }

        protected void OnFinish(FsvArgs args)
        {
            SearchFinished?.Invoke(this, args);
        }

        protected void OnFileFound(FsvArgs args)
        {
            FileFound?.Invoke(this, args);
        }

        protected void OnDirectoryFound(FsvArgs args)
        {
            DirectoryFound?.Invoke(this, args);
        }

        protected void OnFilteredFileFound(FsvArgs args)
        {
            FilteredFileFound?.Invoke(this, args);
        }

        protected void OnFilteredDirectoryFound(FsvArgs args)
        {
            FilteredDirectoryFound?.Invoke(this, args);
        }

        #endregion

        public FileSystemVisitor(string rootDirectory)
        {
            this.RootDirectory = rootDirectory;
            ItemExcluded = false;
            IsStoped = false;
        }
        /// <summary>
        /// .ctor with filter delegate
        /// </summary>
        /// <param name="filter">Should return true, if you exclude file or directory</param>
        public FileSystemVisitor(string rootDirectory, Predicate<string> filter) : this(rootDirectory)
        {
            this.filter = filter;
        }

        public IEnumerator<string> GetEnumerator()
        {
            if (String.IsNullOrEmpty(RootDirectory))
            {
                if (RootDirectory == null)
                {
                    throw new ArgumentNullException("rootDirectory");
                }
                else
                {
                    throw new ArgumentException("RootDoriectory is an empty string!");
                }
            }
            if (IsStoped)
            {
                goto Finish;
            }
            OnStart(new FsvArgs());

            var directories = Directory.GetDirectories(RootDirectory);

            //Searching in directories
            foreach (var dir in directories)
            {
                if (IsStoped)
                {
                    goto Finish;
                }
                OnDirectoryFound(new FsvArgs(dir));
                if (IsStoped)
                {
                    goto Finish;
                }
                //if the dir is excluded, do not search in this dir
                if (ReturnItem(dir))
                {
                    OnFilteredDirectoryFound(new FsvArgs(dir));
                    if (IsStoped)
                    {
                        goto Finish;
                    }

                    yield return dir;

                    FileSystemVisitor subFsv;
                    if (filter != null)
                    {
                        subFsv = new FileSystemVisitor(dir, filter);
                    }
                    else
                    {
                        subFsv = new FileSystemVisitor(dir);
                    }
                    this.SubscribeToSubFsvEvents(subFsv);

                    //Searching in subdirectories
                    foreach (var item in subFsv)
                    {
                        if (IsStoped)
                        {
                            goto Finish;
                        }
                        yield return item;
                    }
                }
            }

            var files = Directory.GetFiles(RootDirectory);

            //Return files
            foreach (var file in files)
            {
                this.OnFileFound(new FsvArgs(file));
                if (IsStoped)
                {
                    goto Finish;
                }
                if (ReturnItem(file))
                {
                    this.OnFilteredFileFound(new FsvArgs(file));
                    if (IsStoped)
                    {
                        goto Finish;
                    }
                    yield return file;
                }
            }

            Finish:
            OnFinish(new FsvArgs());
        }
        #region private methods

        private bool ReturnItem(string item)
        {
            bool result = (this.filter == null || !this.filter(item)) && !ItemExcluded;
            ItemExcluded = false;
            return result;
        }

        private void SubscribeToSubFsvEvents(FileSystemVisitor subFsv)
        {
            subFsv.DirectoryFound += (object s, FsvArgs e) => this.OnDirectoryFound(e);
            subFsv.FileFound += (object s, FsvArgs e) => this.OnFileFound(e);
            subFsv.FilteredDirectoryFound += (object s, FsvArgs e) => this.OnFilteredDirectoryFound(e);
            subFsv.FilteredFileFound += (object s, FsvArgs e) => this.OnFilteredFileFound(e);
        }

        #endregion
    }
}