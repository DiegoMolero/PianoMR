using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    private Vector3 position;
    public float Timer = 0.0f;

    public GameObject note;

    [Header("Song Values")]
    public int Tempo;   //Beats Per Minute
    public int TotalMeasures; //Compases totales
    public int BeatsPerMeasure; //Numer of quarters per Measure - Numero de negras por compás


    [Header("Info Values")]
    public int actualMeasure;
    public int actualBeat;

    private float tempoSong;
    private float auxTimer;
    private float speed;

    private void Awake()
    {
        position = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().position;
        position += GameObject.FindGameObjectWithTag("Triggers").GetComponent<Transform>().position;

    }
    // Use this for initialization
    void Start()
    {
        actualBeat = 1;
        actualMeasure = 1;
        tempoSong = 60f/Tempo;
        auxTimer = Timer;
        this.speed = -0.0945f * (Tempo / 60);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimmer();
        UpdateSong();
    }

    private void UpdateTimmer()
    {
        Timer += Time.deltaTime;
    }

    private void UpdateSong()
    {
        if (Timer >= auxTimer + tempoSong) //New Beat
        {
            auxTimer = Timer;
            UpdateBeat();
        }
    }
    private void UpdateBeat()
    {
        actualBeat++;
        InvokeNote();
        if (actualBeat > BeatsPerMeasure)
        {
            UpdateMeasure();
            actualBeat = 1;
        }
    }
    private void UpdateMeasure()
    {
        actualMeasure++;
        if(actualMeasure > TotalMeasures)
        {
            Debug.Log("FIN");
        }
    }

    private void InvokeNote()
    {
        Vector3 aux_position = GameObject.FindGameObjectWithTag("Triggers").GetComponent<Transform>().position + new Vector3(0f, 0.6f, 0f);
        GameObject aux = Instantiate(note);
        aux.name = aux.transform.name.Replace("(Clone)", "");
        Note aux_note = aux.AddComponent<Note>();
        aux_note.Initialize(PianoDriver.KeyNote.DO, aux_position, speed);
        Destroy(aux, tempoSong * 7); 
        /*
        GameObject aux = Instantiate(note);
        aux.name = aux.transform.name.Replace("(Clone)", "");
        aux.transform.position = GameObject.FindGameObjectWithTag("Triggers").GetComponent<Transform>().position + new Vector3(0f,0.3f,0f);
        */
    }
}

