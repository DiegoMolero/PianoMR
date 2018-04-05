using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoUnityController : Singleton<PianoUnityController> {
    [Tooltip("Function to invoke at incoming packet")]
    public PianoMessageEvent pianoEvent = null;

    // Use this for initialization
    void Start () {
        
        if (pianoEvent == null)
        {
            Debug.Log("Piano event needs a function to invoke for sending incoming data");
        }
        pianoEvent.AddListener(GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().RecievePianoData);
    }
	
	// Update is called once per frame
	void Update () {
        //Key DO
        if (Input.GetKeyDown(KeyCode.C)) pianoEvent.Invoke("90 3C 7F");
        if (Input.GetKeyUp(KeyCode.C)) pianoEvent.Invoke("90 3C 00");
        //Key DO#
        if (Input.GetKeyDown(KeyCode.F)) pianoEvent.Invoke("90 3D 7F");
        if (Input.GetKeyUp(KeyCode.F)) pianoEvent.Invoke("90 3D 00");
        //Key RE
        if (Input.GetKeyDown(KeyCode.V)) pianoEvent.Invoke("90 3E 7F");
        if (Input.GetKeyUp(KeyCode.V)) pianoEvent.Invoke("90 3E 00");
        //Key RE#
        if (Input.GetKeyDown(KeyCode.G)) pianoEvent.Invoke("90 3F 7F");
        if (Input.GetKeyUp(KeyCode.G)) pianoEvent.Invoke("90 3F 00");
        //Key MI
        if (Input.GetKeyDown(KeyCode.B)) pianoEvent.Invoke("90 40 7F");
        if (Input.GetKeyUp(KeyCode.B)) pianoEvent.Invoke("90 40 00");
        //Key FA
        if (Input.GetKeyDown(KeyCode.N)) pianoEvent.Invoke("90 41 7F");
        if (Input.GetKeyUp(KeyCode.N)) pianoEvent.Invoke("90 41 00");
        //Key FA#
        if (Input.GetKeyDown(KeyCode.J)) pianoEvent.Invoke("90 42 7F");
        if (Input.GetKeyUp(KeyCode.J)) pianoEvent.Invoke("90 42 00");
        //Key SOL
        if (Input.GetKeyDown(KeyCode.M)) pianoEvent.Invoke("90 43 7F");
        if (Input.GetKeyUp(KeyCode.M)) pianoEvent.Invoke("90 43 00");
        //Key SOL#
        if (Input.GetKeyDown(KeyCode.K)) pianoEvent.Invoke("90 44 7F");
        if (Input.GetKeyUp(KeyCode.K)) pianoEvent.Invoke("90 44 00");
        //Key LA
        if (Input.GetKeyDown(KeyCode.Comma)) pianoEvent.Invoke("90 45 7F");
        if (Input.GetKeyUp(KeyCode.Comma)) pianoEvent.Invoke("90 45 00");
        //Key LA#
        if (Input.GetKeyDown(KeyCode.L)) pianoEvent.Invoke("90 46 7F");
        if (Input.GetKeyUp(KeyCode.L)) pianoEvent.Invoke("90 46 00");
        //Key SI
        if (Input.GetKeyDown(KeyCode.Period)) pianoEvent.Invoke("90 47 7F");
        if (Input.GetKeyUp(KeyCode.Period)) pianoEvent.Invoke("90 47 00");
        //Key DO'
        if (Input.GetKeyDown(KeyCode.Minus)) pianoEvent.Invoke("90 48 7F");
        if (Input.GetKeyUp(KeyCode.Minus)) pianoEvent.Invoke("90 48 00");
    }
    /// <summary>
    /// It prints in the console the key pressed in order to known how to detect an specific key
    /// </summary>
    private void DiscoverKeyCode()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key)) Debug.Log(key);
        }
    }
}
