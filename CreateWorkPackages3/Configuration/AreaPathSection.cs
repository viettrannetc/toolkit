using System.Configuration;

namespace CreateWorkPackages3.Configuration
{
    class AreaPathSection: ConfigurationSection
    {
        [ConfigurationProperty("AreaPaths", IsDefaultCollection = true)]
        public AreaPathSettingsCollection AreaPaths
        {
            get { return (AreaPathSettingsCollection)this["AreaPaths"]; }
            set { this["AreaPaths"] = value; }
        }
    }
}
