using System;
using Assets.Projects.Scripts;

namespace Assets.Models
{
    public class ToggleAttribute : Attribute
    {
        public bool SelectedValue = false;

        public Action<bool> Changed
        {
            get
            {
                return newSelection =>
                {
                    SelectedValue = newSelection;

                    if (CallBack != null)
                    {
                        CallBack(newSelection);
                    }
                };
            }
        }

        public Action<bool> CallBack;

        public override AttributeValue GetAttributeValue()
        {
            return new AttributeValue()
            {
                Key = Name,
                Value = SelectedValue
            };
        }

        public override void SetAttributeValue(AttributeValue value)
        {
            SelectedValue = (bool)value.Value;
            CallBack(SelectedValue);
        }
    }
}
