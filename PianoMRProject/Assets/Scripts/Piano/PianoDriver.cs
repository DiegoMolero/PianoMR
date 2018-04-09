using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoDriver : MonoBehaviour {


    public TextMesh tm = null;

    public GameObject HololensConnection;
    public GameObject UnityConnection;

    public void RecievePianoData(string data)
    {
        string[] data_fragmented;
        bool aux_activate;
        int key_value;
        if (tm != null) tm.text = data;
        Debug.Log("Event recieving: " + data);
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
            GameObject.FindGameObjectWithTag("Piano").GetComponent<PianoController>().PianoActionRecieved(key_value, aux_activate);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void Awake()
    {
#if UNITY_EDITOR
        GameObject aux = Instantiate(UnityConnection, transform);
        aux.name = aux.transform.name.Replace("(Clone)", "");
        aux.GetComponent<PianoUnitySimulator>().setPianoDriver(this);
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
