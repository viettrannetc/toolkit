using System.Configuration;

namespace CreateWorkPackages3.Configuration
{
    [ConfigurationCollection(typeof(ApplicationSettings), AddItemName = "ApplicationSettings")]
    public class ApplicationSettingsCollection: ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ApplicationSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApplicationSettings)element).AreaPathKey;
        }
    }
}
