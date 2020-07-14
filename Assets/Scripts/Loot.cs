using UnityEngine;

public class Loot : MonoBehaviour
{
    public Sprite pistol;
    public Sprite rifle;
    public Sprite shotgun;
    public Sprite sniper;
    public Sprite medicPack;
    public Sprite gunAmmo;
    public Sprite shotgunAmmo;
    public Sprite sniperAmmo;

    public LootType lootType;
    public int amount;

    SpriteRenderer sprite;

    public void InitializeLoot(LootType loot, int lootAmount) {
        lootType = loot;
        amount = lootAmount;

        switch(loot) {
            case LootType.GUN_AMMO:
                sprite.sprite = gunAmmo;
            break;
            case LootType.SHOTGUN_AMMO:
                sprite.sprite = shotgunAmmo;
            break;
            case LootType.SNIPER_AMMO:
                sprite.sprite = sniperAmmo;
            break;
            case LootType.PISTOL:
                sprite.sprite = pistol;
            break;
            case LootType.RIFLE:
                sprite.sprite = rifle;
            break;
            case LootType.SHOTGUN:
                sprite.sprite = shotgun;
            break;
            case LootType.SNIPER:
                sprite.sprite = sniper;
            break;
            case LootType.HEALTH_PACK:
                sprite.sprite = medicPack;
            break;
        }
    }

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
}
