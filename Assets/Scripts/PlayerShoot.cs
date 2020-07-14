using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public GunMode gunMode = GunMode.PISTOL;
    public GameObject gunObj;
    public GameObject gunBullet;
    public GameObject gunFireEffect;
    public Transform gunFireEffectPos;
    public GameObject laserPointer;
    public Sprite pistolSprite;
    public Sprite rifleSprite;
    public Sprite shotgunSprite;
    public Sprite sniperSprite;
    public Image uiCooldown;

    public Vector3 effectOffset;
    public int shotPower = 1;

    MasterScript master;
    PlayerInventory inventory;
    PlayerControl player;
    SpriteRenderer gunSprite;
    Vector3 GFEFFpos;

    string facing = "L";

    private int cooldown = 0;
    private int cooldownMax = 0;
    private bool sniperHold = false;
    private int initSpeedPlayer = 0;

    void Start()
    {
        master = GameObject.FindGameObjectWithTag("MasterScript").GetComponent<MasterScript>();
        inventory = GetComponent<PlayerInventory>();
        player = GetComponent<PlayerControl>();
        gunSprite = gunObj.GetComponent<SpriteRenderer>();

        GFEFFpos = gunFireEffectPos.position;
        initSpeedPlayer = (int)player.speed;
    }

    void FixedUpdate()
    {
        Facing();
        GunPosition();
        GunRotation();

        uiCooldown.fillAmount = (float)cooldown / (float)cooldownMax;
        if(sniperHold) {
            player.speed = (float)initSpeedPlayer / 2;
        } else {
            player.speed = (float)initSpeedPlayer;
        }
    }

    void Update()
    {
        GunType();
        GunShoot();
        CooldownControl();

        laserPointer.SetActive(sniperHold);
    }

    void CooldownControl()
    {
        cooldown = Mathf.Max(cooldown - 1, 0);
    }

    void GunType()
    {
        if (gunMode == GunMode.PISTOL)
        {
            gunSprite.sprite = pistolSprite;
            cooldownMax = 15;
        }
        else if (gunMode == GunMode.RIFLE)
        {
            gunSprite.sprite = rifleSprite;
            cooldownMax = 8;
        }
        else if (gunMode == GunMode.SHOTGUN)
        {
            gunSprite.sprite = shotgunSprite;
            cooldownMax = 60;
        }
        else if (gunMode == GunMode.SNIPER)
        {
            gunSprite.sprite = sniperSprite;
            cooldownMax = 120;
        }
    }

    void Facing()
    {
        bool isInRange = (player.aimAngle >= 90 && player.aimAngle < 270);
        facing = isInRange ? "L" : "R";
    }

    void GunRotation()
    {
        gunObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, player.aimAngle - 180f));

        bool facingLeft = facing != "L";
        gunSprite.transform.localScale = (facingLeft
            ? new Vector3(-0.5f, -0.5f, 0)
            : new Vector3(0.5f, 0.5f, 0)
        );
    }

    void GunPosition()
    {
        Vector3 playerPos = transform.position;
        float radius = 1f;

        Vector3 newGunPos = new Vector3(
            radius * Mathf.Cos((180f + player.aimAngle) * Mathf.Deg2Rad),
            radius * Mathf.Sin((180f + player.aimAngle) * Mathf.Deg2Rad),
            0
        );
        gunObj.transform.position = newGunPos + playerPos;
    }

    void GunShoot()
    {
        if (cooldown > 0)
        {
            return;
        }

        if (gunMode == GunMode.PISTOL || gunMode == GunMode.RIFLE)
        {
            if (Input.GetMouseButton(0))
            {
                if (inventory.gunAmmo > 0)
                {
                    inventory.gunAmmo--;
                    cooldown = cooldownMax;
                    SpawnBullet(gunFireEffectPos.position);
                }
            }
        }
        else if (gunMode == GunMode.SHOTGUN)
        {
            if (Input.GetMouseButton(0))
            {
                if (inventory.shotgunAmmo > 0)
                {
                    inventory.shotgunAmmo--;
                    cooldown = cooldownMax;
                    SpawnShotgunBullet(gunFireEffectPos.position);
                }
            }
        }
        else if (gunMode == GunMode.SNIPER)
        {

            if (inventory.sniperAmmo > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    sniperHold = true;
                }

                if (Input.GetMouseButtonUp(0) && sniperHold)
                {
                    sniperHold = false;
                    inventory.sniperAmmo--;
                    cooldown = cooldownMax;
                    SpawnSniperBullet(gunFireEffectPos.position);
                }
            }
        }
    }

    void SpawnBullet(Vector3 pos)
    {
        master.PlaySound("Shoot");
        GameObject gunFire = Instantiate(gunBullet, pos, Quaternion.Euler(new Vector3(0, 0, player.aimAngle - 180f)));
        Rigidbody2D rb2d = gunFire.GetComponent<Rigidbody2D>();
        rb2d.velocity = (gunFireEffectPos.right) * (float)shotPower;

        Bullet bullet = gunFire.GetComponent<Bullet>();
        switch (gunMode)
        {
            case GunMode.PISTOL:
                bullet.SetDamage(2);
                break;
            case GunMode.RIFLE:
                bullet.SetDamage(3);
                break;
        }
    }

    void SpawnShotgunBullet(Vector3 pos)
    {
        master.PlaySound("Shoot");
        Quaternion initRot = gunFireEffectPos.rotation;
        for (int i = 0; i < 5; i++)
        {
            int randomAngle = Random.Range(-5, 5);
            float randomPower = Random.Range(1.00f, 1.20f);

            GameObject gunFire = Instantiate(gunBullet, pos, Quaternion.Euler(new Vector3(0, 0, player.aimAngle - 180f)));
            Rigidbody2D rb2d = gunFire.GetComponent<Rigidbody2D>();

            gunFireEffectPos.rotation = gunFireEffectPos.rotation * Quaternion.Euler(new Vector3(0, 0, randomAngle));
            Vector2 velocity = (gunFireEffectPos.right) * ((float)shotPower * randomPower);
            rb2d.velocity = velocity;
            gunFireEffectPos.rotation = initRot;

            Bullet bullet = gunFire.GetComponent<Bullet>();
            bullet.SetDamage(3);
        }
    }

    void SpawnSniperBullet(Vector3 pos)
    {
        master.PlaySound("Shoot");
        GameObject gunFire = Instantiate(gunBullet, pos, Quaternion.Euler(new Vector3(0, 0, player.aimAngle - 180f)));
        BoxCollider2D collider = gunFire.GetComponent<BoxCollider2D>();
        Rigidbody2D rb2d = gunFire.GetComponent<Rigidbody2D>();
        rb2d.velocity = (gunFireEffectPos.right) * (float)shotPower;

        Bullet bullet = gunFire.GetComponent<Bullet>();
        bullet.SetDamage(15);
    }

    void SpawnGunFireEffect(Vector3 pos)
    {
        GameObject gunFire = Instantiate(gunFireEffect, pos, Quaternion.Euler(new Vector3(0, 0, player.aimAngle - 180f)));
        gunFire.transform.SetParent(gunFireEffectPos);
        Destroy(gunFire, 0.1f);
    }
}
