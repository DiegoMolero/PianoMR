using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDisplayEffect : MonoBehaviour {
    public GameObject StarShining;
    public GameObject StarNormal;
    public GameObject ParticleStars;

    public GameObject[] ListStars;
    public float Timer;
    public float timeDisplayStars;
    private float auxTimer;
    private int index;
    private bool startDisplay = false;
    private int startsToShow = 0;
    public List<Transform> StarPosition;
	// Use this for initialization
	void Start () {
        Timer = 0;
        index = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (startDisplay)
        {
            Timer += Time.deltaTime;
            if(startsToShow != 0)
            {
                if(Timer > auxTimer)
                {
                    auxTimer += timeDisplayStars;
                    startsToShow--;
                    createStarShining();
                    index++;
                }
            }
        }

        
    }

    public void DisplayStars(int stars)
    {
        startsToShow = stars;
        ListStars = new GameObject[StarPosition.Capacity];
        for (int i = 0;i < StarPosition.Capacity; i++)
        {
            GameObject aux = Instantiate(StarNormal, StarPosition[i]);
            ListStars[i] = aux;
        }
        auxTimer = timeDisplayStars;
        startDisplay = true;
    }

    private void createStarShining()
    {
        Destroy(ListStars[index].gameObject);
        ListStars[index] = Instantiate(StarShining, StarPosition[index]);
        GameObject aux = Instantiate(ParticleStars, ListStars[index].transform);
        Destroy(aux,1.5f);
    }

}
