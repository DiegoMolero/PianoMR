using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
    private Rigidbody rb;

    public PianoDriver.KeyNote Target;
    private bool initialized = false;
    public TextMesh infoFinger1;
    public TextMesh infoFinger2;
    private int finger;
    private float speed;

    public void Initialize(PianoDriver.KeyNote target, float initPosition, float speed,int finger)
    {
        this.transform.position += new Vector3(0,initPosition,0);
        this.transform.position += positionNote(target);
        this.transform.rotation = Quaternion.LookRotation(this.transform.forward.normalized);
        rb.velocity = new Vector3(0, speed, 0);
        Target = target;
        this.speed = speed;
        this.finger = finger;
        if (finger > 0 && finger < 6) {
            infoFinger1.text = finger.ToString();
            infoFinger2.text = finger.ToString();
        }
        initialized = true;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

      
    }

    private void OnDestroy()
    {
    }


    private Vector3 positionNote(PianoDriver.KeyNote target)
    {
        Transform aux = GameObject.FindGameObjectWithTag("Triggers").transform.Find(target.ToString());
        Vector3 target_position = aux.localPosition.x * this.GetComponent<Transform>().right.normalized;
        return target_position;
    }
}
