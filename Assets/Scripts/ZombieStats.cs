using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    MasterScript master;
    ZombieControl zombie;
    Rigidbody2D rb;

    public GameObject zombieDeadBody;
    public GameObject hitParticle;
    public int health = 10;
    public int maxHealth = 10;
    public int damageDealt = 10;

    public void Startle() {
        zombie.ChargeToPlayer();
    }

    public void Unstartle() {
        zombie.StopChargingToPlayer();
    }

    public bool IsStartled() {
        return zombie.isLockingPlayer;
    }

    public void TakeDamage(int damage)
    {
        master.PlaySound("Hit");
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        health = Mathf.Max(health - damage, 0);
    }

    public void AddKnockback(Vector2 from, float power) {
        Vector2 knockbackForce = (transform.position - (Vector3)from).normalized;
        rb.AddForce(knockbackForce * power, ForceMode2D.Impulse);
    }

    void Start() {
        master = GameObject.FindGameObjectWithTag("MasterScript").GetComponent<MasterScript>();
        zombie = GetComponent<ZombieControl>();
        rb = GetComponent<Rigidbody2D>();

        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            master.zombieKilled++;
            Kill();
        }
    }

    void Kill()
    {
        GameObject deadbody = Instantiate(zombieDeadBody, transform.position, Quaternion.identity);
        deadbody.transform.localScale = transform.localScale;

        master.UnregisterZombie(gameObject);
        Destroy(deadbody, 1.9f);
        Destroy(gameObject);
    }
}
