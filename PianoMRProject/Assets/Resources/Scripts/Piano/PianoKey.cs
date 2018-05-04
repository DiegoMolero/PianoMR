using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour {
    [Tooltip("indicates if the key is being pressed or not")]
    public bool Activate = false;
    [Tooltip("Is the material of the key by defult, when the key is not pressed")]
    public Material  inactivate_material;
    [Tooltip("Changes to this material when the key is pressed")]
    public Material Hit_material;
    public Material Miss_material;
    public Material Press_material;
    private AudioSource audio;
    private bool audio_notes;
    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().material = inactivate_material;
        audio = GetComponent<AudioSource>();
        audio_notes = GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes;
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
        if(GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes) audio.Play();
        if (GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AppState != StageManager.State.Playing) GetComponent<Renderer>().material = Press_material;
        Activate = true;
    }

    public void Release()
    {
        Activate = false;
        GetComponent<Renderer>().material = inactivate_material;
    }
    public void PressHit()
    {
        GetComponent<Renderer>().material = Hit_material;
    }
    public void PressMiss()
    {
        GetComponent<Renderer>().material = Miss_material;
    }
}
