using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsLvl : MonoBehaviour {
    public GameObject game;
    public TextMesh txt;
    public float Timer = 6.0f;
    private int aux_timer;
    // Use this for initialization
    void Start () {
        aux_timer = (int)Timer;
	}
	
	// Update is called once per frame
	void Update () {
        Timer -= Time.deltaTime;
        if((int)Timer < aux_timer)
        {
            aux_timer = (int)Timer;
            txt.text = "Starts in \n" + aux_timer;
            if (aux_timer == 0)
            {
                /*
                GameObject aux = Instantiate(game);
                aux.name = aux.transform.name.Replace("(Clone)", "");
                */
                game.SetActive(true);
                Destroy(this.gameObject);
            }
        }
        
    }
}
