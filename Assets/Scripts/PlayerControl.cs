﻿using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Camera cameraObj;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;

    int baseSpeedMultipler = 400;

    public float speed = 1;
    public float aimAngle = 0;
    public string facing = "R";

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Movement();
        Facing();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(x, y) * (speed * baseSpeedMultipler) * Time.deltaTime;
        animator.SetBool("isMoving", (x != 0 || y != 0));
    }

    void Facing()
    {
        Vector2 mouse = cameraObj.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aimAngle = (Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg) + 180f;

        if (aimAngle >= 90 && aimAngle < 270)
        {
            transform.localScale = new Vector3(2, 2, 0);
            facing = "R";
        }
        else
        {
            transform.localScale = new Vector3(-2, 2, 0);
            facing = "L";
        }
    }

    float PolarAngle(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
