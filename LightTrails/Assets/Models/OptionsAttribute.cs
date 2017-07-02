using Assets.Projects.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Models
{
    public class OptionsAttribute<T> : OptionsAttribute
    {
        public Action<T> SpecificCallBack;

        public T SpecificSelectedValue
        {
            get
            {
                return (T)Enum.Parse(typeof(T), SelectedValue);
            }
            set
            {
                SelectedValue = Enum.GetName(typeof(T), value);
                if (CallBack != null)
                {
                    CallBack(SelectedValue);
                }
            }
        }

        public OptionsAttribute()
        {
            Options = Enum.GetNames(typeof(T)).ToList();
            CallBack = selectedValue =>
            {
                var value = (T)Enum.Parse(typeof(T), selectedValue);
                SpecificCallBack(value);
            };
        }
    }

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
