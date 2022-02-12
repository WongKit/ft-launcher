using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FT_Launcher {
    public class CustomConfigSectionElement : ConfigurationElement {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get { return (string)this["name"]; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url {
            get { return (string)this["url"]; }
        }

        [ConfigurationProperty("launchFile")]
        public string LaunchFile {
            get { return (string)this["launchFile"]; }
        }

        [ConfigurationProperty("launchArgs")]
        public string LaunchArgs {
            get { return (string)this["launchArgs"]; }
        }
    }

    [ConfigurationCollection(typeof(CustomConfigSectionElement), AddItemName = "downloadUrl")]
    public class CustomConfigSectionCollection : ConfigurationElementCollection {
        protected override ConfigurationElement CreateNewElement() {
            return new CustomConfigSectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((CustomConfigSectionElement)element).Name;
        }
    }

    public class CustomConfigSection : ConfigurationSection {
        [ConfigurationProperty("downloadUrls", IsDefaultCollection = true)]
        public CustomConfigSectionCollection NodeList {
            get { return (CustomConfigSectionCollection)this["downloadUrls"]; }
        }
    }
}