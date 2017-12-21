using Assets.Projects.Scripts;
using System;
using UnityEngine;

namespace Assets.Models
{
    public class SizeAttribute : Attribute
    {
        public bool Resizeable = false;

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
                _width = value;
                SizeHasChanged();
            }
        }

        private float _height;
        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
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
                var result = SizeChanged(new Vector2(_width, _height));
                _width = result.x;
                _height = result.y;
            }
        }

        public void OffsetHasChanged()
        {
            if (OffSetChanged != null)
            {
                var result = OffSetChanged(new Vector2(X, Y));
                _x = result.x;
                _y = result.y;
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

        public Func<Vector2, Vector2> SizeChanged;
        public Func<Vector2, Vector2> OffSetChanged;

        public override AttributeValue GetAttributeValue()
        {
            return new AttributeValue()
            {
                Key = Name,
                Value = new float[] { X, Y, _width, _height }
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

                AllHasChanged();
            }
        }
    }
}
