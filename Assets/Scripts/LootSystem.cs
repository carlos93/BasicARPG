using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LootItems
{
    public LootItems(int gold)
    {
        this.gold = gold;
        this.items = new List<int>();
    }

    public int gold;
    public List<int> items;
}

public class LootSystem
{
    public static LootSystem instance = null;

    private Dictionary<int, LootItems> lootMap;

    public LootSystem()
    {
        if (instance == null)
            instance = this;

        lootMap = new Dictionary<int, LootItems>();

        LootItems loot = new LootItems(10);
        lootMap.Add(1, loot);
    }

    public LootItems GetLootForCreature(int creatureId)
    {
        lootMap.TryGetValue(creatureId, out LootItems loot);
        return loot;
    }
}
