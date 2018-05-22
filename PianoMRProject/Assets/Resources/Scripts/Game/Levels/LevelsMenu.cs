using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMenu : MonoBehaviour {

    public List<GameObject> LevelsSphere;
    private GameObject lvlChosen;
	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PianoActionRecieved(PianoDriver.KeyNote key, bool action)
    {
        if (action == true) //If the key is being pressed
        {
            switch (key)
            {
                //LEFT
                case PianoDriver.KeyNote.DO:
                    this.left();
                    Destroy(this.gameObject);
                    break;
                //PLAY
                case PianoDriver.KeyNote.MI:
                    this.playLevel();
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
                    Destroy(this.gameObject);
                    break;
                //RIGHT
                case PianoDriver.KeyNote.SOL:
                    this.right();
                    break;
                //EXIT
                case PianoDriver.KeyNote.DO2:
                    GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.MenuGame);
                    Destroy(this.gameObject);
                    break;
            }
        }
    }

    private void playLevel()
    {
        throw new NotImplementedException();
    }

    private void right()
    {
        throw new NotImplementedException();
    }

    private void left()
    {
        throw new NotImplementedException();
    }
}
