using Assets.Projects.Scripts;
using System;
using System.Collections.Generic;

namespace Assets.UI.Models
{
    public class OptionsAttribute : Attribute
    {
        public Action<string> CallBack;

        public List<string> Options;

        public Action<string> Changed
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

        public string SelectedValue;

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
            SelectedValue = (string)value.Value;
            CallBack(SelectedValue);
        }
    }
}
