using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerOptions : MonoBehaviour
{
    public TextMesh Audio_info;

    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
        if (GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes)
        {
            Audio_info.text = "M\nU\nT\nE\n\nN\nO\nT\nE\nS";
        }
        else
        {
            Audio_info.text = "\nU\nN\nM\nU\nT\nE\n\nN\nO\nT\nE\nS";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PianoActionRecieved(PianoDriver.KeyNote key, bool action)
    {
        if (action == true) //If the key is being pressed
        {
            switch (key)
            {
                case PianoDriver.KeyNote.DO:
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
                    Destroy(this.gameObject);
                    break;
                case PianoDriver.KeyNote.SOL:
                    if (GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes)
                    {
                        GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes = false;
                        Audio_info.text = "\nU\nN\nM\nU\nT\nE\n\nN\nO\nT\nE\nS";
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().AudioNotes = true;
                        Audio_info.text = "M\nU\nT\nE\n\nN\nO\nT\nE\nS";
                    }
                    break;
                case PianoDriver.KeyNote.DO2:
                    JsonManagerScore.InitLvlJSON();
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
