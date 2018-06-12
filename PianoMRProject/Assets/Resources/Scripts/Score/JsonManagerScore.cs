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
        //Debug.Log(jsonFile); SEE CONTENT OF JSON FILE

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

    public static LvlJson ReadLvlJSON(int level)
    {
        List<LvlJson> lvlList = ReadLvlJSON();
        if (lvlList == null) return null;
        foreach (LvlJson aux in lvlList)
        {
            if (aux.Lvl == level)
            {
                return aux;
            }
        }
        return null;
    }

    public static bool StoreLvlJSON(LvlJson aux)
    {
        //READ JSON
        bool inside = false;
        bool new_record = false;
        List<LvlJson> lvls = null;
        try
        {
            lvls = JsonManagerScore.ReadLvlJSON();
            //SEARCH LVL
            foreach (LvlJson lvl in lvls)
            {
                //UPDATE IF IS CREATED
                if (lvl.Lvl == aux.Lvl)
                {
                    if (lvl.Score < aux.Score) // IF NEW RECORD
                    {
                        lvl.Score = aux.Score;
                        if (lvl.Stars < aux.Stars) { //IF NEW STAR OBTAINED
                            lvl.Stars = aux.Stars;
                            lvl.LimitStarts = aux.LimitStarts;
                        } 
                        new_record = true;
                    }
                    inside = true;
                }
            }
        }
        catch
        {
        }
        //CREATE IF IS NEW
        if (!inside)
        {
            if (lvls == null)
            {
                lvls = new List<LvlJson>();
            }
            lvls.Add(aux);
            new_record = true;
        }
        //WRITE FILE
        string path = Path.Combine(Application.persistentDataPath, "score.json");
        string json = JsonConvert.SerializeObject(lvls.ToArray());
        using (TextWriter writer = File.CreateText(path))
        {
            writer.Write(json);
        }
        return new_record;
    }

    public static void InitLvlJSON()
    {
        int aux_limit = 5;
        string path = Path.Combine(Application.persistentDataPath, "score.json");
        List<LvlJson> lvlList = new List<LvlJson>();
        int[] limits = new int[] {0,0,0,0,0};
        for(int index = 1; index <= aux_limit; index++)
        {
            lvlList.Add(new LvlJson(index, 0, 0, limits));
        }
        string json = JsonConvert.SerializeObject(lvlList.ToArray());
        using (TextWriter writer = File.CreateText(path))
        {
            writer.Write(json);
        }
    }

}


