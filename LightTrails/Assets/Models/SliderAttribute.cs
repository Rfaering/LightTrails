using Assets.Projects.Scripts;
using System;
using UnityEngine;

namespace Assets.Models
{
    public class SliderAttribute : Attribute
    {
        public float Min = 0;
        public float Max = 100;

        private float _selectedValue;
        public float SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                _selectedValue = value;
                if (CallBack != null)
                {
                    CallBack(SelectedValue);
                }
            }
        }

        public Action<float> Changed
        {
            get
            {
                return newSelection => SelectedValue = newSelection;
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
