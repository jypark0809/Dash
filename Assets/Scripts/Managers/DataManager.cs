using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDictionary();
}

public class DataManager
{
    public Dictionary<int, Ending> EndingDict { get; private set; } = new Dictionary<int, Ending>();
    public Dictionary<int, Hint> HintDict { get; private set; } = new Dictionary<int, Hint>();
    public Dictionary<int, Item> ItemDict { get; private set; } = new Dictionary<int, Item>();

    public void Init()
    {
        EndingDict = LoadJson<EndingData, int, Ending>("EndingScript").MakeDictionary();
        HintDict = LoadJson<HintData, int, Hint>("HintScript").MakeDictionary();
        ItemDict = LoadJson<ItemData, int, Item>("ShopItem").MakeDictionary();

    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}