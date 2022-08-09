using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string id;
    public string nickname;
    public string gender; // ĳ���� ����
    public int stat1;
    public int stat2;
    public int stat3;
    public int prevStat;
    public int stage; // ���ݱ��� Ŭ������ ��������
    public int amber; // ���� ��ȭ
    public int ruby; // ���� ��ȭ
}

[Serializable]
public class UserData
{
    public User user = new User();
}