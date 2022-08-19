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
        GameOver,
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
        Npc1 = 1, // 모범생
        Npc2, // 소꿉친구
        Npc3, // 후배
        Npc4, // 학생회장
        Npc5, // 체육부장
        Npc6, // 선도부장
        Nerd // 오타쿠
    }

    public static string[] maleStat = { "외모", "집중력", "진실" };
    public static string[] femaleStat = { "섬세함", "안정감", "끈기" };
    public static string[] maleTarget = { "모범생", "소꿉친구", "후배" };
    public static string[] femaleTarget = { "학생회장", "체육부장", "선도부장" };
}
