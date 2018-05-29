using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject MenuLevels;
    public GameObject LvlImport;
    public GameObject Score;
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
                    lvl_aux = Instantiate(MenuLevels);
                    lvl_aux.name = lvl_aux.transform.name.Replace("(Clone)", "");
                    lvl_aux.transform.position = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().position;
                    lvl_aux.transform.rotation = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().rotation;
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();
                    break;
                case PianoDriver.KeyNote.SOL:
                    lvl_aux = Instantiate(LvlImport);
                    lvl_aux.name = lvl_aux.transform.name.Replace("(Clone)", "");
                    lvl_aux.transform.position = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().position;
                    lvl_aux.transform.rotation = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().rotation;
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();
                    break;
                case PianoDriver.KeyNote.DO2:
                    lvl_aux = Instantiate(Score);
                    lvl_aux.name = lvl_aux.transform.name.Replace("(Clone)", "");
                    lvl_aux.transform.position = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().position;
                    lvl_aux.transform.rotation = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().rotation;
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();
                    break;
            }
        }
    }
}
