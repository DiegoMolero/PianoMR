using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManagerScore
{
    public static List<LvlJson> ReadLvlJSON()
    {
        List<LvlJson> lvlList = new List<LvlJson>();
        //READ FILE
        string path = Path.Combine(Application.persistentDataPath, "score.json");
        string jsonFile = null;
        using (TextReader reader = File.OpenText(path))
        {
            jsonFile = reader.ReadToEnd();
        }
        Debug.Log(jsonFile);

        //PARSE JSON FILE
        var objects = JArray.Parse(jsonFile); // parse as array  
        foreach (JObject root in objects)
        {
            //Debug.Log(root.ToString());
            LvlJson lvl = JsonConvert.DeserializeObject<LvlJson>(root.ToString());
            lvlList.Add(lvl);
        }
        return lvlList;
    }

    public static void StoreLvlJSON(LvlJson aux)
    {
        //READ JSON
        bool inside = false;
        List<LvlJson> lvls = null;
        try
        {
            lvls = JsonManagerScore.ReadLvlJSON();
            //SEARCH LVL
            foreach (LvlJson lvl in lvls)
            {
                Debug.Log(lvl.Lvl.ToString());
                Debug.Log(lvl.Score.ToString());
                //UPDATE IF IS CREATED
                if (lvl.Lvl == aux.Lvl)
                {
                    if (lvl.Score < aux.Score)
                    {
                        lvl.Score = aux.Score;
                    }
                    inside = true;
                }
            }
        }
        catch
        {
            Debug.Log("SALIAO");
        }
        //CREATE IF IS NEW
        Debug.Log(inside);
        if (!inside)
        {
            if (lvls == null)
            {
                lvls = new List<LvlJson>();
            }
            lvls.Add(aux);
        }
        //WRITE FILE
        string path = Path.Combine(Application.persistentDataPath, "score.json");
        string json = JsonConvert.SerializeObject(lvls.ToArray());
        using (TextWriter writer = File.CreateText(path))
        {
            writer.Write(json);
        }
    }

    public static void InitLvlJSON()
    {
        string path = Path.Combine(Application.persistentDataPath, "score.json");
        List<LvlJson> lvlList = new List<LvlJson>();
        lvlList.Add(new LvlJson(1,0));
        string json = JsonConvert.SerializeObject(lvlList.ToArray());
        using (TextWriter writer = File.CreateText(path))
        {
            writer.Write(json);
        }
    }

}


