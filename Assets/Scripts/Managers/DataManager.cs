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
    public Dictionary<int, Ending> EndingDict { get; private set; } = new Dictionary<int, Ending>();
    public Dictionary<int, Hint> HintDict { get; private set; } = new Dictionary<int, Hint>();
    public Dictionary<int, Item> ItemDict { get; private set; } = new Dictionary<int, Item>();

    public void Init()
    {
        EndingDict = LoadJson<EndingData, int, Ending>("EndingScript").MakeDictionary();
        HintDict = LoadJson<HintData, int, Hint>("HintScript").MakeDictionary();
        ItemDict = LoadJson<ItemData, int, Item>("ShopItem").MakeDictionary();

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

    public void ClearAllStage()
    {
        UserData.user.id = "";
        UserData.user.gender = "unselected";
        UserData.user.stat1 = 0;
        UserData.user.stat2 = 0;
        UserData.user.stat3 = 0;
        UserData.user.extraStat = 0;
        UserData.user.stage = 1;
        PlayerPrefs.SetInt("extrahealth", 0);
        PlayerPrefs.SetInt("round", PlayerPrefs.GetInt("round") + 1);

        SaveUserDataToJson(UserData);
        Debug.Log(JsonUtility.ToJson(UserData, true));
    }

    public void InitData()
    {
        UserData.user.id = "";
        UserData.user.nickname = "주인공";
        UserData.user.gender = "unselected";
        UserData.user.stat1 = 0;
        UserData.user.stat2 = 0;
        UserData.user.stat3 = 0;
        UserData.user.stage = 1;
        UserData.user.extraStat = 0;
        UserData.user.amber = 0;
        UserData.user.ruby = 0;
        UserData.user.curCostume = 0;

        // Init ending collection
        UserData.user.ending = new bool[12];
        for (int i = 0; i < 12; i++)
            UserData.user.ending[i] = false;

        // Init Costume Collection
        UserData.user.maleCostume = new bool[2] { true, false };
        UserData.user.femaleCostume = new bool[2] { true, false };

        // PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("extrahealth", 0);       // 러브레터
        PlayerPrefs.SetInt("round", 0);             // 회차(돌파권, 난이도)
        PlayerPrefs.SetInt("vibrate", 1);           // 옵션(진동)
        PlayerPrefs.SetInt("isAccessFirst", 0);     // 튜토리얼
        PlayerPrefs.SetFloat("BgmVolume", 0.5f);
        PlayerPrefs.SetFloat("EffectVolume", 0.5f);
        PlayerPrefs.SetInt("maleCostume", 0);       // 남자 코스튬
        PlayerPrefs.SetInt("femaleCostume", 0);     // 여자 코스튬
        PlayerPrefs.SetInt("difficulty", 0);        // 난이도

        SaveUserDataToJson(UserData);
        Debug.Log(JsonUtility.ToJson(UserData, true));
    }

    public void PrintLog()
    {
        string jsonData = JsonUtility.ToJson(UserData, true);
        Debug.Log(jsonData);
    }
}