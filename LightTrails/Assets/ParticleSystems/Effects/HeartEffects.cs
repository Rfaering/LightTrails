using UnityEngine;

public class HeartEffects : MonoBehaviour
{
    [Range(0, 3)]
    public float Intensity = 0.5f;

    // Update is called once per frame
    void Update()
    {
        var ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = Intensity;
    }
}
