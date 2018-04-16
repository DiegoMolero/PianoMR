using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class StageManager : Singleton<StageManager>
{
    public enum State
    {
        None= 0,//Null State
        AppInitialized= 1,//App Started
        SplashesView= 2, //Show the splashes
        QRScan = 3, //Scanning QR Ip 
        PianoConnection= 4,//Initializing TCP connection
        VuforiaPiano = 5,//Detecting piano board with vuforia
        GameInitialization =6//Starting the game
    };
    public State AppState= State.AppInitialized;
    private State PreviousState;

    public TextMesh InfoText = null;

    public GameObject Splashes;
    public GameObject QRScanner;
    public GameObject PianoDriver;
    public GameObject ImagePianoTarget;
    public GameObject Piano;

    private GameObject splahes_aux;
    private GameObject driver_aux;
    private GameObject qrscanner_aux;
    private GameObject pianotarget_aux;
    private GameObject piano_aux;

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
                #region QR SCAN STATE
                case State.QRScan:
                    Destroy(splahes_aux);
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
                #region VUFORIA PIANO STATE
                case State.VuforiaPiano:
                    printMsg("Vuforia Piano State Initializaed");
                    pianotarget_aux = Instantiate(ImagePianoTarget);
                    pianotarget_aux.name = pianotarget_aux.transform.name.Replace("(Clone)", "");
                    EnableVuforia();
                    /*
                    piano_aux = Instantiate(Piano);
                    piano_aux.name = piano_aux.transform.name.Replace("(Clone)", "");*/
                    //NextState();
                    break;
                #endregion
                #region GAME STARTS STATE
                case State.GameInitialization:
                    DisableVuforia();
                    Destroy(pianotarget_aux);
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
