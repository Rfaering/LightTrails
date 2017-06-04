using System;

namespace Assets.UI.Models
{
    public class Effect
    {
        public enum EffectKind { Particle, Shader }

        public string Name;
        public Type MenuItemType;
        public bool Loop;
        public EffectKind Type;

        public static Effect Create(string name, EffectKind effectType, Type typeOfMenuItem = null, bool loop = false)
        {
            return new Effect
            {
                Name = name,
                MenuItemType = typeOfMenuItem,
                Loop = loop,
                Type = effectType
            };
        }
    }
}
