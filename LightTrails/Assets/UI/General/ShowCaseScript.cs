using UnityEngine;
using System.Linq;

public class ShowCaseScript : MonoBehaviour
{
    public bool IsLooping = false;
    private ParticleSystem _particleSystem;

    // Use this for initialization
    void Start()
    {
        /*_particleSystem = GetComponent<ParticleSystem>();

        var allParticleSystems = gameObject.GetComponentsInChildren<ParticleSystem>(true);
        var maxDuration = allParticleSystems.Max(x => x.main.startLifetime.constantMax);
        foreach (var ps in allParticleSystems)
        {
            ps.Clear();
            ps.Stop();
            ps.randomSeed = 42;

            var main = ps.main;
            main.loop = false;
            main.duration = 10 - maxDuration;
        }

        _particleSystem.Play(true);*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!_particleSystem.isPlaying)
        {
            _particleSystem.Play(true);
        }*/
    }
}