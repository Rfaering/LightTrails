using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddEffectMenuItem : MonoBehaviour, IPointerClickHandler
{
    private Record recorder;

    private bool IsDisabled { get { return recorder.ActivelyRecording; } }

    public Attribute[] Attributes;

    private void Start()
    {
        recorder = FindObjectOfType<Record>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (IsDisabled)
        {
            return;
        }

        EffectOptionsOverlay.RenderedEffectType = Assets.Models.Effect.EffectKind.Particle;
        SceneManager.LoadScene("Scenes/Effects", LoadSceneMode.Additive);
    }

    void Update()
    {
        GetComponent<Button>().interactable = !IsDisabled;
    }
}
