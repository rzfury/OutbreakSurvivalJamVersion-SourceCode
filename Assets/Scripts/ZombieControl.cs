using UnityEngine;

public class ZombieControl : MonoBehaviour
{
    MasterScript master;
    GameObject player;
    Animator animator;
    Rigidbody2D rb;

    public float speed = 1;
    public int detectionRadius = 1;
    public Vector2 detectionOffset;
    public bool isLockingPlayer = false;

    private int baseSpeedMultipler = 100;

    void Start()
    {
        master = GameObject.FindGameObjectWithTag("MasterScript").GetComponent<MasterScript>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (IsPlayerInRange() && !isLockingPlayer)
        {
            animator.SetBool("isZombieRun", true);
            ChargeToPlayer();
        }

        if (isLockingPlayer)
        {
            MoveTowards();
        }
    }

    void Update() {
        Facing();
    }

    void Facing() {
        if(player.transform.position.x < transform.position.x) {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else {
            transform.localScale = new Vector3(2, 2, 1);
        }
    }

    bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position + (Vector3)detectionOffset);
        return distance < detectionRadius;
    }

    void MoveTowards()
    {
        Vector2 toPlayerNormalized = (player.transform.position - transform.position).normalized;
        rb.AddForce(toPlayerNormalized * speed * baseSpeedMultipler * Time.fixedDeltaTime);
    }

    public void ChargeToPlayer() {
        master.RegisterStartledZombie(gameObject);
        isLockingPlayer = true;
    }

    public void StopChargingToPlayer() {
        master.UnregisterZombie(gameObject);
        isLockingPlayer = false;
    }

    public Rigidbody2D GetRigidbody2D() {
        return rb;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (Vector3)detectionOffset, detectionRadius);
    }

    Vector2 RandomPolar(int rad)
    {
        float randomAngle = Random.Range(0, 360);
        return new Vector2(
            rad * Mathf.Cos(randomAngle),
            rad * Mathf.Sin(randomAngle)
        );
    }
}
