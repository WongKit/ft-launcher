using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FT_Launcher {
    public class DownloadUrlElement : ConfigurationElement {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("launchFile")]
        public string LaunchFile {
            get { return (string)this["launchFile"]; }
            set { this["launchFile"] = value; }
        }

        [ConfigurationProperty("launchArgs")]
        public string LaunchArgs {
            get { return (string)this["launchArgs"]; }
            set { this["launchArgs"] = value; }
        }
    }

    [ConfigurationCollection(typeof(DownloadUrlElement), AddItemName = "downloadUrl")]
    public class DownloadUrlElementCollection : ConfigurationElementCollection {
        protected override ConfigurationElement CreateNewElement() {
            return new DownloadUrlElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((DownloadUrlElement)element).Name;
        }
    }

    public class CustomConfigSection : ConfigurationSection {
        [ConfigurationProperty("downloadUrls", IsDefaultCollection = true)]
        public DownloadUrlElementCollection NodeList {
            get { return (DownloadUrlElementCollection)this["downloadUrls"]; }
        }
    }
}