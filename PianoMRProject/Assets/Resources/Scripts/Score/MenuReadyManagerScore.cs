using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuReadyManagerScore : MonoBehaviour
{
    public TextMesh ScoreTable;

    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
        List<LvlJson> list = JsonManagerScore.ReadLvlJSON();
        if (list != null)
        {
            foreach (LvlJson lvl in list)
            {
                ScoreTable.text = ScoreTable.text +
                    "Level: " + lvl.Lvl + "  " +
                    "Score: " + lvl.Score + "\n";
            }
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
                case PianoDriver.KeyNote.DO2:
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
                    Destroy(this.gameObject);
                    break;
                case PianoDriver.KeyNote.DO:
                    JsonManagerScore.InitLvlJSON();
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
