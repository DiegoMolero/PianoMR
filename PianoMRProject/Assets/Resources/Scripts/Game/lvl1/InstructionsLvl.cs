using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsLvl : MonoBehaviour {
    public GameObject game;
    public GameObject InstructionFingers;
    public GameObject InstructionKeys;
    public float Timer = 6.0f;
    public TextMesh txt;
    private int InstructionState;

    private GameObject aux;
    private float limit_timer;

    private int aux_timer;
    // Use this for initialization
    void Start () {
        limit_timer = Timer;
        aux_timer = (int)Timer;
        InstructionState = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Timer -= Time.deltaTime;
        if((int)Timer < aux_timer)
        {
            aux_timer = (int)Timer;
            txt.text = "Starts in \n" + aux_timer;
            if (aux_timer > limit_timer* 2/3)
            {
                if( InstructionState == 0)
                {
                    InstructionState++;
                    aux = Instantiate(InstructionKeys, this.transform);
                    aux.name = aux.transform.name.Replace("(Clone)", "");

                }

            }
            else if(aux_timer < limit_timer * 2/3)
            {
                if (InstructionState == 1)
                {
                    InstructionState++;
                    Destroy(aux);
                    aux = Instantiate(InstructionFingers, this.transform);
                    aux.name = aux.transform.name.Replace("(Clone)", "");
                }

            }

            if (aux_timer == 0)
            {
                GameObject lvl_aux = Instantiate(game);
                lvl_aux.name = lvl_aux.transform.name.Replace("(Clone)", "");
                lvl_aux.transform.position = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().position;
                lvl_aux.transform.rotation = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().rotation;
                Destroy(this.gameObject);
            }
        }
        
    }
}
