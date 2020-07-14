using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    MasterScript master;
    Rigidbody2D rb;

    public Image UIHPBar;
    public GameObject hitEffect;

    public int health = 100;
    public int maxHealth = 100;

    public void TakeDamage(int damage)
    {
        master.PlaySound("Hit");
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        health = Mathf.Max(health - damage, 0);
    }

    public void AddKnockback(Vector2 from, float power)
    {
        Vector2 knockbackForce = (transform.position - (Vector3)from).normalized;
        rb.AddForce(knockbackForce * power, ForceMode2D.Impulse);
    }

    void Start()
    {
        master = GameObject.FindGameObjectWithTag("MasterScript").GetComponent<MasterScript>();
        rb = GetComponent<Rigidbody2D>();

        health = maxHealth;
    }

    void Update()
    {
        UIHPBar.fillAmount = ((float)health) / ((float)maxHealth);

        if (health <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        master.GameOver();
    }
}
