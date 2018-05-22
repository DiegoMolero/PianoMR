using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTriggers : MonoBehaviour {
    public List<GameObject> Triggers;

    private GameObject aux;
	// Use this for initialization
	void Awake () {
		foreach (GameObject aux_triggers in Triggers)
        {
            aux = Instantiate(aux_triggers,this.transform);
            aux.name = aux.transform.name.Replace("(Clone)", "");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
