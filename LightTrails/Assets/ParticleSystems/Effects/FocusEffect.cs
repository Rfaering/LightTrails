using UnityEngine;
using System;

public class FocusEffect : MonoBehaviour
{
    [Range(1, 3)]
    public float Speed = 2;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var ps = transform.GetChild(1).GetComponent<ParticleSystem>();
        var correctSpeed = 4 - Speed;

        var main = ps.main;
        var emission = ps.emission;
        main.startLifetime = 0.5f * correctSpeed;
        emission.rateOverTime = 8.0f / correctSpeed;

    }
}
