using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ParticlePicker : MonoBehaviour
{
    public List<ShowCaseScript> ParticleSystems;
    private Dropdown dropDown;
    private ShowCaseScript _lastSelection;

    // Use this for initialization
    void Start()
    {
        var particleList = FindObjectOfType<ParticleList>();
        var list = particleList.GetComponentsInChildren<ShowCaseScript>(true);
        ParticleSystems.AddRange(list);

        dropDown = GetComponent<Dropdown>();
        dropDown.ClearOptions();
        dropDown.AddOptions(list.Select(MapToOption).ToList());

        dropDown.onValueChanged.AddListener(SelectionChanged);
        dropDown.Select();
    }

    private void SelectionChanged(int index)
    {
        var newSelection = ParticleSystems.FirstOrDefault(x => x.gameObject.name == dropDown.options[index].text);

        if (_lastSelection != null)
        {
            _lastSelection.gameObject.SetActive(false);
        }

        newSelection.gameObject.SetActive(true);
        _lastSelection = newSelection;
    }

    private Dropdown.OptionData MapToOption(ShowCaseScript system)
    {
        return new Dropdown.OptionData()
        {
            text = system.gameObject.name
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
