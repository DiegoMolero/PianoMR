using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
    private Rigidbody rb;

    public PianoDriver.KeyNote Target;
    public float Timer = 0.0f;

    public void Initialize(PianoDriver.KeyNote target, Vector3 initPosition, float speed)
    {
        this.transform.position = initPosition + positionNote(target);
        Target = target;
        this.speed = speed;
    }

    public float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Start () {
        rb.velocity = new Vector3(0,speed,0);
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
    }

    private void OnDestroy()
    {
        //Debug.Log(Timer.ToString());
    }


    private Vector3 positionNote(PianoDriver.KeyNote target)
    {
        Vector3 target_position= new Vector3(0,0,0);
        switch (target)
        {
            case PianoDriver.KeyNote.DO:
                target_position = new Vector3(-0.0781f, 0f, 0f);
                break;
            case PianoDriver.KeyNote.RE:
                target_position = new Vector3(-0.0555f, 0f, 0f);
                break;
            case PianoDriver.KeyNote.MI:
                target_position = new Vector3(-0.0329f, 0f, 0f);
                break;
            case PianoDriver.KeyNote.FA:
                target_position = new Vector3(-0.0103f, 0f, 0f);
                break;
            case PianoDriver.KeyNote.SOL:
                target_position = new Vector3(0.0123f, 0f, 0f);
                break;
            case PianoDriver.KeyNote.LA:
                target_position = new Vector3(0.0225f, 0f, 0f);
                break;
        }
        return target_position;
    }
}
