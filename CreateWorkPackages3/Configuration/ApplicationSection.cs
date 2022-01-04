using System;
using System.Configuration;

namespace CreateWorkPackages3.Configuration
{
    class ApplicationSection: ConfigurationSection
    {
        [ConfigurationProperty("Applications", IsDefaultCollection = true)]
        public ApplicationSettingsCollection Applications
        {
            get { return (ApplicationSettingsCollection)this["Applications"]; }
            set { this["Applications"] = value; }
        }
    }
}
