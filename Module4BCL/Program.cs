using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Module4BCL
{
    class Program
    {
        private static int count;
        private static List<RuleElement> rules = new List<RuleElement>();
        private static List<string> folders = new List<string>();
        static void Main(string[] args)
        {
            ConfigurateApp();
            foreach (var folder in folders)
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = folder;
                watcher.NotifyFilter = NotifyFilters.LastAccess |
                         NotifyFilters.LastWrite |
                         NotifyFilters.FileName |
                         NotifyFilters.DirectoryName;
                watcher.Filter = "*.*";
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnChanged);
                watcher.EnableRaisingEvents = true;
            }
            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            count++;
            foreach (var rule in rules)
            {
                if (new Regex(rule.Template).IsMatch(e.Name.Split('.')[0]))
                {
                    var newPath = e.FullPath.Insert(e.FullPath.IndexOf(e.Name), $"{rule.Folder}\\");
                    new FileInfo(newPath).Directory.Create();
                    File.Move(e.FullPath, newPath);
                }
            }
        }

        private static void ConfigurateApp()
        {
            var asm = Assembly.GetExecutingAssembly();
            var section = (ConfigurationManager.GetSection("fileSystemWatcher") as FileSystemWatcherConfigurationSection);

            foreach (var item in section.Folders)
            {
                folders.Add((item as FolderElement)?.Name);
            }

            rules = new List<RuleElement>();
            foreach (var item in section.Rules)
            {
                rules.Add(item as RuleElement);
            }

            Thread.CurrentThread.CurrentCulture = section.Culture;
            Thread.CurrentThread.CurrentUICulture = section.Culture;
        }
    }
}
