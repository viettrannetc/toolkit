using System.Configuration;

namespace CreateWorkPackages3.Configuration
{
    public class ApplicationSettings: ConfigurationElement
    {
        [ConfigurationProperty("AreaPathKey", IsRequired = true)]
        public string AreaPathKey
        {
            get => (string)this["AreaPathKey"];
            set => value = (string)this["AreaPathKey"];
        }

        [ConfigurationProperty("ApplicationId", IsRequired = true)]
        public int ApplicationId
        {
            get => (int)this["ApplicationId"];
            set => value = (int)this["ApplicationId"];
        }
    }
}
