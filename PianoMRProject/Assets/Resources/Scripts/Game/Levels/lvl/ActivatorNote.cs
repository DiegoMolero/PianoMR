﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorNote : MonoBehaviour {
    private bool isPressed;
    private bool active=false;
    private GameObject note;
    public PianoDriver.KeyNote KeyNote;
    [Tooltip("Is the material of the key by defult, when the key is not pressed")]
    public Material inactivate_material;
    [Tooltip("Changes to this material when the key is pressed")]
    public Material activate_material;

    private Renderer render;
    
    private void Awake()
    {
        render = GetComponent<Renderer>();
    }
    // Use this for initialization
    void Start () {
        render.material = inactivate_material;
        GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().pianoEvent.AddListener(PianoActionRecieved);
    }

    public void PianoActionRecieved(PianoDriver.KeyNote key, bool action)
    {
        if (key == KeyNote) ActionRecieved(action);

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        active = true;   
        if(col.gameObject.tag == "Note")
        {
            note = col.gameObject;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        active = false;
    }

    public void ActionRecieved(bool action)
    {
        if (action) Press();
        else Release();
    }


    public void Press()
    {
        render.material = activate_material;
        if (active==true) //If a note has enter into the trigger and the key pressed
        {
            Destroy(note);
            GameObject.FindGameObjectWithTag("SheetManager").GetComponent<MusicSheetManager>().IncreaseScore();
            GameObject.FindGameObjectWithTag(KeyNote.ToString()).GetComponent<PianoKey>().PressHit();
            active = false;
            createEffect();

        }
        else
        {
            GameObject.FindGameObjectWithTag("SheetManager").GetComponent<MusicSheetManager>().DecreaseScore();
            GameObject.FindGameObjectWithTag(KeyNote.ToString()).GetComponent<PianoKey>().PressMiss();
        }
    }

    public void Release()
    {
        render.material = inactivate_material;
    }

    private void createEffect()
    {
        GameObject effect = (Resources.Load("Prefabs/FBX/ParticleKillNote", typeof(GameObject))) as GameObject;
        GameObject aux = Instantiate(effect, this.transform);
        Destroy(aux, 1.5f);
    }

}
