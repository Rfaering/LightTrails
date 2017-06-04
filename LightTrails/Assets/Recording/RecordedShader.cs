using UnityEngine;

public partial class Record : MonoBehaviour
{
    public class RecordedShader : IRecordedEffect
    {
        public bool Looping { get; private set; }

        public Material Material { get; private set; }
        public ShaderEffect ShaderEffect { get; private set; }

        public float Length { get; private set; }
        public float ElapsedTime { get; private set; }

        public RecordedShader(ShaderEffect shaderEffect, float length)
        {
            ShaderEffect = shaderEffect;
            shaderEffect.TimeRunning = false;
            Material = shaderEffect.Material;
            Material.SetFloat("_InputTime", 0);
            Length = length;
            ElapsedTime = 0;
        }

        public void Progress(float deltaTime)
        {
            ElapsedTime += deltaTime;
            Material.SetFloat("_InputTime", ElapsedTime);
        }

        public void Destroy()
        {
            Material.SetFloat("_InputTime", 0);
            ShaderEffect.TimeRunning = true;
        }
    }
}




