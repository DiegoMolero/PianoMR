using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {
    public GameObject Lvl;
    private GameObject lvl_aux;
    public int Level_Number;
    public TextMesh Level_Info;
    public TextMesh Score_Info;
    public TextMesh NextStar_Info;
    public GameObject ModelStar;
    public Transform positionModelStart;
    // Use this for initialization
    void Start () {
        LvlJson lvlJson = null;
        if (Level_Number != 0) lvlJson = JsonManagerScore.ReadLvlJSON(Level_Number);
        Level_Info.text = "LEVEL " + Level_Number;

        if (lvlJson != null)
        {
            this.GetComponent<StarDisplay>().DisplayStars(lvlJson.Stars);
            Score_Info.text = "Score: "+lvlJson.Score;
            //Calculate next star
            if (lvlJson.Stars != 0)
            {
                if(lvlJson.Stars == 5) NextStar_Info.text = "Level completed!";
                else
                {
                    NextStar_Info.text = "\nNext         in: " +lvlJson.LimitStarts[lvlJson.Stars];
                    Instantiate(ModelStar,positionModelStart);
                }
            }

        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadLvl()
    {
        if(Level_Number != 0)
        {
            lvl_aux = Instantiate(Lvl);
            lvl_aux.name = lvl_aux.transform.name.Replace("(Clone)", "");
            lvl_aux.transform.position = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().position;
            lvl_aux.transform.rotation = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().rotation;
        }

    }
}
