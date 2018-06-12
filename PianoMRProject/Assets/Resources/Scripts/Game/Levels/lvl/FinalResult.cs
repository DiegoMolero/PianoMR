using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResult : MonoBehaviour {
    public TextMesh text;
    public int scoreObtained;
    public int timeShowResult;
    public bool import;
    public bool New_record;
	// Use this for initialization
	void Start () {
        if (New_record == true) //IF IS A NEW RECORD
        {
            text.text = "NEW RECORD!\n";
        }
        text.text = text.text+"Your score is\n" + scoreObtained;
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
    }

    public void PianoActionRecieved(PianoDriver.KeyNote key, bool action)
    {
        if (action == true) Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (!import) //IF IS A NORMAL LEVEL
        {
            GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
        }
        else //IF IS A IMPORTED LEVEL
        {
            GameObject.Find("LvlImport").GetComponent<StateImportManager>().NextState();
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
