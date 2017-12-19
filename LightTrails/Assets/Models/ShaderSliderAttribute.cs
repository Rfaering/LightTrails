using Assets.Projects.Scripts;
using System;
using UnityEngine;

namespace Assets.Models
{
    public class ShaderSliderAttribute : SliderAttribute
    {
        public ShaderSliderAttribute(string displayName, string shaderPropertyName, float min, float max, Material material)
        {
            Name = displayName;
            CallBack = value => material.SetFloat(shaderPropertyName, value);
            SelectedValue = material.GetFloat(shaderPropertyName);
            Min = min;
            Max = max;
        }
    }
}
