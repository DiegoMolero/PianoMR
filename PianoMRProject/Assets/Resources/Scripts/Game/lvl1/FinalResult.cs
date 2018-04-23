using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResult : MonoBehaviour {
    public TextMesh text;
    public int scoreObtained;
    public int scoreNeeded;
    public int timeShowResult;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, timeShowResult);
        if (scoreObtained >= scoreNeeded)
        {
            text.text = "Congrats! :D\nPass to the next level";
        }
        else
        {
            text.text = "Try again!\n You need " + scoreNeeded + " for complete the level";

        }
    }

    private void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
