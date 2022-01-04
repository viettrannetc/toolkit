using System.Configuration;

namespace CreateWorkPackages3.Configuration
{
    public class AreaPathSettings: ConfigurationElement
    {
        [ConfigurationProperty("AreaPathKey", IsRequired = true)]
        public string AreaPathKey
        {
            get => (string)this["AreaPathKey"];
            set => value = (string)this["AreaPathKey"];
        }

        [ConfigurationProperty("TeamId", IsRequired = true)]
        public int TeamId
        {
            get => (int)this["TeamId"];
            set => value = (int)this["TeamId"];
        }

        [ConfigurationProperty("RelatedCaseId", IsRequired = true)]
        public int RelatedCaseId
        {
            get => (int)this["RelatedCaseId"];
            set => value = (int)this["RelatedCaseId"];
        }
    }
}
