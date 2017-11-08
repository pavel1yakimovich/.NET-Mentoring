using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Module4BCL
{
    class Program
    {
        private static int count;
        static void Main(string[] args)
        {
            List<string> folders;
            List<RuleElement> rules;
            ConfigurateApp(out folders, out rules);
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "D:\\testfolder";
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            count++;
        }

        private static void ConfigurateApp(out List<string> folders, out List<RuleElement> rules)
        {
            var asm = Assembly.GetExecutingAssembly();
            var section = (ConfigurationManager.GetSection("fileSystemWatcher") as FileSystemWatcherConfigurationSection);

            folders = new List<string>();
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
