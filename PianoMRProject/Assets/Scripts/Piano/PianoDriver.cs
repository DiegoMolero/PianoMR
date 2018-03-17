using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoDriver : MonoBehaviour {


    public TextMesh tm = null;

    public void RecievePianoData(string data)
    {

        if (tm != null)  tm.text = data;
        Debug.Log("Event recieving: "+data);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
