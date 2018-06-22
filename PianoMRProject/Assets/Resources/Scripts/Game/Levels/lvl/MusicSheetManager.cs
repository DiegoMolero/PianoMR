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
    [Header("Info Level")]
    public int Level = 0;
    [Header("Info Values")]
    public int actualMeasure;
    public int actualBeat;
    public int actualScore;
    [Header("Song that will be played")]
    public MusicSheet musicSheet;
    public string FileName;
    private float tempoSong;
    private float speed;
    private int partBeat;
    private float initPositionNotes;
    private Vector3 aux_position;
    [Header("User Feedback")]
    public FeedbackScore score;
    [Header("Prefab loads when the game ends")]
    public GameObject lvl_ends;
    private float auxTimer=0;
    [Header("Prefab loads when the game ends")]
    public List<int> StarsLimits;
    private XMLReaderMusic xmlReader;

    private void Awake()
    {


    }
    // Use this for initialization
    void Start()
    {
        xmlReader = this.GetComponent<XMLReaderMusic>();
        if (musicSheet == null)
        {
            xmlReader.ReadLocalFile(FileName);
            musicSheet= xmlReader.musicSheet;
        }
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
        if(actualMeasure > TotalMeasures) //IF THE LEVEL ENDS
        {
            GameObject lvl_aux = Instantiate(lvl_ends);
            bool record = false;
            if (Level != 0)
            {
                int stars = calculateStars();
                record = JsonManagerScore.StoreLvlJSON(new LvlJson(Level, actualScore,stars, StarsLimits.ToArray()));
                lvl_aux.GetComponent<StarDisplayEffect>().DisplayStars(stars);
            }
            //STORE RESULT
            lvl_aux.GetComponent<FinalResult>().scoreObtained = actualScore;
            lvl_aux.GetComponent<FinalResult>().New_record = record;
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
        actualScore = score.IncreaseScore();
    }

    public void DecreaseScore()
    {
        actualScore = score.DecreaseScore();
    }

    private int calculateStars()
    {
        int counter = 0;
        foreach (int limit in StarsLimits)
        {
            if (actualScore > limit) counter++;
            else
            {
                break;
            }
        }
        return counter;
    }
}

