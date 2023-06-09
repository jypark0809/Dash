using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[Serializable]
public class GameData
{
    public string id;
    public string nickname;
    public string gender;
    public int stat1;
    public int stat2;
    public int stat3;
    public int extraStat;
    public int extraHealth;
    public int stage;
    public int round;
    public int amber;
    public int ruby;
    public int curCostume;
    public bool[] ending;
    public bool[] maleCostume;
    public bool[] femaleCostume;

    public GameData()
    {
        id = "";
        nickname = "";
        gender = "unselected";
        stat1 = 0;
        stat2 = 0;
        stat3 = 0;
        extraStat = 0;
        extraHealth = 0; // 러브레터
        stage = 1;
        round = 0;
        amber = 0;
        ruby = 0;
        curCostume = 0;

        // Init ending collection
        ending = new bool[12];
        for (int i = 0; i < 12; i++)
            ending[i] = false;

        // Init Costume Collection
        maleCostume = new bool[2] { true, false };
        femaleCostume = new bool[2] { true, false };
    }
}


public class GameManager
{
    public GameData _gameData = new GameData();
    public GameData SaveData
    {
        get { return _gameData; }
        set
        {
            _gameData = value;
            SaveGame();
        }
    }

    public bool IsLoaded = false;
    public void Init()
    {
        _path = Application.persistentDataPath + "/SaveData.json";

        if (LoadGame())
            return;

        IsLoaded = true;
        SaveGame();
    }

    #region SaveData 초기화
    public void ClearAllStage()
    {
        _gameData.id = "";
        _gameData.gender = "unselected";
        _gameData.stat1 = 0;
        _gameData.stat2 = 0;
        _gameData.stat3 = 0;
        _gameData.extraStat = 0;
        _gameData.extraHealth = 0;
        _gameData.stage = 1;

        // n 회차 + 1
        int round = SaveData.round + 1;
        _gameData.round = round;

        Managers.Game.SaveGame();
    }

    // Cheating
    public void InitData()
    {
        _gameData.id = "";
        _gameData.gender = "unselected";
        _gameData.stat1 = 0;
        _gameData.stat2 = 0;
        _gameData.stat3 = 0;
        _gameData.extraStat = 0;
        _gameData.stage = 1;
        _gameData.extraHealth = 0;
        _gameData.nickname = "주인공";
        _gameData.round = 0;
        _gameData.amber = 0;
        _gameData.ruby = 0;
        _gameData.curCostume = 0;
        SaveData = _gameData;

        // Init ending collection
        _gameData.ending = new bool[12];
        for (int i = 0; i < 12; i++)
            _gameData.ending[i] = false;

        // Init Costume Collection
        _gameData.maleCostume = new bool[2] { true, false };
        _gameData.femaleCostume = new bool[2] { true, false };

        // PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("extrahealth", 0);       // 러브레터
        PlayerPrefs.SetInt("vibrate", 1);           // 옵션(진동)
        PlayerPrefs.SetInt("isAccessFirst", 0);     // 튜토리얼
        PlayerPrefs.SetFloat("BgmVolume", 0.5f);
        PlayerPrefs.SetFloat("EffectVolume", 0.5f);
        PlayerPrefs.SetInt("maleCostume", 0);       // 남자 코스튬
        PlayerPrefs.SetInt("femaleCostume", 0);     // 여자 코스튬
        PlayerPrefs.SetInt("difficulty", 0);        // 난이도

        SaveGame();
    }
    #endregion

    #region Save&Load
    string _path;
    public void SaveGame()
    {
        string jsonStr = JsonConvert.SerializeObject(Managers.Game.SaveData);

        File.WriteAllText(_path, jsonStr);
    }

    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonConvert.DeserializeObject<GameData>(fileStr);
        if (data != null)
            Managers.Game.SaveData = data;


        IsLoaded = true;
        return true;
    }
    #endregion
}