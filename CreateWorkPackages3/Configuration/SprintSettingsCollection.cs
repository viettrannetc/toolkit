using System.Configuration;

namespace CreateWorkPackages3.Configuration
{

    [ConfigurationCollection(typeof(SprintSettings), AddItemName = "SprintSettings")]
    public class SprintSettingsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SprintSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SprintSettings)element).SprintKey;
        }
    }
}
