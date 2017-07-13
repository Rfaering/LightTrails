using Assets.Models;
using Assets.Projects.Scripts;
using System.Linq;
using UnityEngine;

public class ItemsMenu : MonoBehaviour
{
    public GameObject ParticleEffectPrefab;
    public GameObject ShaderEffectPrefab;

    void Start()
    {
        ItemSelected(FindObjectOfType<RecorderMenuItem>());
        LoadEffects();

        MaskPanel.EnsureLoaded();
    }

    internal void SelectFirstItem()
    {
        ItemSelected(GetComponentsInChildren<MenuItem>().First());
    }

    internal void LoadEffects()
    {
        if (Project.CurrentModel != null)
        {
            var names = EffectOptions.Options.ToDictionary(x => x.Name);
            foreach (var item in Project.CurrentModel.Items.Effects)
            {
                if (names.ContainsKey(item.Name))
                {
                    var menuItem = AddEffect(names[item.Name]);
                    menuItem.SetEffectSaveState(item);
                }
            }
        }
    }

    internal EffectMenuItem AddEffect(Effect effect)
    {
        var addEffectItem = transform.Find("AddEffect");
        addEffectItem.SetParent(addEffectItem.root);

        GameObject newGameObject = null;
        EffectMenuItem effectMenuItem = null;

        if (effect.Type == Effect.EffectKind.Particle)
        {
            newGameObject = Instantiate(ParticleEffectPrefab);
            newGameObject.transform.SetParent(transform);

            if (effect.MenuItemType != null)
            {
                effectMenuItem = newGameObject.AddComponent(effect.MenuItemType) as ParticleEffectMenuItem;
            }
            else
            {
                effectMenuItem = newGameObject.AddComponent<ParticleEffectMenuItem>();
            }

        }
        else if (effect.Type == Effect.EffectKind.Shader)
        {
            var otherShader = GetComponentsInChildren<ShaderEffectMenuItem>().FirstOrDefault();
            if (otherShader != null)
            {
                otherShader.Remove();
            }


            newGameObject = Instantiate(ShaderEffectPrefab);
            newGameObject.transform.SetParent(transform);

            if (effect.MenuItemType != null)
            {
                effectMenuItem = newGameObject.AddComponent(effect.MenuItemType) as ShaderEffectMenuItem;
            }
            else
            {
                effectMenuItem = newGameObject.AddComponent<ShaderEffectMenuItem>();
            }
        }

        newGameObject.name = effect.Name;
        newGameObject.GetComponent<EffectMenuItem>().Initialize(effect);
        addEffectItem.SetParent(transform);
        newGameObject.GetComponent<EffectMenuItem>().SelectEffect();

        return effectMenuItem;
    }

    public void ItemSelected(MenuItem element)
    {
        MaskPanel.Close();

        foreach (var item in GetComponentsInChildren<MenuItem>())
        {
            if (item != element)
            {
                item.HasBeenUnSelected();
            }

        }

        element.HasBeenSelected();

        FindObjectOfType<AttributesMenu>().CreateProperties(element.Attributes);

        var draggableSystem = Resources.FindObjectsOfTypeAll<DraggableParticleSystem>().First();
        draggableSystem.gameObject.SetActive(false);
    }

    public EffectMenuItem GetSelectedItem()
    {
        return FindObjectsOfType<EffectMenuItem>().FirstOrDefault(x => x.Selected);
    }
}
