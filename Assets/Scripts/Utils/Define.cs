using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Background,
        Player,
        Stage,
        Obstacle,
        Item,
    }

    public enum PlayerState
    { 
        Run,
        Jump,
        Die,
        Clear,
    }

    public enum Scene
    {
        Unknown,
        Lobby,
        Game,
        Ending,
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
        Npc0_HappyEnding,
        Npc0_BadEnding,
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
    }

    public static string[] maleStat = { "외모", "집중력", "진실" };
    public static string[] femaleStat = { "섬세함", "안정감", "끈기" };
    public static string[] maleTarget = { "모범생", "소꿉친구", "후배" };
    public static string[] femaleTarget = { "학생회장", "체육부장", "선도부장" };
}
