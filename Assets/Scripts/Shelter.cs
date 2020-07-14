using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour
{

    public GameObject LootItem;
    public Vector2 tpPos;

    public LootType loot1;
    public LootType loot2;

    public int lootAmount = 1;
    public bool predefined = false;

    public List<List<int>> lootTable;

    void Start()
    {
        if(!predefined) {
            loot1 = SetLoot1Type();
            loot2 = SetLoot2Type();

            InitLoot1Table();
            InitLoot1Amount();
        }
    }

    public void SpawnLoot1(Vector2 pos)
    {
        if (loot1 != LootType.NONE)
        {
            GameObject lootDrop = Instantiate(LootItem, pos, Quaternion.identity);
            Loot lootScript = lootDrop.GetComponent<Loot>();
            lootScript.InitializeLoot(loot1, lootAmount);
            loot1 = LootType.NONE;
        }
    }

    public void SpawnLoot2(Vector2 pos)
    {
        if (loot2 != LootType.NONE)
        {
            GameObject lootDrop = Instantiate(LootItem, pos, Quaternion.identity);
            Loot lootScript = lootDrop.GetComponent<Loot>();
            lootScript.InitializeLoot(loot2, 1);
            loot2 = LootType.NONE;
        }
    }

    LootType SetLoot1Type()
    {
        int RNG = Random.Range(0, 100);
        if (RNG >= 40 && RNG < 80)
        {
            return LootType.GUN_AMMO;
        }
        else if (RNG >= 80 && RNG < 90)
        {
            return LootType.SHOTGUN_AMMO;
        }
        else if (RNG >= 90 && RNG < 100)
        {
            return LootType.SNIPER_AMMO;
        }
        else
        {
            return LootType.HEALTH_PACK;
        }
    }

    LootType SetLoot2Type()
    {
        int RNG = Random.Range(0, 100);
        if (RNG >= 20 && RNG < 50)
        {
            return LootType.RIFLE;
        }
        else if (RNG >= 50 && RNG < 75)
        {
            return LootType.SHOTGUN;
        }
        else if (RNG >= 75 && RNG < 100)
        {
            return LootType.SNIPER;
        }
        else
        {
            return LootType.PISTOL;
        }
    }

    // Loot Table Format = [chances, amount]
    void InitLoot1Table()
    {
        if (loot1 == LootType.GUN_AMMO)
        {
            lootTable = new List<List<int>>() {
                new List<int>() { 60, 30 },
                new List<int>() { 30, 50 },
                new List<int>() { 10, 100 },
            };
        }
        else if (loot1 == LootType.SHOTGUN_AMMO || loot1 == LootType.SNIPER_AMMO)
        {
            lootTable = new List<List<int>>() {
                new List<int>() { 60, 5 },
                new List<int>() { 30, 10 },
                new List<int>() { 10, 30 },
            };
        }
        else if (loot1 == LootType.HEALTH_PACK)
        {
            lootTable = new List<List<int>>() {
                new List<int>() { 60, 2 },
                new List<int>() { 30, 5 },
                new List<int>() { 10, 10 },
            };
        }
    }

    void InitLoot1Amount()
    {
        lootAmount = RNGLootAmount();
    }

    int RNGLootAmount()
    {
        int RNG = Random.Range(0, 100);
        int chances = 0;
        foreach (List<int> lootTableItem in lootTable)
        {
            chances += lootTableItem[0];
            if (chances < RNG)
            {
                return lootTableItem[1];
            }
        }
        return lootTable[0][1];
    }
}
