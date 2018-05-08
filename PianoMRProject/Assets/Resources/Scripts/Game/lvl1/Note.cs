using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
    private Rigidbody rb;

    public PianoDriver.KeyNote Target;
    public float Timer = 0.0f;
    private bool initialized = false;
    public TextMesh infoFinguer1;
    public TextMesh infoFinguer2;
    private int figuer;

    public void Initialize(PianoDriver.KeyNote target, float initPosition, float speed,int finger)
    {
        this.transform.position += new Vector3(0,initPosition,0);//* this.GetComponent<Transform>().up.normalized;
        this.transform.position += positionNote(target);
        this.transform.rotation = Quaternion.LookRotation(this.transform.forward.normalized);
        rb.velocity = new Vector3(0, speed, 0);
        Target = target;
        this.speed = speed;
        this.figuer = finger;
        if (finger > 0 && finger < 6) {
            infoFinguer1.text = finger.ToString();
            infoFinguer2.text = finger.ToString();
        }
        initialized = true;
    }

    public float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(initialized){
            Timer += Time.deltaTime;
        }
      
    }

    private void OnDestroy()
    {
        //Debug.Log(Timer.ToString());
    }


    private Vector3 positionNote(PianoDriver.KeyNote target)
    {
        Vector3 target_position= new Vector3(0,0,0);
        Transform aux = GameObject.FindGameObjectWithTag("Triggers").transform.FindChild(target.ToString());
        Debug.Log(target.ToString() + " "+aux.position.x);
        target_position = aux.position.x * this.GetComponent<Transform>().right.normalized;
        /*
        switch (target)
        {
            case PianoDriver.KeyNote.DO:
                target_position = -0.0781f * this.GetComponent<Transform>().right.normalized;
                break;
            case PianoDriver.KeyNote.RE:
                target_position = -0.0555f * this.GetComponent<Transform>().right.normalized;
                break;
            case PianoDriver.KeyNote.MI:
                target_position = -0.0329f * this.GetComponent<Transform>().right.normalized;
                break;
            case PianoDriver.KeyNote.FA:
                target_position = -0.0103f * this.GetComponent<Transform>().right.normalized;
                break;
            case PianoDriver.KeyNote.SOL:
                target_position = 0.0123f * this.GetComponent<Transform>().right.normalized;
                break;
            case PianoDriver.KeyNote.LA:
                target_position = 0.0225f * this.GetComponent<Transform>().right.normalized;
                break;
        }
*/
        return target_position;
    }
}
