using UnityEngine;

public abstract class RunningEffect : MonoBehaviour
{
    public float Length { get; internal set; }
    public float ElapsedTime { get; internal set; }

    public abstract void Initialize(float newLength);
    public abstract void Progress(float newElapsedTime);
}
