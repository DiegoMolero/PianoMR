using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject lvl1;
    private GameObject lvl_aux;

    public TextMesh audio_info;
    // Use this for initialization
    void Start () {
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
        if (GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes)
        {
            audio_info.text = "M\nU\nT\nE\n\nN\nO\nT\nE\nS";
        }
        else
        {
            audio_info.text = "\nU\nN\nM\nU\nT\nE\n\nN\nO\nT\nE\nS";
        }
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
                    lvl_aux.transform.position = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().position;
                    lvl_aux.transform.rotation = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().rotation;
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();
                    break;
                case PianoDriver.KeyNote.DO2:
                    if (GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes)
                    {
                        GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes = false;
                        audio_info.text = "\nU\nN\nM\nU\nT\nE\n\nN\nO\nT\nE\nS";
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes = true;
                        audio_info.text = "M\nU\nT\nE\n\nN\nO\nT\nE\nS";
                    }
                    break;
            }
        }
    }
}
