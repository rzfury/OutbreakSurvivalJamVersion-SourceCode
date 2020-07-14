using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject knife;
    public GameObject gun;

    public Image gunSlotImage;
    public Text ammoCount;
    public Text medicCount;

    public Image uiSlotSelected;
    public Sprite pistolUi;
    public Sprite rifleUi;
    public Sprite shotgunUi;
    public Sprite sniperUi;

    public bool hasGun = false;
    public bool equippingHealthPack = false;

    public int gunAmmo = 0;
    public int shotgunAmmo = 0;
    public int sniperAmmo = 0;
    public int medicPack = 0;

    PlayerMeleeAttack scriptMelee;
    PlayerShoot scriptShoot;

    public int currentSlot = 1;

    void Start()
    {
        scriptMelee = GetComponent<PlayerMeleeAttack>();
        scriptShoot = GetComponent<PlayerShoot>();
    }

    void Update()
    {
        medicCount.text = medicPack.ToString();
        if (hasGun)
        {
            gunSlotImage.color = new Color(1, 1, 1, 1);
            if (scriptShoot.gunMode == GunMode.PISTOL)
            {
                gunSlotImage.sprite = pistolUi;
                ammoCount.text = gunAmmo.ToString();
            }
            else if (scriptShoot.gunMode == GunMode.RIFLE)
            {
                gunSlotImage.sprite = rifleUi;
                ammoCount.text = gunAmmo.ToString();
            }
            else if (scriptShoot.gunMode == GunMode.SHOTGUN)
            {
                gunSlotImage.sprite = shotgunUi;
                ammoCount.text = shotgunAmmo.ToString();
            }
            else if (scriptShoot.gunMode == GunMode.SNIPER)
            {
                gunSlotImage.sprite = sniperUi;
                ammoCount.text = sniperAmmo.ToString();
            }
        }
        else
        {
            gunSlotImage.color = new Color(0, 0, 0, 0);
        }

        Vector2 slotPos = new Vector2((currentSlot - 1) * 64, 0);
        uiSlotSelected.rectTransform.anchoredPosition = slotPos;

        SelectSlot();
    }

    public void ChangeSlot(int slot)
    {
        if (currentSlot != slot)
        {
            EnableSlot(currentSlot, false);
            EnableSlot(slot, true);
            currentSlot = slot;
        }
    }

    void SelectSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // ChangeSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // ChangeSlot(1);
        }
    }

    void EnableSlot(int slot, bool enable)
    {
        switch (slot)
        {
            case 1:
                knife.SetActive(enable);
                scriptMelee.enabled = enable;
                break;
            case 2:
                if (hasGun)
                {
                    gun.SetActive(enable);
                    scriptShoot.enabled = enable;
                }
                break;
            case 3:
                equippingHealthPack = enable;
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }
}