using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResult : MonoBehaviour {
    public TextMesh text;
    public int scoreObtained;
    public int scoreNeeded;
    public int timeShowResult;
    public bool import;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, timeShowResult);
        text.text = "Congrats! \nYour score is\n"+scoreObtained;
    }

    private void OnDestroy()
    {
        if (import ==false)
        {
            GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
        }
        else
        {
            GameObject.Find("LvlImport").GetComponent<StateImportManager>().NextState();
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
