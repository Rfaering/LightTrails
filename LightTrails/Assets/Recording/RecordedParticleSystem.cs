using System;
using UnityEngine;
using System.Linq;

public partial class Record : MonoBehaviour
{
    public class RecordedParticleSystem : IRecordedEffect
    {
        public bool Looping { get; private set; }

        public ParticleSystem Origin { get; private set; }
        public ParticleSystem FirstHalf { get; private set; }
        public ParticleSystem SecondHalf { get; private set; }

        public bool FirstHasStarted = false;
        public bool SecondHasStarted = false;

        public float Length { get; private set; }
        public float ElapsedTime { get; private set; }

        public RecordedParticleSystem(ParticleSystem system, float length)
        {
            Length = length;
            ElapsedTime = 0;

            var showCaseScript = system.GetComponent<ShowCaseScript>();
            Looping = false;// showCaseScript.IsLooping;

            Origin = system;

            SetDefaultValues(Origin.gameObject, length);
            Origin.Simulate(0, true, true);

            if (Looping)
            {
                Origin.gameObject.SetActive(false);

                FirstHalf = Instantiate(system);
                SecondHalf = Instantiate(system);

                SetDefaultValues(FirstHalf.gameObject, length);
                SetDefaultValues(SecondHalf.gameObject, length);

                FirstHalf.gameObject.SetActive(true);
                SecondHalf.gameObject.SetActive(true);

                FirstHalf.Simulate(0.0f, true, true);
                FirstHalf.Simulate(Length / 2.0f, true, false);

                SecondHalf.Simulate(0.0f, true, true);

                FirstHasStarted = false;
                SecondHasStarted = false;

                EnsureCorrectActiveState(FirstHalf, true);
                EnsureCorrectActiveState(SecondHalf, false);
            }
            else
            {

            }
        }

        private void SetDefaultValues(GameObject gameObject, float length)
        {
            /*var allParticleSystems = gameObject.GetComponentsInChildren<ParticleSystem>(true);
            var maxDuration = allParticleSystems.Max(x => x.main.startLifetime.constantMax);
            foreach (var ps in allParticleSystems)
            {
                ps.Clear();
                ps.Stop();
                ps.randomSeed = 42;

                var main = ps.main;
                main.loop = false;
                main.duration = length - maxDuration;
            }*/
        }

        public void Progress(float newElapsedTime)
        {
            var deltaTime = newElapsedTime - ElapsedTime;

            if (Looping)
            {
                if (ElapsedTime > (Length / 2.0f))
                {
                    EnsureCorrectActiveState(FirstHalf, true);
                    EnsureCorrectActiveState(SecondHalf, true);

                    FirstHalf.Simulate(newElapsedTime + Length / 2.0f, true, true);
                    SecondHalf.Simulate(newElapsedTime - Length / 2.0f, true, true);
                }
                else
                {
                    EnsureCorrectActiveState(FirstHalf, true);
                    EnsureCorrectActiveState(SecondHalf, false);
                    FirstHalf.Simulate(newElapsedTime + Length / 2.0f, true, true);
                }
            }
            else
            {
                if (deltaTime >= 0)
                {
                    Origin.Simulate(deltaTime, true, false);
                }
                else
                {
                    Origin.Simulate(newElapsedTime, true, true);
                }
            }

            ElapsedTime = newElapsedTime;
        }

        private void EnsureCorrectActiveState(ParticleSystem ps, bool enabled)
        {
            if (ps.gameObject.activeSelf != enabled)
            {
                ps.gameObject.SetActive(enabled);
            }
        }

        public void Destroy()
        {
            if (Origin != null)
            {
                Origin.gameObject.SetActive(false);
                Origin.gameObject.SetActive(true);
            }

            if (Looping && FirstHalf != null && SecondHalf != null)
            {
                DestroyImmediate(FirstHalf.gameObject);
                DestroyImmediate(SecondHalf.gameObject);
            }
        }
    }
}




