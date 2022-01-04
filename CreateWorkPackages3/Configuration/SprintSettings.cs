using System.Configuration;

namespace CreateWorkPackages3.Configuration
{
    public class SprintSettings: ConfigurationElement
    {
        [ConfigurationProperty("SprintKey", IsRequired = true)]
        public string SprintKey
        {
            get => (string)this["SprintKey"];
            set => value = (string)this["SprintKey"];
        }

        [ConfigurationProperty("SprintId", IsRequired = true)]
        public int SprintId
        {
            get => (int)this["SprintId"];
            set => value = (int)this["SprintId"];
        }

        [ConfigurationProperty("DueDate", IsRequired = true)]
        public string DueDate
        {
            get => (string)this["DueDate"];
            set => value = (string)this["DueDate"];
        }
       
    }
}

