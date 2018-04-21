using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MusicSheet : MonoBehaviour{

    [Header("Song Values")]
    public int Tempo;   //Beats Per Minute
    public int TotalMeasures; //Compases totales
    public int BeatsPerMeasure; //Numer of quarters per Measure - Numero de negras por compás
    [Header("Notes")]
    public List<NoteMusicSheet> notes;

        // Use this for initialization
        void Start () {
		
	    }
	
	    // Update is called once per frame
	    void Update () {
		
	    }
    public List<NoteMusicSheet> getNotes(int measure, int beat){

        List<NoteMusicSheet> aux = new List<NoteMusicSheet>();

        foreach (NoteMusicSheet note in notes)
        {
            if (note.Measure == measure)
            {
                if (note.Beat == beat)
                {
                    aux.Add(note);
                }
            }
        }
        return aux;
        }
}
