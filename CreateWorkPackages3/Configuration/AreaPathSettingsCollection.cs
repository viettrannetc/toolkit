using System.Configuration;

namespace CreateWorkPackages3.Configuration
{
    [ConfigurationCollection(typeof(AreaPathSettings), AddItemName = "AreaPathSettings")]
    public class AreaPathSettingsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AreaPathSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AreaPathSettings)element).AreaPathKey;
        }
    }
}
