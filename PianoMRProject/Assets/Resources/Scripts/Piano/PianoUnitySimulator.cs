using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoUnitySimulator : Singleton<PianoUnitySimulator> {
    private PianoDriver _pianodriver;

    // Use this for initialization
    public void setPianoDriver(PianoDriver aux)
    {
        _pianodriver = aux;
    }
	
	// Update is called once per frame
	void Update () {
        if (_pianodriver == null) return;
        //Key DO
        if (Input.GetKeyDown(KeyCode.C)) _pianodriver.RecievePianoData("90 3C 7F");
        if (Input.GetKeyUp(KeyCode.C)) _pianodriver.RecievePianoData("90 3C 00");
        //Key DO#
        if (Input.GetKeyDown(KeyCode.F)) _pianodriver.RecievePianoData("90 3D 7F");
        if (Input.GetKeyUp(KeyCode.F)) _pianodriver.RecievePianoData("90 3D 00");
        //Key RE
        if (Input.GetKeyDown(KeyCode.V)) _pianodriver.RecievePianoData("90 3E 7F");
        if (Input.GetKeyUp(KeyCode.V)) _pianodriver.RecievePianoData("90 3E 00");
        //Key RE#
        if (Input.GetKeyDown(KeyCode.G)) _pianodriver.RecievePianoData("90 3F 7F");
        if (Input.GetKeyUp(KeyCode.G)) _pianodriver.RecievePianoData("90 3F 00");
        //Key MI
        if (Input.GetKeyDown(KeyCode.B)) _pianodriver.RecievePianoData("90 40 7F");
        if (Input.GetKeyUp(KeyCode.B)) _pianodriver.RecievePianoData("90 40 00");
        //Key FA
        if (Input.GetKeyDown(KeyCode.N)) _pianodriver.RecievePianoData("90 41 7F");
        if (Input.GetKeyUp(KeyCode.N)) _pianodriver.RecievePianoData("90 41 00");
        //Key FA#
        if (Input.GetKeyDown(KeyCode.J)) _pianodriver.RecievePianoData("90 42 7F");
        if (Input.GetKeyUp(KeyCode.J)) _pianodriver.RecievePianoData("90 42 00");
        //Key SOL
        if (Input.GetKeyDown(KeyCode.M)) _pianodriver.RecievePianoData("90 43 7F");
        if (Input.GetKeyUp(KeyCode.M)) _pianodriver.RecievePianoData("90 43 00");
        //Key SOL#
        if (Input.GetKeyDown(KeyCode.K)) _pianodriver.RecievePianoData("90 44 7F");
        if (Input.GetKeyUp(KeyCode.K)) _pianodriver.RecievePianoData("90 44 00");
        //Key LA
        if (Input.GetKeyDown(KeyCode.Comma)) _pianodriver.RecievePianoData("90 45 7F");
        if (Input.GetKeyUp(KeyCode.Comma)) _pianodriver.RecievePianoData("90 45 00");
        //Key LA#
        if (Input.GetKeyDown(KeyCode.L)) _pianodriver.RecievePianoData("90 46 7F");
        if (Input.GetKeyUp(KeyCode.L)) _pianodriver.RecievePianoData("90 46 00");
        //Key SI
        if (Input.GetKeyDown(KeyCode.Period)) _pianodriver.RecievePianoData("90 47 7F");
        if (Input.GetKeyUp(KeyCode.Period)) _pianodriver.RecievePianoData("90 47 00");
        //Key DO'
        if (Input.GetKeyDown(KeyCode.Minus)) _pianodriver.RecievePianoData("90 48 7F");
        if (Input.GetKeyUp(KeyCode.Minus)) _pianodriver.RecievePianoData("90 48 00");
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
