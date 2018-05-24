using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDisplay : MonoBehaviour {
    public GameObject StarShining;
    public GameObject StarNormal;

    public List<Transform> StarPosition;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayStars(int stars)
    {
        int nStar = stars;
        for (int index = 0;index < StarPosition.Capacity; index++)
        {
            GameObject aux;
            if (index < nStar) {
                aux = Instantiate(StarShining, StarPosition[index]);
            }
            else
            {
                aux = Instantiate(StarNormal, StarPosition[index]);
            }
        }
    }

}
