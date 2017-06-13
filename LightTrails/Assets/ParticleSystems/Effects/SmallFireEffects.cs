using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFireEffects : MonoBehaviour {
    public bool Sparkles;
	// Update is called once per frame
	void Update () {
        var sparkles = transform.Find("embers");
        sparkles.gameObject.SetActive(Sparkles);
	}
}
