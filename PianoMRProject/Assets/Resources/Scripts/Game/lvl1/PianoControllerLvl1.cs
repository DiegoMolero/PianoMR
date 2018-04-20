using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoController : MonoBehaviour {
    public GameObject _DO,_DO_A,_RE,_RE_A,_MI,_FA,_FA_A,_SOL,_SOL_A,_LA,_LA_A,_SI,_DO2;
    public Hashtable keys = new Hashtable();

    private void Awake()
    {
        keys.Add(PianoDriver.KeyNote.DO, _DO);  //DO
        keys.Add(PianoDriver.KeyNote.DO_A, _DO_A);  //DO#
        keys.Add(PianoDriver.KeyNote.RE, _RE);  //RE
        keys.Add(PianoDriver.KeyNote.RE_A, _RE_A);  //RE#
        keys.Add(PianoDriver.KeyNote.MI, _MI);  //MI
        keys.Add(PianoDriver.KeyNote.FA, _FA);  //FA
        keys.Add(PianoDriver.KeyNote.FA_A, _FA_A);  //FA#
        keys.Add(PianoDriver.KeyNote.SOL, _SOL);  //SOL
        keys.Add(PianoDriver.KeyNote.SOL_A, _SOL_A);  //SOL#
        keys.Add(PianoDriver.KeyNote.LA, _LA);  //LA
        keys.Add(PianoDriver.KeyNote.LA_A, _LA_A);  //LA#
        keys.Add(PianoDriver.KeyNote.SI, _SI);  //SI
        keys.Add(PianoDriver.KeyNote.DO2, _DO2);  //DO'
    }

    // Use this for initialization
    void Start () {
            GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PianoActionRecieved(PianoDriver.KeyNote key,bool action)
    {
        try
        {
            //Debug.Log(key + " " + action);
            GameObject aux = (GameObject)keys[key];
            aux.GetComponent<PianoKey>().ActionRecieved(action);
        }
        catch (Exception e) {

        }
        
    }
}
