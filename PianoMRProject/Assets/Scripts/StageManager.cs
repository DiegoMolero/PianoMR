using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public enum State
    {
        None,//Null State
        AppInitialized,//App Started
        SplashesView, //Show the splashes
        VuforiaPiano,//Detecting piano board with vuforia
        QRScan, //Scanning QR Ip 
        PianoConnection,//Initializing TCP connection
        GameInitialization//Starting the game
    };
    public State AppState= State.AppInitialized;
    public TextMesh InfoText = null;
    private State PreviousState;

    public GameObject PianoDriver;
    // Use this for initialization
    protected override void Awake()
    { 
        PreviousState = State.None;
#if !UNITY_EDITOR
        AppState = State.PianoConnection;
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
                    Debug.Log("App Initialized");
                    break;
                case State.SplashesView:
                    
                    break;
                case State.VuforiaPiano:
                    
                    break;
                case State.QRScan:
                    printMsg("QR State Initialized");
                    GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<Placeholder>().OnScan();
                    break;
                case State.PianoConnection:
                    Debug.Log("PianoConnection State Initialized");
                    GameObject aux = Instantiate(PianoDriver);
                    aux.name = aux.transform.name.Replace("(Clone)", "");
                    break;
                case State.GameInitialization:
                    
                    break;
            }
        }
	}

    private void printMsg(string msg)
    {
        Debug.Log(msg);
        this.InfoText.text = msg;
    }
}
