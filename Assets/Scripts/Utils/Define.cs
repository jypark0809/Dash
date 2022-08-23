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
        Self, // 주인공
        Npc1, // 모범생(백설)
        Npc2, // 소꿉친구(차가운)
        Npc3, // 후배(고유미)
        Npc4, // 학생회장(서새한)
        Npc5, // 체육부장(채대성)
        Npc6, // 선도부장(선도진)
        Nerd // 오타쿠
    }

    public static string[] maleStat = { "외모", "집중력", "진실" };
    public static string[] femaleStat = { "섬세함", "애교", "체력" };
    public static string[] maleTarget = { "백설", "차가운", "고유미" };
    public static string[] femaleTarget = { "서새한", "채대성", "선도진" };
    public static string[] npcName = { Managers.Data.UserData.user.nickname, 
        "백설", "차가운", "고유미", "서새한", "채대성", "선도진", "오탁후" };
}