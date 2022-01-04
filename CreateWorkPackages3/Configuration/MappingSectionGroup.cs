using System.Configuration;

namespace CreateWorkPackages3.Configuration
{
    class MappingSectionGroup: ConfigurationSectionGroup
    {
        [ConfigurationProperty("SprintSection", IsRequired = false)]
        public SprintSection ContextSettings
        {
            get { return (SprintSection)base.Sections["SprintSection"]; }
        }
    }
}
