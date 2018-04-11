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
    private State PreviousState;

    public TextMesh InfoText = null;

    public GameObject PianoDriver;
    public GameObject QRScanner;

    public string IpAdrress = null;
    // Use this for initialization
    protected override void Awake()
    { 
        PreviousState = State.None;
#if !UNITY_EDITOR
        //AppState = State.PianoConnection;
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
#if !UNITY_EDITOR
                    GameObject scanner = Instantiate(QRScanner);
#endif
#if UNITY_EDITOR
                    ChangeState(State.PianoConnection);
                    IpAdrress = "simulator";
#endif
                    break;
                case State.PianoConnection:
#if !UNITY_EDITOR
                    Destroy(QRScanner);
#endif
                    printMsg("PianoConnection State Initialized");
                        GameObject driver = Instantiate(PianoDriver);
                        driver.name = driver.transform.name.Replace("(Clone)", "");
                    break;
                case State.GameInitialization:
                    printMsg("");
                    break;
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
}
