using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string id;
    public string nickname;
    public string gender; // 캐릭터 성별
    public int stat1;
    public int stat2;
    public int stat3;
    public int prevStat;
    public int stage; // 지금까지 클리어한 스테이지
    public int amber; // 무료 재화
    public int ruby; // 유료 재화
}

[Serializable]
public class UserData
{
    public User user = new User();
}