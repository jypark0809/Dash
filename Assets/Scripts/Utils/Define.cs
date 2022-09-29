using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Map,
        Player,
        Stage,
        Obstacle,
        Item,
    }

    public enum PlayerState
    { 
        Run,
        Jump,
        Fight,
        Die,
        Clear,
    }

    public enum Scene
    {
        Unknown,
        Title,
        Lobby,
        Game,
        Ending,
        ReplayEnding,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
        PointerDown,
    }

    public enum ObstacleType
    {
        Unknown,
        Signage,
        VaultingHorse,
        Books,
        Pool,
        Spider,
        Teacher,
    }

    public enum ItemType
    {
        Unknown,
        Letter,
        Coffee,
        JumpingShoes,
    }

    public enum EndingId
    {
        Unknown,
        Npc1_HappyEnding,
        Npc1_BadEnding,
        Npc2_HappyEnding,
        Npc2_BadEnding,
        Npc3_HappyEnding,
        Npc3_BadEnding,
        Npc4_HappyEnding,
        Npc4_BadEnding,
        Npc5_HappyEnding,
        Npc5_BadEnding,
        Npc6_HappyEnding,
        Npc6_BadEnding,
    }

    public enum NpcId
    {
        Self, // Player
        Npc1, // 백설
        Npc2, // 차가윤
        Npc3, // 고유미
        Npc4, // 서새한
        Npc5, // 채대성
        Npc6, // 선도진
        Nerd, // 오탁후
        Npc7, // 남학생
        Npc8, // 여학생
        Teacher, // 선생님
    }

    public enum ShopItemId
    {
        none,
        loveletter1,
        loveletter2,
        amber100,
        maleCostume,
        femaleCostume,
    }

    public static string[] maleStat = { "외모", "진실", "집중" };
    public static string[] femaleStat = { "섬세함", "애교", "체력" };
    public static string[] maleTarget = { "백설", "차가윤", "고유미" };
    public static string[] femaleTarget = { "서새한", "채대성", "선도진" };
    public static string[] npcName = { Managers.Data.UserData.user.nickname, 
        "백설", "차가윤", "고유미", "서새한", "채대성", "선도진", "오탁후", "남학생", "여학생", "선생님" };
}