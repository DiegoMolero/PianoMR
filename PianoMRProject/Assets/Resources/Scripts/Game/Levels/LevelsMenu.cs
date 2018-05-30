using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMenu : MonoBehaviour {

    public Transform Arrows;
    public List<GameObject> LevelsSphere;

    private GameObject lvlChosen;
    private int index;
	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
        index = 0;
        lvlChosen = null;
        invokeSphereLevel();
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
                    break;
                //PLAY
                case PianoDriver.KeyNote.MI:
                    this.playLevel();
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
        lvlChosen.GetComponent<LevelSelector>().LoadLvl();
        Destroy(this.gameObject);
    }

    private void right()
    {
        int limit = LevelsSphere.Capacity - 1;
        index++;
        if (index > limit)
        {
            index = 0;
        }
        //Debug.Log(index + "  " + limit);
        invokeSphereLevel();
    }

    private void left()
    {
        int limit = LevelsSphere.Capacity - 1;
        index--;
        if (index < 0)
        {
            index = limit;
        }
        //Debug.Log(index + "  " + limit);
        invokeSphereLevel();
    }

    private void invokeSphereLevel()
    {
        if(lvlChosen != null)
        {
            Destroy(lvlChosen);
        }
        lvlChosen = Instantiate(LevelsSphere[index], this.transform);
        lvlChosen.name = lvlChosen.transform.name.Replace("(Clone)", "");
        lvlChosen.transform.position = Arrows.position;
        //lvlChosen.transform.rotation = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().rotation;
    }
}
