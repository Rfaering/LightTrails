using FfmpegWrapper;
using UnityEngine;
using System.Linq;
using System.Collections;

public class Record : MonoBehaviour
{
    public bool Recording = false;

    public float TimeBetweenFrames = 1.0f / 25.0f;

    public float ElapsedVideoTime = 0;
    private float RecordingTime = 10;

    public bool FirstFrame = false;

    public GameObject ProgressBar;

    public int Frame = 0;

    public RecordedParticleSystem[] RecordingParticleSystems;

    void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.Close();
#endif
    }

    private void OnPreRender()
    {
        if (!Recording)
        {
            return;
        }
    }

    void OnPostRender()
    {
        if (!Recording)
        {
            return;
        }

        if (FirstFrame)
        {
            Frame = 0;
            FirstFrame = false;
        }
        else
        {
            ElapsedVideoTime += TimeBetweenFrames;
            foreach (var ps in RecordingParticleSystems)
            {
                ps.Progress(TimeBetweenFrames);
            }
        }

        if (ElapsedVideoTime > RecordingTime)
        {
            StopRecording();
            foreach (var ps in RecordingParticleSystems)
            {
                ps.Destroy();
            }

            RecordingParticleSystems = null;
            return;
        }

        var imagePicker = FindObjectOfType<ImageAreaPicker>();
        Rect rect = imagePicker.GetRect();
        int width = (int)rect.width;
        int height = (int)rect.height;

        Texture2D lOut = new Texture2D(width, height, TextureFormat.ARGB32, false);
        lOut.ReadPixels(rect, 0, 0);

        Frame++;
        var bytes = lOut.GetRawTextureData();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.SetFrameThroughBitMap(bytes, width, height);
#endif
        Destroy(lOut);
    }

    void Update()
    {
        if (Recording)
        {
            var videoProgressbar = ProgressBar.GetComponent<VideoProgressBar>();
            videoProgressbar.Show();
            videoProgressbar.SetProgress((ElapsedVideoTime / RecordingTime));
        }
        else
        {
            ProgressBar.GetComponent<VideoProgressBar>().Hide();
        }
    }

    public void StartRecording(int recordingTime, OutputFormat format)
    {
        Recording = true;
        FirstFrame = true;
        ElapsedVideoTime = 0;
        RecordingTime = recordingTime;

        var recorderMenuItem = FindObjectOfType<RecorderMenuItem>();

        var activeParticleList = FindObjectOfType<ActiveParticleList>().gameObject;

        var allParticleSystems = activeParticleList
                                    .GetComponentsInChildren<ParticleSystem>()
                                    .Where(x => x.transform.parent == activeParticleList.transform);

        RecordingParticleSystems = allParticleSystems.Select(ps => new RecordedParticleSystem(ps, RecordingTime)).ToArray();

        var videoFileName = recorderMenuItem.VideoFileName();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.Start(new Settings()
        {
            Fps = Fps.fps24,
            InputImageFormat = InputImageFormat.Bmp,
            OutputFormat = format,
            OutputFile = videoFileName
        });
#endif

        AttributeMenuItem.RefreshButtonEnabledState();
    }

    public void StopRecording()
    {
        ElapsedVideoTime = 0;
        Recording = false;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.Close();
#endif
        AttributeMenuItem.RefreshButtonEnabledState();
    }

    IEnumerator WaitAndClose()
    {
        yield return new WaitForSeconds(2);
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Recorder.Close();
#endif
        AttributeMenuItem.RefreshButtonEnabledState();
    }

    public class RecordedParticleSystem
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




