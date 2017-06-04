using Assets.UI.Models;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using Assets.Projects.Scripts;

public class ParticleEffectMenuItem : EffectMenuItem
{
    public override void Initialize(Effect effect)
    {
        EffectName = effect.Name;
        GetComponentInChildren<Text>().text = effect.Name;
        var child = Resources.FindObjectsOfTypeAll<ParticleList>().First().gameObject.transform.Find(effect.Name);
        var createdEffect = Instantiate(child, FindObjectOfType<ActiveParticleList>().gameObject.transform);
        createdEffect.gameObject.SetActive(true);

        assosicatedEffect = createdEffect.gameObject;
    }

    public override void HasBeenSelected()
    {
        var draggableSystem = Resources.FindObjectsOfTypeAll<DraggableParticleSystem>().First();
        draggableSystem.ConnectEffect(assosicatedEffect);
        draggableSystem.gameObject.SetActive(true);
    }

    public override void SetEffectSaveState(StoredEffectItem state)
    {
        assosicatedEffect.transform.localPosition = new Vector3(state.Position[0], state.Position[1], state.Position[2]);
        base.SetEffectSaveState(state);
    }

    public override void Remove()
    {
        Destroy(assosicatedEffect);
        base.Remove();
    }
}
