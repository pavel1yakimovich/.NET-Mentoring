using System;
using System.Collections;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Globalization;

namespace Module4BCL
{ 
    public class FileSystemWatcherConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("culture")]
        public CultureInfo Culture
        {
            get => (CultureInfo)(this["culture"]);
            set { this["culture"] = value; }
        }

        [ConfigurationProperty("folders")]
        [ConfigurationCollection(typeof(FoldersElementCollectoin), AddItemName = "folder")]
        public FoldersElementCollectoin Folders
        {
            get => (FoldersElementCollectoin)(this["folders"]);
            set { this["folders"] = value; }
        }

        [ConfigurationProperty("rules")]
        [ConfigurationCollection(typeof(FoldersElementCollectoin), AddItemName = "rule")]
        public RulesElementCollectoin Rules
        {
            get => (RulesElementCollectoin)(this["rules"]);
            set { this["rules"] = value; }
        }
    }

    public class FoldersElementCollectoin : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as FolderElement).Name;
        }
    }

    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }
    }

    public class RulesElementCollectoin : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as RuleElement);
        }
    }

    public class RuleElement : ConfigurationElement
    {

        [ConfigurationProperty("template", IsKey = true)]
        public string Template
        {
            get => (string)(this["template"]);
            set { this["template"] = value; }
        }

        [ConfigurationProperty("folder")]
        public string Folder
        {
            get => (string)(this["folder"]);
            set { this["folder"] = value; }
        }

        [ConfigurationProperty("addIndex")]
        public bool AddIndex
        {
            get => (bool)(this["addIndex"]);
            set { this["addIndex"] = value; }
        }

        [ConfigurationProperty("addDate")]
        public bool AddDate
        {
            get => (bool)(this["addDate"]);
            set { this["addDate"] = value; }
        }
    }
}
