using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Module4BCL
{
    class Program
    {
        private List<string> folders;
        static void Main(string[] args)
        {
            List<string> folders;
            ConfigurateApp(out folders);
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
            if (e.Name.Contains("."))
            {
                Console.WriteLine("File found");


            }
        }

        private static void ConfigurateApp(out List<string> folders)
        {
            folders = new List<string>();
            var asm = Assembly.GetExecutingAssembly();
            var section = (ConfigurationManager.GetSection("fileSystemWatcher") as FileSystemWatcherConfigurationSection);
            foreach (var item in section.Folders)
                Console.WriteLine((item as FolderElement).Name);
            var culture = section.Culture;
        }
    }
}
