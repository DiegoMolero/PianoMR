﻿using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

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
    public GameObject Splashes;


    private GameObject splahes_aux;
    private GameObject driver_aux;
    private GameObject qrscanner_aux;

    public string IpAdrress = null;
    // Use this for initialization
    protected override void Awake()
    { 
        PreviousState = State.None;
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
                #region APP INITIALATION STATE
                case State.AppInitialized:
                    printMsg("App Initialized");
                    NextState();
                    break;
                #endregion
                #region SPLASHES STATE
                case State.SplashesView:
                    printMsg("Splashes View");
                    splahes_aux = Instantiate(Splashes);
                    splahes_aux.name = splahes_aux.transform.name.Replace("(Clone)", "");
                    //NextState();
                    break;
                #endregion
                #region VUFORIA PIANO STATE
                case State.VuforiaPiano:
                    Destroy(splahes_aux);
                    printMsg("Vuforia Piano State Initializaed");
                    EnableVuforia();
                    //NextState();
                    break;
                #endregion
                #region QR SCAN STATE
                case State.QRScan:
                    DisableVuforia();
                    printMsg("QR State Initialized");
#if !UNITY_EDITOR
                    qrscanner_aux = Instantiate(QRScanner);
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
                    Destroy(qrscanner_aux);
#endif
                    printMsg("PianoConnection State Initialized");
                    driver_aux = Instantiate(PianoDriver);
                    driver_aux.name = driver_aux.transform.name.Replace("(Clone)", "");
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
    #region STATE MANAGERS FUNCTIONS
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

    #endregion
    #region VUFORIA MANAGERS FUNCTIONS
    public void DisableVuforia()
    {
        Camera.main.GetComponent<VuforiaBehaviour>().enabled = false;
        VuforiaRuntime.Instance.Deinit();
    }
    public void EnableVuforia()
    {
        VuforiaConfiguration.Instance.Vuforia.DelayedInitialization = false;
        Camera.main.GetComponent<VuforiaBehaviour>().enabled = true;
        VuforiaRuntime.Instance.InitVuforia();
    }
    #endregion

}
