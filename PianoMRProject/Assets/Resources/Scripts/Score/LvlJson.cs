using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LvlJson {
    public int Lvl;
    public int Score;

    public LvlJson(int lvl, int score)
    {
        Lvl = lvl;
        Score = score;
    }

    public string toJSON()
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }
}
