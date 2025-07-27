using System;
using System.Collections;
using UnityEngine;

public partial class ZombieFollow : MonoBehaviour
{

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string isMoving = "IsMoving";
    private const string triggerAttack = "Attack";
    private Animator animator;

    bool isFollowing = false;

    [SerializeField, Range(1f, 100f)]
    private float speed = 1f;
    Transform player;
    private Rigidbody2D rb;

    private bool canMove;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        canMove = true;
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

    public void TriggerAttack()
    {
        animator.SetTrigger(triggerAttack);
    }


    private void Update()
    {
        animator.SetFloat(horizontal, Mathf.Abs(rb.linearVelocityY) < 0.5 ? rb.linearVelocityX : 0);
        animator.SetFloat(vertical, Mathf.Abs(rb.linearVelocityX) < 0.5 ? rb.linearVelocityY : 0);
        animator.SetBool(isMoving, rb.linearVelocity.sqrMagnitude >= 0.01f);

    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        if (!isFollowing || player == null || !canMove) return;
        Vector3 direction = player.position - transform.position;
        Vector3 velocity = direction.normalized * speed;
        rb.linearVelocity = velocity;
    }

    public void Push(Vector2 force)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(LockZombie(0.5f));
    }

    private IEnumerator LockZombie(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
