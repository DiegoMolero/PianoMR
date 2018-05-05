using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColumn : MonoBehaviour {
    public GameObject column;
    private Quaternion _facing;
    private Vector3 directionVector;
    private GameObject aux;

    void Start()
    {
        aux = Instantiate(column, this.transform);

        _facing = aux.transform.rotation;
        aux.transform.rotation = Quaternion.LookRotation(aux.transform.right.normalized);
    }
    void Update()
    {
        /*
        var rotation = Quaternion.LookRotation(directionVector.normalized);
        rotation *= _facing;
        aux.transform.rotation = rotation;
        */
    }
}
