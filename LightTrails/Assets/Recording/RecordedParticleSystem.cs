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

                var newMain = Origin.main;
                newMain.loop = false;
                newMain.duration = length;

                FirstHalf = Instantiate(system);
                SecondHalf = Instantiate(system);

                SetDefaultValues(FirstHalf.gameObject, length);
                SetDefaultValues(SecondHalf.gameObject, length);

                FirstHalf.gameObject.SetActive(true);
                SecondHalf.gameObject.SetActive(true);

                FirstHalf.Simulate(0.0f, true, true);
                FirstHalf.Simulate(Length / 2.0f, true, false);

                SecondHalf.Simulate(0.0f, true, true);
            }
        }

        private void SetDefaultValues(GameObject gameObject, float length)
        {
            foreach (var ps in gameObject.GetComponentsInChildren<ParticleSystem>(true))
            {
                ps.Clear();
                ps.Stop();
                ps.randomSeed = 42;

                var main = ps.main;
                main.loop = false;
                main.duration = length;
                main.playOnAwake = false;
            }
        }

        public void Progress(float deltaTime)
        {
            if (Looping)
            {
                FirstHalf.Simulate(deltaTime, true, false);
                ElapsedTime += deltaTime;
                if (ElapsedTime > (Length / 2.0f))
                {
                    SecondHalf.Simulate(deltaTime, true, false);
                }
            }
            else
            {
                Origin.Simulate(deltaTime, true, false);
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




