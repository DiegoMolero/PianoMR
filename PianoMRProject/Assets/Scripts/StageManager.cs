﻿using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Vuforia;

public class StageManager : Singleton<StageManager>
{
    public enum State
    {
        None=0,//Null State
        AppInitialized=1,//App Started
        SplashesView=2, //Show the splashes
        VuforiaPiano = 3,//Detecting piano board with vuforia
        QRScan =4, //Scanning QR Ip 
        PianoConnection=5,//Initializing TCP connection
        GameInitialization=6//Starting the game
    };
    public State AppState= State.AppInitialized;
    private State PreviousState;

    public TextMesh InfoText = null;

    public GameObject PianoDriver;
    public GameObject QRScanner;
    public GameObject MainCamera;

    public string IpAdrress = null;
    // Use this for initialization
    protected override void Awake()
    { 
        PreviousState = State.None;
#if !UNITY_EDITOR
#endif
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Change of state 
        if (AppState != PreviousState)
        {
            PreviousState = AppState;
            switch (AppState)
            {
                case State.AppInitialized:
                    printMsg("App Initialized");
                    NextState();
                    break;
                #region SPLASHES STATE
                case State.SplashesView:
                    printMsg("Splashes View");
                    NextState();
                    break;
                #endregion
                #region VUFORIA PIANO STATE
                case State.VuforiaPiano:
                    printMsg("Vuforia Piano State Initializaed");
                    //MainCamera.GetComponent<VuforiaBehaviour>().enabled = true;
                    //MainCamera.GetComponent<DefaultInitializationErrorHandler>().enabled = true;
                    NextState();
                    break;
                #endregion
                #region QR SCAN STATE
                case State.QRScan:
                    //MainCamera.GetComponent<VuforiaBehaviour>().enabled = false;
                    //MainCamera.GetComponent<DefaultInitializationErrorHandler>().enabled = false;
                    printMsg("QR State Initialized");
#if !UNITY_EDITOR
                    GameObject scanner = Instantiate(QRScanner);
#endif
#if UNITY_EDITOR
                    NextState();
                    IpAdrress = "simulator";
#endif
                    break;
                #endregion
                #region PIANO CONNECTION STATE
                case State.PianoConnection:
#if !UNITY_EDITOR
                    Destroy(QRScanner);
#endif
                    printMsg("PianoConnection State Initialized");
                        GameObject driver = Instantiate(PianoDriver);
                        driver.name = driver.transform.name.Replace("(Clone)", "");
                    break;
                #endregion
                #region GAME STARTS STATE
                case State.GameInitialization:
                    printMsg("Game Initializaed");
                    break;
                #endregion
            }
        }
	}

    private void printMsg(string msg)
    {
        Debug.Log(msg);
        this.InfoText.text = msg;
    }

    public void ChangeState(State aux)
    {
        PreviousState = AppState;
        AppState = aux;
    }
    public void NextState()
    {
        PreviousState = AppState;
        AppState = AppState + 1;
    }
}
