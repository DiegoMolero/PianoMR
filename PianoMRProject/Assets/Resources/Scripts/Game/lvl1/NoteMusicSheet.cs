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

}
