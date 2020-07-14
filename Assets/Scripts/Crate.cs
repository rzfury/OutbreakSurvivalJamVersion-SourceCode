using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    Animation anim;

    public GameObject crateBreakObject;
    public GameObject lootObject;

    public bool predefined = false;
    public int crateDurability = 10;
    public LootType loot = LootType.NONE;
    public int lootAmount = 1;
    public List<List<int>> lootTable;

    public void Break(int damage) {     
        crateDurability = Mathf.Max(crateDurability - damage, 0);
        if(anim.isPlaying) {
            anim.Rewind("crate_breaking");
        }
        else {
            anim.Play("crate_breaking");
        }

        if(crateDurability == 0) {
            BreakImmediately();
        }
    }

    public void BreakImmediately() {
        GameObject crateBreak = Instantiate(crateBreakObject, transform.position, Quaternion.identity);
        GameObject lootItem = Instantiate(lootObject, transform.position, Quaternion.identity);
        Loot lootObj = lootItem.GetComponent<Loot>();

        Destroy(crateBreak, 0.9f);
        Destroy(gameObject);
        
        lootObj.InitializeLoot(loot, lootAmount);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        
        if(!predefined) {
            InitLootType();
            InitLootTable();
            InitLootAmount();
        }
    }

    void InitLootType()
    {
        int RNG = Random.Range(0, 100);
        if(RNG >= 40 && RNG < 60) {
            loot = LootType.GUN_AMMO;
        }
        else if(RNG >= 60 && RNG < 70) {
            loot = LootType.SHOTGUN_AMMO;
        }
        else if(RNG >= 70 && RNG < 80) {
            loot = LootType.SNIPER_AMMO;
        }
        else if(RNG >= 80 && RNG < 90) {
            loot = LootType.PISTOL;
        }
        else if(RNG >= 90 && RNG < 100) {
            int RNG2 = Random.Range(0, 4);
            if(RNG == 2) {
                loot = LootType.SHOTGUN;
            }
            else if(RNG == 3) {
                loot = LootType.SNIPER;
            }
            else {
                loot = LootType.RIFLE;
            }
        }
        else {
            loot = LootType.HEALTH_PACK;
        }
    }

    // Loot Table Format = [chances, amount]
    void InitLootTable()
    {
        if (loot == LootType.GUN_AMMO)
        {
            lootTable = new List<List<int>>() {
                new List<int>() { 60, 10 },
                new List<int>() { 30, 20 },
                new List<int>() { 10, 50 },
            };
        }
        else if (loot == LootType.SHOTGUN_AMMO || loot == LootType.SNIPER_AMMO)
        {
            lootTable = new List<List<int>>() {
                new List<int>() { 60, 1 },
                new List<int>() { 30, 5 },
                new List<int>() { 10, 10 },
            };
        }
        else if (loot == LootType.HEALTH_PACK)
        {
            lootTable = new List<List<int>>() {
                new List<int>() { 60, 1 },
                new List<int>() { 30, 2 },
                new List<int>() { 10, 5 },
            };
        }
        else
        {
            lootTable = null;
        }
    }

    void InitLootAmount()
    {
        if (lootTable == null)
        {
            lootAmount = 1;
        }
        else
        {
            lootAmount = RNGLootAmount();
        }
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
