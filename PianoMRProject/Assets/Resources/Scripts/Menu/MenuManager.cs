using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject lvl1;

    private GameObject lvl_aux;
	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PianoActionRecieved(PianoDriver.KeyNote key, bool action)
    {
        if (action == true) //If the key is being pressed
        {
            switch (key)
            {
                case PianoDriver.KeyNote.DO:
                    lvl_aux = Instantiate(lvl1);
                    lvl_aux.name = lvl_aux.transform.name.Replace("(Clone)", "");
                    lvl_aux.transform.position = this.transform.position;
                    lvl_aux.transform.rotation = this.transform.rotation;
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();
                    break;

            }
        }
    }
}
