using UnityEngine;

public class ZombieAction : MonoBehaviour
{
    PlayerStats player;
    ZombieStats zombie;

    float hitCooldown = 0;
    float hitCooldownMax = 50f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        zombie = GetComponent<ZombieStats>();
    }

    void Update()
    {
        if(hitCooldown > 0) {
            hitCooldown--;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(hitCooldown == 0) {
                hitCooldown = hitCooldownMax;
                player.TakeDamage(zombie.damageDealt);
            }
        }
    }
}
