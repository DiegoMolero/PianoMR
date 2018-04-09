using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour {
    [Tooltip("indicates if the key is being pressed or not")]
    public bool activate = false;
    [Tooltip("Is the material of the key by defult, when the key is not pressed")]
    public Material  inactivate_material;
    [Tooltip("Changes to this material when the key is pressed")]
    public Material activate_material;
    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().material = inactivate_material;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ActionRecieved(bool action)
    {
        if (action) Press();
        else Release();
    }


    public void Press()
    {
        GetComponent<Renderer>().material = activate_material;
        activate = true;
    }

    public void Release()
    {
        activate = false;
        GetComponent<Renderer>().material = inactivate_material;
    }
}
