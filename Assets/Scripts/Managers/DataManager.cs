using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public UserData UserData { get; set; }
    public Dictionary<int, string[]> ScriptData { get; set; }

    public void Init()
    {
        // Ending Scene Data Load
        ScriptData = new Dictionary<int, string[]>();
        GenerateData();

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
        // UserData = LoadJson<UserData>("UserData");
    }

    Loader LoadJson<Loader>(string path)
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

    void GenerateData()
    {
        ScriptData.Add(0, new string[] { "����� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "����� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "����� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(1, new string[] { "����� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "����� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "����� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(2, new string[] { "�Ҳ�ģ�� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "�Ҳ�ģ�� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "�Ҳ�ģ�� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(3, new string[] { "�Ҳ�ģ�� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "�Ҳ�ģ�� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "�Ҳ�ģ�� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(4, new string[] { "�Ĺ� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "�Ĺ� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "�Ĺ� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(5, new string[] { "�Ĺ� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "�Ĺ� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "�Ĺ� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(6, new string[] { "�л�ȸ�� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "�л�ȸ�� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "�л�ȸ�� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(7, new string[] { "�л�ȸ�� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "�л�ȸ�� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "�л�ȸ�� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(8, new string[] { "ü������ Happy Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "ü������ Happy Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "ü������ Happy Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(9, new string[] { "ü������ Bad Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "ü������ Bad Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "ü������ Bad Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(10, new string[] { "�������� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "�������� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "�������� Happy Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});

        ScriptData.Add(11, new string[] { "�������� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(1/3)",
            "�������� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(2/3)",
            "�������� Bad Ending ��� ��ũ��Ʈ �Դϴ�.(3/3)"});
    }
}
