using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public Image uiHealing;
    public GameObject lootObj;
    public Shelter currentShelter;

    public float healingProgress = 0;
    public float healingProgressMax = 100;

    MasterScript master;
    PlayerInventory inventory;
    PlayerShoot shoot;
    PlayerStats stats;
    Vector2 lastPositionBeforeEnterRoom;

    void Start()
    {
        master = GameObject.FindGameObjectWithTag("MasterScript").GetComponent<MasterScript>();
        inventory = GetComponent<PlayerInventory>();
        shoot = GetComponent<PlayerShoot>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (inventory.equippingHealthPack)
        {
            if (inventory.medicPack > 0 && stats.health < stats.maxHealth)
            {
                if (Input.GetMouseButton(0))
                {
                    healingProgress++;
                    if (healingProgress >= healingProgressMax)
                    {
                        master.PlaySound("Heal");
                        healingProgress = 0;
                        inventory.medicPack--;
                        stats.health = Mathf.Min(stats.health + 10, stats.maxHealth);
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    healingProgress = 0;
                }
            }
        }

        uiHealing.fillAmount = healingProgress / healingProgressMax;
    }

    void PickupLoot(Loot loot)
    {
        Destroy(loot.gameObject);
        if (loot.lootType == LootType.PISTOL)
        {
            if (!inventory.hasGun)
            {
                inventory.hasGun = true;
            }
            else
            {
                SwapGun();
            }
            shoot.gunMode = GunMode.PISTOL;
            inventory.ChangeSlot(2);
        }
        else if (loot.lootType == LootType.RIFLE)
        {
            if (!inventory.hasGun)
            {
                inventory.hasGun = true;
            }
            else
            {
                SwapGun();
            }
            shoot.gunMode = GunMode.RIFLE;
            inventory.ChangeSlot(2);
        }
        else if (loot.lootType == LootType.SHOTGUN)
        {
            if (!inventory.hasGun)
            {
                inventory.hasGun = true;
            }
            else
            {
                SwapGun();
            }
            shoot.gunMode = GunMode.SHOTGUN;
            inventory.ChangeSlot(2);
        }
        else if (loot.lootType == LootType.SNIPER)
        {
            if (!inventory.hasGun)
            {
                inventory.hasGun = true;
            }
            else
            {
                SwapGun();
            }
            shoot.gunMode = GunMode.SNIPER;
            inventory.ChangeSlot(2);
        }
        else if (loot.lootType == LootType.GUN_AMMO)
        {
            inventory.gunAmmo += loot.amount;
        }
        else if (loot.lootType == LootType.SHOTGUN_AMMO)
        {
            inventory.shotgunAmmo += loot.amount;
        }
        else if (loot.lootType == LootType.SNIPER_AMMO)
        {
            inventory.sniperAmmo += loot.amount;
        }
        else if (loot.lootType == LootType.HEALTH_PACK)
        {
            inventory.medicPack += loot.amount;
        }
    }

    void SwapGun()
    {
        LootType lootType = LootType.PISTOL;
        switch (shoot.gunMode)
        {
            case GunMode.RIFLE:
                lootType = LootType.RIFLE;
                break;
            case GunMode.SHOTGUN:
                lootType = LootType.SHOTGUN;
                break;
            case GunMode.SNIPER:
                lootType = LootType.SNIPER;
                break;
        }

        GameObject obj = Instantiate(lootObj, transform.position, Quaternion.identity);
        Loot objLoot = obj.GetComponent<Loot>();
        objLoot.InitializeLoot(lootType, 1);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LootItem"))
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(1))
            {
                Loot loot = other.gameObject.GetComponent<Loot>();
                PickupLoot(loot);
            }
        }
        else if (other.gameObject.CompareTag("Shelter"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentShelter = other.GetComponent<Shelter>();

                lastPositionBeforeEnterRoom = transform.position;

                transform.position = new Vector2(200, -7);
            }
        }
        else if (other.gameObject.CompareTag("Room_Exit"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentShelter = null;

                transform.position = lastPositionBeforeEnterRoom;
            }
        }
        else if (other.gameObject.CompareTag("Room_Loot1"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentShelter.SpawnLoot1(transform.position);
            }
        }
        else if (other.gameObject.CompareTag("Room_Loot2"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentShelter.SpawnLoot2(transform.position);
            }
        }
    }
}
