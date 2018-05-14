using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MusicSheetManager : MonoBehaviour
{
    private Vector3 position;
    public float TimerSeconds = 0.0f;
    public Stopwatch sw;
    public GameObject note;

    //Song values
    private float Tempo;   //Beats Per Minute
    private int TotalMeasures; //Compases totales
    private int BeatsPerMeasure; //Numer of quarters per Measure - Numero de negras por compás

    [Header("Info Values")]
    public int actualMeasure;
    public int actualBeat;
    public int actualScore;
    [Header("Song that will be played")]
    public MusicSheet musicSheet;
    private float tempoSong;
    private float speed;
    private int partBeat;
    private float initPositionNotes;
    private Vector3 aux_position;
    [Header("User Feedback")]
    public TextMesh scoreText;
    [Header("Prefab loads when the game ends")]
    public GameObject lvl_ends;
    private float auxTimer=0;

    private void Awake()
    {


    }
    // Use this for initialization
    void Start()
    {
        initPositionNotes = musicSheet.InitPositionNotes;
        Tempo = musicSheet.Tempo;
        TotalMeasures = musicSheet.TotalMeasures;
        BeatsPerMeasure = musicSheet.BeatsPerMeasure;
        actualBeat = 1;
        actualMeasure = 1;
        tempoSong = 60f / Tempo;
        sw = new Stopwatch();
        this.speed = -0.0945f * (Tempo / 60f);
        actualScore = 0;
        partBeat = 1;
        sw.Start();
        UpdateTimmer();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSong();
    }

    private void UpdateTimmer()
    {
        // Timer += (Time.fixedDeltaTime-Time.deltaTime);
        {
            auxTimer = sw.ElapsedMilliseconds / 1000f;
            //UnityEngine.Debug.Log("SUMA: " + sw.Elapsed.Seconds + "  " + sw.ElapsedMilliseconds / 1000f+"  = "+auxTimer);
        }
    }

    private void UpdateSong()
    {
        float real_auxTimer = sw.ElapsedMilliseconds / 1000f;
        if (real_auxTimer >= (auxTimer + (float)tempoSong/4)) //New PartBeat
        {
            UpdateBeatPart();
            auxTimer = real_auxTimer;


        }
    }
    private void UpdateBeat()
    {
        actualBeat++;
        if (actualBeat > BeatsPerMeasure)
        {
            UpdateMeasure();
            actualBeat = 1;
        }
    }
    private void UpdateBeatPart()
    {
        InvokeNote();
        partBeat++;
        if(partBeat > 4)
        {
            UpdateBeat();
            partBeat = 1;
        }
    }
    private void UpdateMeasure()
    {
        actualMeasure++;
        if(actualMeasure > TotalMeasures)
        {
            GameObject lvl_aux = Instantiate(lvl_ends);
            lvl_aux.GetComponent<FinalResult>().scoreObtained = (int)((float)actualScore / (float)musicSheet.notes.Capacity * 100);
            lvl_aux.name = lvl_aux.transform.name.Replace("(Clone)", "");
            lvl_aux.transform.position = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().position;
            lvl_aux.transform.rotation = GameObject.FindGameObjectWithTag("Piano").GetComponent<Transform>().rotation;
            Destroy(this.transform.parent.gameObject);
        }
    }


    private void InvokeNote()
    {
        List<NoteMusicSheet> notes = musicSheet.getNotes(actualMeasure,actualBeat,partBeat);
        if (notes == null) return;
        foreach (NoteMusicSheet note_musicsheet in notes)
        {
            GameObject aux = Instantiate(note, GameObject.FindGameObjectWithTag("Triggers").GetComponent<Transform>());
            aux.name = aux.transform.name.Replace("(Clone)", "");
            aux.GetComponent<Note>().Initialize(note_musicsheet.Note, initPositionNotes, speed, note_musicsheet.Finger);
            Destroy(aux, tempoSong * 8);
        }
    }

    public void IncreaseScore()
    {
        actualScore++;
        float aux_score = (float)actualScore / (float)musicSheet.notes.Capacity * 100;
        scoreText.text = "Score " + (int)aux_score + "%";
    }

    public void DecreaseScore()
    {
        actualScore--;
        float aux_score = (float)actualScore / (float)musicSheet.notes.Capacity * 100;
        scoreText.text = "Score " + (int)aux_score + "%";
    }
}

