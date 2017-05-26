using Assets.Projects.Scripts;
using System;

namespace Assets.UI.Models
{
    public class Attribute
    {
        public string Name;
        public Func<bool> IsEnabled = () => true;

        public virtual AttributeValue GetAttributeValue()
        {
            return null;
        }

        public virtual void SetAttributeValue(AttributeValue value)
        {
        }
    }
}
