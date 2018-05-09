using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

public class XMLReaderMusic : MonoBehaviour {
    private List<string> data;
    public string file;

    public List<NoteMusicSheet> Notes;
    public float Tempo;
    public int TotalMeasures;
    public int BeatsPerMeasure;

    //Aux variable to calculate the position of notes
    private int previous_measure;
    private int previous_beat;
    private int previous_partbeat;

    private int actual_measure;
    private int actual_beat;
    private int actual_partbeat;

    private void Awake()
    {
        TotalMeasures = 0;
        actual_measure = 1;
        actual_beat = 1;
        actual_partbeat = 1;
        this.Notes = Read(file);
        MusicSheet musicSheet = new MusicSheet();
        musicSheet.BeatsPerMeasure = BeatsPerMeasure;
        musicSheet.InitPositionNotes = 0.445f;
        musicSheet.notes = Notes;
        musicSheet.Tempo = Tempo;
        musicSheet.TotalMeasures = TotalMeasures;
        GameObject.FindGameObjectWithTag("SheetManager").GetComponent<MusicSheetManager>().musicSheet = musicSheet;
    }
    // Use this for initialization
    void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<NoteMusicSheet> Read(string file)
    {
        List<NoteMusicSheet> MusicSheetNotes = new List<NoteMusicSheet>();
        string path = "Stages/" + file;
        TextAsset e = Resources.Load<TextAsset>(path);

        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ProhibitDtd = false;

        XmlReader reader = XmlReader.Create(new StringReader(e.text), settings);

        XDocument _xml = XDocument.Load(reader);
        IEnumerable<XElement> _dict = _xml.Element("score-partwise").Element("part").Elements("measure");
        //SetUp MusicSheet
        BeatsPerMeasure = Int32.Parse(_xml.Element("score-partwise").Element("part").Element("measure").Element("attributes").Element("time").Element("beats").Value);
        Tempo = float.Parse(_xml.Element("score-partwise").Element("part").Element("measure").Element("direction").Element("direction-type").Element("metronome").Element("per-minute").Value);
        Debug.Log(Tempo);

        //Read Notes
        foreach (XElement measure in _dict)
        {
            TotalMeasures++;
            actual_measure = Int32.Parse(measure.Attribute("number").Value);
            updateMeasure();
            foreach (XElement note in measure.Elements("note"))
            {
                try
                {
                    if (Int32.Parse(note.Element("voice").Value) == 1)
                    {
                        string aux_type = note.Element("type").Value;
                        if (note.Element("rest") != null) //IF IS A REST
                        {
                            updatePositionNote(aux_type);
                        }
                        if (note.Element("pitch") != null) //IF HAS A PITCH
                        {
                            NoteMusicSheet noteMusicSheet = null;
                            XElement pitch = note.Element("pitch");
                            string aux_note = pitch.Element("step").Value;
                            int aux_octave = Int32.Parse(pitch.Element("octave").Value);
                            Debug.Log("LEYENDO: " + aux_note + " " + aux_octave + " measuere: " + actual_measure+ " type "+ aux_type);
                           
                            if (note.Element("chord") == null) //IF IS NOT A CHORD
                            {
                                noteMusicSheet = new NoteMusicSheet(actual_measure,actual_beat,actual_partbeat,ParsePitch(aux_note,aux_octave),1);
                                updatePositionNote(aux_type);
                            }
                            else
                            {
                                noteMusicSheet = new NoteMusicSheet(previous_measure, previous_beat, previous_partbeat, ParsePitch(aux_note, aux_octave), 1);
                            }
                            MusicSheetNotes.Add(noteMusicSheet);
                        }


                    }

                }
                catch (Exception exc)
                {
                    Debug.Log(exc.ToString());
                }

            }
        }
        return MusicSheetNotes;
    }

    private PianoDriver.KeyNote ParsePitch(string note, int octave)
    {
        if (octave != 4 && octave != 5) {
            throw new System.ArgumentException("Only octaves from 4 to 5 are allowed");
        }
        else
        {
            switch (note)
            {
                case "C":
                    if (octave == 4) return PianoDriver.KeyNote.DO;
                    else if(octave == 5) return PianoDriver.KeyNote.DO2;
                    break;
                case "D":
                    return PianoDriver.KeyNote.RE;
                case "E":
                    return PianoDriver.KeyNote.MI;
                case "F":
                    return PianoDriver.KeyNote.FA;
                case "G":
                    return PianoDriver.KeyNote.SOL;
                case "A":
                    return PianoDriver.KeyNote.LA;
                case "B":
                    return PianoDriver.KeyNote.SI;
            }
        }
        throw new System.ArgumentException("Note not found");
    }

    private void updatePositionNote(string type)
    {
        previous_measure = actual_measure;
        previous_beat = actual_beat;
        previous_partbeat = actual_partbeat;
        switch (type)
        {
            case "whole":
                updatePartBeat(4*4);
                break;
            case "half":
                updatePartBeat(4 * 2);
                break;
            case "quarter":
                updatePartBeat(4);
                break;
            case "eighth":
                updatePartBeat(2);
                break;
            case "16th":
                updatePartBeat(1);
                break;

        }
    }
    private void updatePartBeat(int partBeat)
    {
        for(int i= partBeat;i > 0; i--)
        {
            actual_partbeat++;
            if (actual_partbeat > 4)
            {
                actual_partbeat = 1;
                updateBeat();
            }
        }

    }

    private void updateBeat()
    {
        actual_beat++;
        if (actual_beat > BeatsPerMeasure)
        {
            actual_beat = 1;
            actual_measure++;
            
        }
    }
    private void updateMeasure()
    {
        actual_partbeat = 1;
        actual_beat = 1;
    }


}

