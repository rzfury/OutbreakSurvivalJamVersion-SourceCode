using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public Animator knifeAnim;
    public GameObject slashEffect;

    public Vector2 hitboxSize;
    public Vector2 hitboxOffset;

    PlayerControl player;
    Vector2 hitboxFacing = new Vector2(1, 1);
    bool isAttacking = false;

    void Start()
    {
        player = GetComponent<PlayerControl>();
    }

    void Update()
    {
        Facing();
        Attack();
    }

    void Facing()
    {
        if (player.facing == "L")
        {
            hitboxFacing = new Vector2(-1, 1);
        }
        else
        {
            hitboxFacing = new Vector2(1, 1);
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(isAttacking) {
                return;
            }

            Vector3 effOffset = new Vector2(player.facing == "L" ? -1 : 1, 0);
            GameObject eff = Instantiate(slashEffect, transform.position + effOffset, Quaternion.identity);
            eff.transform.localScale = new Vector2(player.facing == "L" ? -2 : 2, 2);
            Destroy(eff, 0.3f);

            knifeAnim.SetBool("isMeleeAttacking", true);
            Invoke("SetNotAttacking", 0.2f);

            isAttacking = true;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)(hitboxOffset * hitboxFacing), hitboxSize, 0f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Zombie"))
                {
                    ZombieStats zombie = collider.GetComponent<ZombieStats>();
                    zombie.TakeDamage(4);
                    zombie.AddKnockback(transform.position, 100);
                }
                else if(collider.gameObject.CompareTag("Crate")) {
                    Crate crate = collider.gameObject.GetComponent<Crate>();
                    crate.Break(4);
                }
            }
        }
    }

    void SetNotAttacking()
    {
        knifeAnim.SetBool("isMeleeAttacking", false);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isAttacking)
        {
            Gizmos.DrawWireCube(transform.position + (Vector3)(hitboxOffset * hitboxFacing), hitboxSize);
        }
    }
}
