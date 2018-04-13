using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoController : MonoBehaviour {
    public GameObject _DO,_DO_A,_RE,_RE_A,_MI,_FA,_FA_A,_SOL,_SOL_A,_LA,_LA_A,_SI,_DO2;
    public Hashtable keys = new Hashtable();

    private void Awake()
    {
        keys.Add(60, _DO);  //DO
        keys.Add(61, _DO_A);  //DO#
        keys.Add(62, _RE);  //RE
        keys.Add(63, _RE_A);  //RE#
        keys.Add(64, _MI);  //MI
        keys.Add(65, _FA);  //FA
        keys.Add(66, _FA_A);  //FA#
        keys.Add(67, _SOL);  //SOL
        keys.Add(68, _SOL_A);  //SOL#
        keys.Add(69, _LA);  //LA
        keys.Add(70, _LA_A);  //LA#
        keys.Add(71, _SI);  //SI
        keys.Add(72, _DO2);  //DO'
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PianoActionRecieved(int key,bool action)
    {
       Debug.Log(key+" "+action);
       GameObject aux= (GameObject)keys[key];
       aux.GetComponent<PianoKey>().ActionRecieved(action);
    }
}
