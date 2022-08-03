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
        Login,
        Lobby,
        Game,
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
}
