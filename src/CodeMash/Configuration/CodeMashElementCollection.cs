using System.Configuration;

namespace CodeMash
{
    public class CodeMashElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        public CodeMashConfigurationElement this[object key]
        {
            get { return this.BaseGet(key) as CodeMashConfigurationElement; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CodeMashConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CodeMashConfigurationElement)element).Name;
        }

        protected override bool IsElementName(string elementName)
        {
            return !string.IsNullOrEmpty(elementName) && elementName.Equals("client");
        }
    }
}