using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Data
{    
    public string key;
    public string value;

    public Data(string k, string v)
    {
        key = k;
        value = v;
    }
}

public class JsonManager {

    public static List<Data> ItemList = new List<Data>();

    public static void SetValue(string key, string value){
        for(int i = 0; i < ItemList.Count; i++){
            if(ItemList[i].key.Equals(key)){
                ItemList[i].value = value;
                return;
            }
        }
        ItemList.Add(new Data(key, value));
    }

    public static void Save()
    {
        Debug.Log("저장하기");

        JsonData ItemJson = JsonMapper.ToJson(ItemList);

        if(Application.platform == RuntimePlatform.WindowsEditor){
            File.WriteAllText(Application.dataPath 
                                + "/ItemData.json"
                                , ItemJson.ToString());
        }
        else {
            File.WriteAllText(Application.persistentDataPath 
                                + "/ItemData.json"
                                , ItemJson.ToString());
        }
    }

    public static void Load()
    {
        Debug.Log("불러오기");
        string Jsonstring;
        if(Application.platform == RuntimePlatform.WindowsEditor){
            Jsonstring = File.ReadAllText(Application.dataPath
                                                    + "/ItemData.json");
        }
        else {
            Jsonstring = File.ReadAllText(Application.persistentDataPath
                                                    + "/ItemData.json");
        }
        Debug.Log(Jsonstring);

        LoadDataFromString(Jsonstring);
    }

    public static void LoadDataFromString(string Jsonstring){
        ItemList.Clear();        
        JsonData itemData = JsonMapper.ToObject(Jsonstring);

        for(int i = 0; i < itemData.Count; i++)
        {
            ItemList.Add(new Data(itemData[i]["key"].ToString(), itemData[i]["value"].ToString()));
            PlayerPrefs.SetString(itemData[i]["key"].ToString(), itemData[i]["value"].ToString());
        }
    }
}