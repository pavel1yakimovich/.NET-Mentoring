using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using strings = Module4BCL.Resources.Strings;

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
                watcher.Filter = "*.*";
                watcher.Created += new FileSystemEventHandler(OnChanged);
                watcher.EnableRaisingEvents = true;
            }
            Console.WriteLine(strings.StringToFinish);
            while (Console.Read() != 'q') ;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!e.Name.Contains('.'))
            {
                return;
            }

            Console.WriteLine($"{strings.FileFound}{e.FullPath}");
            count++;
            bool ruleFound = false;
            foreach (var rule in rules)
            {
                StringBuilder fileNameWithoutExtension = new StringBuilder(Path.GetFileNameWithoutExtension(e.Name));
                if (new Regex(rule.Template).IsMatch(fileNameWithoutExtension.ToString()))
                {
                    ruleFound = true;
                    Console.WriteLine($"{strings.RuleFound}{rule.Template}");
                    var date = DateTime.Now;
                    StringBuilder newPath = new StringBuilder(Path.Combine(Path.GetDirectoryName(e.FullPath), rule.Folder));
                    StringBuilder newFileName = fileNameWithoutExtension;
                    newFileName = rule.AddDate ? newFileName.Append(date.ToString("m", strings.Culture.DateTimeFormat)) : newFileName;
                    newFileName = rule.AddIndex ? newFileName.Append($"_{count}") : newFileName;
                    newFileName.Append(Path.GetExtension(e.Name));
                    var newPathStr = Path.Combine(newPath.ToString(), newFileName.ToString());
                    new FileInfo(newPathStr).Directory.Create();
                    File.Move(e.FullPath, newPathStr);
                    Console.WriteLine($"{strings.FileReplaced}{newPathStr}");
                }
            }

            if (!ruleFound)
            {
                Console.WriteLine($"{strings.RuleNotFound}");
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

            strings.Culture = section.Culture;

            Thread.CurrentThread.CurrentCulture = section.Culture;
            Thread.CurrentThread.CurrentUICulture = section.Culture;
        }
    }
}
