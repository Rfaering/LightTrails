using System;

namespace Assets.UI.Models
{
    public class Effect
    {
        public string Name;
        public Type MenuItemType;
        public bool Loop;

        public static Effect Create(string name, Type typeOfMenuItem = null, bool loop = false)
        {
            return new Effect { Name = name, MenuItemType = typeOfMenuItem, Loop = loop };
        }
    }
}
