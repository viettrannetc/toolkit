using System.Configuration;

namespace CreateWorkPackages3.Configuration
{
    class SprintSection: ConfigurationSection
    {
        [ConfigurationProperty("Sprints", IsDefaultCollection = true)]
        public SprintSettingsCollection Sprints
        {
            get { return (SprintSettingsCollection)this["Sprints"]; }
            set { this["Sprints"] = value; }
        }
    }
}
