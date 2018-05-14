using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerImportLvl : MonoBehaviour
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
                case PianoDriver.KeyNote.DO:
                    this.transform.parent.GetComponent<StateImportManager>().ChangeState(StateImportManager.State.Playing);
                    Destroy(this.gameObject);
                    break;
                case PianoDriver.KeyNote.RE:
                    this.transform.parent.GetComponent<StateImportManager>().ChangeState(StateImportManager.State.ScanQR);
                    Destroy(this.gameObject);
                    break;
                case PianoDriver.KeyNote.DO2:
                    this.transform.parent.GetComponent<StateImportManager>().ChangeState(StateImportManager.State.Exit);
                    break;
            }
        }
    }
}
