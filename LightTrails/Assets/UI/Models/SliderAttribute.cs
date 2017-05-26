using Assets.Projects.Scripts;
using System;

namespace Assets.UI.Models
{
    public class SliderAttribute : Attribute
    {
        public float Min = 0;
        public float Max = 100;
        public float SelectedValue = 100;

        public Action<float> Changed
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

        public Action<float> CallBack;

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
            SelectedValue = (float)value.Value;
            CallBack(SelectedValue);
        }
    }
}
