using System;
using UnityEngine;

public partial class ZombieFollow : MonoBehaviour
{

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string isMoving = "IsMoving";
    private Animator animator;

    bool isFollowing = false;

    [SerializeField, Range(1f, 100f)]
    private float speed = 1f;
    Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isFollowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = false;
            animator.SetBool(isMoving, false);
        }
    }


    private void Update()
    {
        animator.SetFloat(horizontal, Mathf.Abs(rb.linearVelocityY) < 0.5 ? rb.linearVelocityX : 0);
        animator.SetFloat(vertical, Mathf.Abs(rb.linearVelocityX) < 0.5 ? rb.linearVelocityY : 0);
        animator.SetBool(isMoving, rb.linearVelocity.sqrMagnitude >= 0.01f);

    }

    private void FixedUpdate()
    {
        if (!isFollowing || player == null) return;
        Vector3 direction = player.position - transform.position;
        Vector3 velocity = direction.normalized * speed;
        rb.linearVelocity = velocity;
    }
}
