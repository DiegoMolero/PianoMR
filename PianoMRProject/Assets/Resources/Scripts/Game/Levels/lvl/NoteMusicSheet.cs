using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NoteMusicSheet {
    public int Measure;
    public int Beat;
    public int PartBeat=1;
    public PianoDriver.KeyNote Note;
    public int Finger;

    public NoteMusicSheet(int measure, int beat, int partBeat, PianoDriver.KeyNote note, int finger)
    {
        Measure = measure;
        Beat = beat;
        PartBeat = partBeat;
        Note = note;
        Finger = finger;
    }
}
