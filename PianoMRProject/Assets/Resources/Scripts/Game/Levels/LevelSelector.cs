using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {
    public GameObject Lvl;
    private GameObject lvl_aux;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadLvl()
    {
        lvl_aux = Instantiate(Lvl);
        lvl_aux.name = lvl_aux.transform.name.Replace("(Clone)", "");
        lvl_aux.transform.position = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().position;
        lvl_aux.transform.rotation = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().rotation;
    }
}
