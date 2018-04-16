using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ActivatePianoVuforia : MonoBehaviour
{
    public GameObject piano;

    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
    }

    public void PianoActionRecieved(int key, bool action)
    {

        if (this.isActiveAndEnabled)
        {
            if (key == 67 && action == false)
            {
                GameObject aux = Instantiate(piano);
                aux.name = aux.transform.name.Replace("(Clone)", "");
                aux.transform.position = this.transform.position;
                aux.transform.rotation = this.transform.rotation;
                GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();
            }
        }

    }
}
