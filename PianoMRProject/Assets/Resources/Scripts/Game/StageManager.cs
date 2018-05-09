using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class StageManager : Singleton<StageManager>
{
    public enum State
    {
        None = 0,//Null State
        AppInitialized = 1,//App Started
        SplashesView = 2, //Show the splashes
        QRScan = 3, //Scanning QR Ip 
        PianoConnection = 4,//Initializing TCP connection
        VuforiaPiano = 5,//Detecting piano board with vuforia
        GameInitialization = 6,//Starting the game
        MenuGame = 7, //The piano displays a menu for choosing the level
        Playing = 8 //When a level is load and the user is playing it
    };
    [Header("App Info")]
    public State AppState;
    private State PreviousState;
    public TextMesh InfoText = null;
    public bool AudioNotes = true;

    [Header("Splashes State")]
    public GameObject Splashes;
    [Header("QR Scanner State")]
    public GameObject QRScanner;
    [Header("Piano Connection State")]
    public GameObject PianoDriver;
    public string IpAdrress = null;
    [Header("Vuforia Scanner State")]
    public GameObject ImagePianoTarget;
    public GameObject VuforiaIntructions;
    [Header("Game Initialization State")]
    public GameObject Piano;
    [Header("Menu Game State")]
    public GameObject Menu;

    private GameObject splahes_aux;
    private GameObject driver_aux;
    private GameObject qrscanner_aux;
    private GameObject vuforiainstructions_aux;
    private GameObject pianotarget_aux;
    private GameObject piano_aux;
    private GameObject menu_aux;


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
                #region NONE STATE
                case State.None:
                    printMsg("NONE");
                    break;
                #endregion
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
#if !UNITY_EDITOR
                    vuforiainstructions_aux = Instantiate(VuforiaIntructions);
                    vuforiainstructions_aux.name = vuforiainstructions_aux.transform.name.Replace("(Clone)", "");
                    pianotarget_aux = Instantiate(ImagePianoTarget);
                    pianotarget_aux.name = pianotarget_aux.transform.name.Replace("(Clone)", "");
                    EnableVuforia();
#endif
#if UNITY_EDITOR
                    NextState();
#endif
                    break;
#endregion
                #region GAME STARTS STATE
                case State.GameInitialization:
                    printMsg("Game Initializaed");
#if !UNITY_EDITOR
                    DisableVuforia();
                    Destroy(pianotarget_aux);
                    Destroy(vuforiainstructions_aux);
#endif
#if UNITY_EDITOR
                    piano_aux = Instantiate(Piano);
                    piano_aux.name = piano_aux.transform.name.Replace("(Clone)", "");
                    piano_aux.transform.position = new Vector3(0, -0.14f, 0.52f);
#endif
                    NextState();
                    break;
#endregion
                #region MENU STATE
                case State.MenuGame:
                    printMsg("Menu State");
                    menu_aux = Instantiate(Menu);
                    menu_aux.name = menu_aux.transform.name.Replace("(Clone)", "");
                    menu_aux.transform.position = GameObject.FindGameObjectWithTag("Piano").transform.position;
                    menu_aux.transform.rotation = GameObject.FindGameObjectWithTag("Piano").transform.rotation;
                    break;
                #endregion
                #region PLAYING STATE
                case State.Playing:
                    printMsg("Playing State");
                    Destroy(menu_aux);
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
    public void SetPiano(GameObject piano)
    {
        piano_aux = piano;
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
