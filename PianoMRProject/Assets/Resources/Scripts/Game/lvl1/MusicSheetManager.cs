using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSheetManager : MonoBehaviour
{
    private Vector3 position;
    public float Timer = 0.0f;

    public GameObject note;

    //Song values
    private int Tempo;   //Beats Per Minute
    private int TotalMeasures; //Compases totales
    private int BeatsPerMeasure; //Numer of quarters per Measure - Numero de negras por compás

    [Header("Info Values")]
    public int actualMeasure;
    public int actualBeat;
    public int actualScore;
    [Header("Song that will be played")]
    public MusicSheet musicSheet;
    private float tempoSong;
    private float auxTimer;
    private float speed;
    public float initPositionNotes;
    private Vector3 aux_position;
    [Header("User Feedback")]
    public TextMesh scoreText;
    [Header("Prefab loads when the game ends")]
    public GameObject lvl_ends;


    private void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        Tempo = musicSheet.Tempo;
        TotalMeasures = musicSheet.TotalMeasures;
        BeatsPerMeasure = musicSheet.BeatsPerMeasure;
        actualBeat = 1;
        actualMeasure = 1;
        tempoSong = 60f/Tempo;
        auxTimer = Timer;
        this.speed = -0.0945f * (Tempo / 60f);
        actualScore = 0;
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
        InvokeNote();
        actualBeat++;
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
        List<NoteMusicSheet> notes = musicSheet.getNotes(actualMeasure,actualBeat);
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

