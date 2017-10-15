using System;

namespace Assets.Models
{
    public class Effect
    {
        public enum EffectKind { Particle, Shader }

        public string Name;
        public Type MenuItemType;
        public EffectKind Type;

        public static Effect Create(string name, EffectKind effectType, Type typeOfMenuItem = null)
        {
            return new Effect
            {
                Name = name,
                MenuItemType = typeOfMenuItem,
                Type = effectType
            };
        }
    }
}
