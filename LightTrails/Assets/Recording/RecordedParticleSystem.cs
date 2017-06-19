using UnityEngine;

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
            Looping = showCaseScript.IsLooping;

            Origin = system;
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

                SecondHalf.Simulate(0.0f, true, true);

                FirstHasStarted = false;
                SecondHasStarted = false;
            }
        }

        private void SetDefaultValues(GameObject gameObject, float length, bool prewarm = true)
        {
            foreach (var ps in gameObject.GetComponentsInChildren<ParticleSystem>(true))
            {
                ps.Clear();
                ps.Stop();
                ps.randomSeed = 42;

                var main = ps.main;
                main.loop = true;
                main.prewarm = false;
                main.duration = length;
                main.playOnAwake = false;
            }
        }

        public void Progress(float deltaTime)
        {
            if (Looping)
            {
                if (ElapsedTime > (Length / 2.0f))
                {
                    if (!SecondHasStarted)
                    {
                        EnsureCorrectActiveState(FirstHalf, true);
                        EnsureCorrectActiveState(SecondHalf, true);

                        SecondHalf.Simulate(0.0f, true, true);
                        SecondHasStarted = true;
                    }

                    FirstHalf.Simulate(deltaTime, true, false);
                    SecondHalf.Simulate(deltaTime, true, false);
                }
                else
                {
                    if (!FirstHasStarted)
                    {
                        EnsureCorrectActiveState(FirstHalf, true);
                        EnsureCorrectActiveState(SecondHalf, false);

                        FirstHalf.Simulate(0.0f, true, true);
                        FirstHalf.Simulate(Length / 2.0f, true, false);

                        FirstHasStarted = true;

                        foreach (var ps in FirstHalf.GetComponentsInChildren<ParticleSystem>(true))
                        {
                            var main = ps.main;
                            main.loop = false;
                        }
                    }

                    FirstHalf.Simulate(deltaTime, true, false);
                }

                ElapsedTime += deltaTime;
            }
            else
            {
                Origin.Simulate(deltaTime, true, false);
            }
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
            Origin.gameObject.SetActive(false);
            Origin.gameObject.SetActive(true);

            if (Looping)
            {
                DestroyImmediate(FirstHalf.gameObject);
                DestroyImmediate(SecondHalf.gameObject);
            }
        }
    }
}




