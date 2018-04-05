using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoDriver : MonoBehaviour {


    public TextMesh tm = null;

    public GameObject HololensConnection;
    public GameObject UnityConnection;

    public void RecievePianoData(string data)
    {
        if (tm != null)  tm.text = data;
        Debug.Log("Event recieving: "+data);
    }

    public void Awake()
    {
#if UNITY_EDITOR
        GameObject aux = Instantiate(UnityConnection, transform);
        aux.name = aux.transform.name.Replace("(Clone)", "");
#endif
#if !UNITY_EDITOR
        GameObject aux = Instantiate(HololensConnection, transform);
        aux.name = aux.transform.name.Replace("(Clone)", "");
#endif
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ParsePianoData(string Data)
    {

    }
}
