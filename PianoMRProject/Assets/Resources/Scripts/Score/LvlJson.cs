using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class LvlJson {
    public int Lvl;
    public int Score;
    public int Stars;

    [JsonConstructor]
    public LvlJson(int lvl, int score,int stars)
    {
        Lvl = lvl;
        Score = score;
        Stars = stars;
    }

    public string toJSON()
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }
}
