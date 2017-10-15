using Assets.Projects.Scripts;
using System;
using UnityEngine;

namespace Assets.Models
{
    public class SizeAttribute : Attribute
    {
        private float _x;
        public float X
        {
            get { return _x; }
            set
            {
                _x = value;
                OffsetHasChanged();
            }
        }

        private float _y;

        public float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OffsetHasChanged();
            }
        }

        public void SetOffSet(float x, float y)
        {
            _x = x;
            _y = y;
            OffsetHasChanged();
        }

        private float _width;
        public float Width
        {
            get { return _width; }
            set
            {
                float ratio = _height / (float)_width;

                _width = value;

                if (IsLinked)
                {
                    _height = (int)(ratio * _width);
                }

                SizeHasChanged();
            }
        }

        private float _height;
        public float Height
        {
            get { return _height; }
            set
            {
                float ratio = _width / (float)_height;

                _height = value;

                if (IsLinked)
                {
                    _width = (int)(ratio * _height);
                }

                SizeHasChanged();
            }
        }

        public void SetSize(float width, float height)
        {
            _height = height;
            _width = width;
            SizeHasChanged();
        }

        public void AllHasChanged()
        {
            OffsetHasChanged();
            SizeHasChanged();
        }

        public void SizeHasChanged()
        {
            if (SizeChanged != null)
            {
                SizeChanged(new Vector2(_width, _height));
            }
        }

        public void OffsetHasChanged()
        {
            if (OffSetChanged != null)
            {
                OffSetChanged(new Vector2(X, Y));
            }
        }

        public float ForceWidth
        {
            set { _width = value; }
        }

        public float ForceHeight
        {
            set { _height = value; }
        }


        public bool IsLinked = true;

        public Action<Vector2> SizeChanged;
        public Action<Vector2> OffSetChanged;

        public override AttributeValue GetAttributeValue()
        {
            return new AttributeValue()
            {
                Key = Name,
                Value = new float[] { X, Y, _width, _height, IsLinked ? 1 : 0 }
            };
        }

        public override void SetAttributeValue(AttributeValue value)
        {
            if (value.Value is Array)
            {
                var array = (float[])value.Value;
                X = array[0];
                Y = array[1];
                _width = array[2];
                _height = array[3];
                IsLinked = array[4] > 0;

                AllHasChanged();
            }
        }
    }
}
