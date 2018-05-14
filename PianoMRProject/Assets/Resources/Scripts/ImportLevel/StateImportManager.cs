using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateImportManager : MonoBehaviour {
    public GameObject ScanQRLvl;
    public GameObject LvlImported;
    public GameObject MenuImportLvl;
    public string FileName;


    public XMLReaderMusic xmlReader;
    private GameObject aux;

    public State LvlState;
    private State PreviousState;

    public enum State
    {
        None = 0, //None
        ScanQR = 1,//Scan QR
        LoadXML = 2,//Load XML file
        Playing = 3, //Playing the lvl
        Menu = 4, //Choose option
        Exit = 5  //Exit to the main menu
    }
    // Use this for initialization
    void Start()
    {
        xmlReader = this.GetComponent<XMLReaderMusic>();
#if !UNITY_EDITOR
        LvlState = State.ScanQR;
#endif
#if UNITY_EDITOR
        LvlState = State.LoadXML;
#endif
    }
    // Update is called once per frame
    void Update () {
        if (LvlState != PreviousState)
        {
            PreviousState = LvlState;
            switch (LvlState)
            {
                #region NONE STATE
                case State.ScanQR:
#if !UNITY_EDITOR
                    aux = Instantiate(ScanQRLvl,this.transform);
#endif
                    break;
                #endregion
                #region LOADXML STATE
                case State.LoadXML:
#if !UNITY_EDITOR
                    Destroy(aux);
#endif

#if UNITY_EDITOR
                    xmlReader.ReadLocalFile(FileName);
                    NextState();
#endif
                    break;
                #endregion
                #region PLAYING STATE
                case State.Playing:
#if UNITY_EDITOR
                    Instantiate(LvlImported,this.transform);
                    GameObject.FindGameObjectWithTag("SheetManager").GetComponent<MusicSheetManager>().musicSheet = xmlReader.musicSheet;
#endif
                    break;
                #endregion
                #region MENU STATE
                case State.Menu:
                    Instantiate(MenuImportLvl, this.transform);
                    break;
                #endregion
                #region EXIT
                case State.Exit:
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
                    Destroy(this.gameObject);
                    break;
                    #endregion
            }
        }
    }


    #region STATE MANAGERS FUNCTIONS
    public void ChangeState(State aux)
    {
        PreviousState = LvlState;
        LvlState = aux;
    }
    public void NextState()
    {
        PreviousState = LvlState;
        LvlState = LvlState + 1;
    }

    #endregion
}
