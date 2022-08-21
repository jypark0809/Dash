using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region User

[Serializable]
public class User
{
    public string id;
    public string nickname;
    public string gender; // ĳ���� ����
    public int stat1;
    public int stat2;
    public int stat3;
    public int stage; // ���ݱ��� Ŭ������ ��������
    public int amber; // ���� ��ȭ
    public int ruby; // ���� ��ȭ
    public bool[] ending; // ����
}

[Serializable]
public class UserData
{
    public User user = new User();
}

#endregion

#region Script

[Serializable]
public class Ending
{
    public int endingId;
    public int index; // �ƾ� ������ index
    public Script[] scripts;
}

[Serializable]
public class Script
{
    public int npcId;
    public int imageId;
    public string line;
}

[Serializable]
public class EndingData : ILoader<int, Ending>
{
    public List<Ending> endings = new List<Ending>();

    public Dictionary<int, Ending> MakeDictionary()
    {
        Dictionary<int, Ending> dict = new Dictionary<int, Ending>();

        foreach (Ending ending in endings)
            dict.Add(ending.endingId, ending);

        return dict;
    }
}

#endregion Script