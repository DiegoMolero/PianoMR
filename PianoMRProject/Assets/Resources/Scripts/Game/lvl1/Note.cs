using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
    private Rigidbody rb;

    public PianoDriver.KeyNote Target;
    private bool initialized = false;
    public TextMesh infoFinguer1;
    public TextMesh infoFinguer2;
    private int figuer;

    public void Initialize(PianoDriver.KeyNote target, float initPosition, float speed,int finger)
    {
        this.transform.position += new Vector3(0,initPosition,0);
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
