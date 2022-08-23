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
        Self, // ���ΰ�
        Npc1, // �����(�鼳)
        Npc2, // �Ҳ�ģ��(������)
        Npc3, // �Ĺ�(������)
        Npc4, // �л�ȸ��(������)
        Npc5, // ü������(ä�뼺)
        Npc6, // ��������(������)
        Nerd // ��Ÿ��
    }

    public static string[] maleStat = { "�ܸ�", "���߷�", "����" };
    public static string[] femaleStat = { "������", "�ֱ�", "ü��" };
    public static string[] maleTarget = { "�鼳", "������", "������" };
    public static string[] femaleTarget = { "������", "ä�뼺", "������" };
    public static string[] npcName = { Managers.Data.UserData.user.nickname, 
        "�鼳", "������", "������", "������", "ä�뼺", "������", "��Ź��" };
}