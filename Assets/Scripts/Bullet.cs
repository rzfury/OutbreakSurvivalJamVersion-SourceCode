using UnityEngine;

public class Bullet : MonoBehaviour
{
    MasterScript master;
    public GameObject hitParticle;
    public int bulletDamage = 1;

    public void SetDamage(int damage) {
        bulletDamage = damage;
    }

    void Start()
    {
        master = GameObject.FindGameObjectWithTag("MasterScript").GetComponent<MasterScript>();
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        master.PlaySound("Hit");
        if(other.gameObject.CompareTag("Zombie")) {
            ZombieStats zombie = other.gameObject.GetComponent<ZombieStats>();
            zombie.TakeDamage(bulletDamage);
            if(!zombie.IsStartled()) {
                zombie.Startle();
            }
        }
        else if(other.gameObject.CompareTag("Crate")) {
            Crate crate = other.gameObject.GetComponent<Crate>();
            crate.Break(2);
        }
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
