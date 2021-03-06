﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PianoDriver : MonoBehaviour {
    public GameObject HololensConnection;
    public GameObject UnityConnection;
    public PianoEventKey pianoEvent;

    //Indicates the note of the key pressed
    public enum KeyNote
    {
        DO = 60,
        DO_A = 61,
        RE = 62,
        RE_A = 63,
        MI = 64,
        FA = 65,
        FA_A = 66,
        SOL = 67,
        SOL_A = 68,
        LA = 69,
        LA_A = 70,
        SI = 71,
        DO2 = 72
    };

    public void RecievePianoData(string data)
    {
        string[] data_fragmented;
        bool aux_activate;
        int key_value;
        data_fragmented = data.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
        //Parse the hexadecimal value into int
        key_value = int.Parse(data_fragmented[1], System.Globalization.NumberStyles.HexNumber);
        //Check if the key is pressed or unpressed
        if (int.Parse(data_fragmented[2], System.Globalization.NumberStyles.HexNumber) == 0){
            aux_activate = false;
        }
        else{
            aux_activate = true;
        }
        //Send the key pressed to the piano controller
        try
        {
            pianoEvent.Invoke((KeyNote)key_value,aux_activate);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void Awake()
    {
        pianoEvent = new PianoEventKey();
#if UNITY_EDITOR
        GameObject aux = Instantiate(UnityConnection, transform);
        aux.name = aux.transform.name.Replace("(Clone)", "");
        aux.GetComponent<PianoUnitySimulator>().setPianoDriver(this);
        GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();

#endif
#if !UNITY_EDITOR
        GameObject aux = Instantiate(HololensConnection, transform);
        aux.name = aux.transform.name.Replace("(Clone)", "");
        aux.GetComponent<TCPCommunication>().setPianoDriver(this);
#endif
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
