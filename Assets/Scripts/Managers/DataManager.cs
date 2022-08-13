using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDictionary();
}

public class DataManager
{
    public UserData UserData { get; set; }
    public Dictionary<int, Script> ScriptDict { get; private set; } = new Dictionary<int, Script>();

    public void Init()
    {
        ScriptDict = LoadJson<ScriptTempData, int, Script>("EndingScript").MakeDictionary();

        // User Data Load
        if (File.Exists(Path.Combine(Application.persistentDataPath, "UserData.json")))
        {
            UserData = LoadUserDataFromJson<UserData>();
        }
        else
        {
            UserData = new UserData();
            InitData();
        }
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public T LoadUserDataFromJson<T>()
    {
        string path = Path.Combine(Application.persistentDataPath, "UserData.json");
        string userData = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(userData);
        
    }

    public void SaveUserDataToJson(UserData user)
    {
        string jsonData = JsonUtility.ToJson(user, true);
        string path = Path.Combine(Application.persistentDataPath, "UserData.json");
        File.WriteAllText(path, jsonData);
    }

    public void InitData()
    {
        UserData.user.id = "";
        UserData.user.nickname = "";
        UserData.user.gender = "unselected";
        UserData.user.stat1 = 0;
        UserData.user.stat2 = 0;
        UserData.user.stat3 = 0;
        UserData.user.prevStat = 0;
        UserData.user.stage = 1;
        UserData.user.amber = 0;
        UserData.user.ruby = 0;

        SaveUserDataToJson(UserData);
        Debug.Log(JsonUtility.ToJson(UserData, true));
    }

    public void PrintLog()
    {
        string jsonData = JsonUtility.ToJson(UserData, true);
        Debug.Log(jsonData);
    }
}
