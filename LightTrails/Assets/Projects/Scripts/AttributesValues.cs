using System;

namespace Assets.Projects.Scripts
{
    [Serializable]
    public class AttributesValues
    {
        public AttributeValue[] Values;
    }

    [Serializable]
    public class AttributeValue
    {
        public string Key;
        public object Value;
    }
}
