using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuReadyManagerImportLvl : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PianoActionRecieved(PianoDriver.KeyNote key, bool action)
    {
        if (action == true) //If the key is being pressed
        {
            switch (key)
            {
                case PianoDriver.KeyNote.SOL:
                    this.transform.parent.GetComponent<StateImportManager>().NextState();
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
