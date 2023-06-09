using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Script

[Serializable]
public class Hint
{
    public int hintId;
    public Script[] scripts;
}

[Serializable]
public class Ending
{
    public int endingId;
    public int index;
    public Script[] scripts;
}

[Serializable]
public class Script
{
    public int npcId;
    public int imageId;
    public string line;
}

[Serializable]
public class EndingData : ILoader<int, Ending>
{
    public List<Ending> endings = new List<Ending>();

    public Dictionary<int, Ending> MakeDictionary()
    {
        Dictionary<int, Ending> dict = new Dictionary<int, Ending>();

        foreach (Ending ending in endings)
            dict.Add(ending.endingId, ending);

        return dict;
    }
}

[Serializable]
public class HintData : ILoader<int, Hint>
{
    public List<Hint> hints = new List<Hint>();

    public Dictionary<int, Hint> MakeDictionary()
    {
        Dictionary<int, Hint> dict = new Dictionary<int, Hint>();

        foreach (Hint hint in hints)
            dict.Add(hint.hintId, hint);

        return dict;
    }
}

#endregion Script

#region ShopItem

[Serializable]
public class Item
{
    public int itemId;
    public string itemName;
    public string type;
    public int price;
}

[Serializable]
public class ItemData : ILoader<int, Item>
{
    public List<Item> items = new List<Item>();

    public Dictionary<int, Item> MakeDictionary()
    {
        Dictionary<int, Item> dict = new Dictionary<int, Item>();

        foreach (Item item in items)
            dict.Add(item.itemId, item);

        return dict;
    }
}

#endregion