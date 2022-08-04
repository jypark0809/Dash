using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string id;
    public string nickname;
    public string gender;
    public int stat1;
    public int stat2;
    public int stat3;
    public int prevStat;
    public int stage;
    public int amber;
    public int ruby;
}

[Serializable]
public class UserData
{
    public User user = new User();
}