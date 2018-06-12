using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class LvlJson {
    public int Lvl;
    public int Score;
    public int Stars;
    public int[] LimitStarts;
    [JsonConstructor]
    public LvlJson(int lvl, int score,int stars,int [] limitstarts)
    {
        Lvl = lvl;
        Score = score;
        Stars = stars;
        LimitStarts = limitstarts;
    }

    public string toJSON()
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }
}
