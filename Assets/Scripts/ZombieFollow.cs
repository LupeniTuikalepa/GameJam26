using System;
using UnityEngine;

public class ZombieFollow : MonoBehaviour
{
    
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string isMoving = "IsMoving";
    private GameObject target;
    private Animator animator;
    
    bool isFollowing = false;
    
    
    [SerializeField, Range(1f, 100f)]
    private float speed = 1f;
    Transform player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.tag == "Player")
      {
          target = other.gameObject;
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
            if (!isFollowing || player == null) return;
            Vector3 direction = player.position - transform.position;
            Vector3 velocity = direction.normalized * speed;
            transform.position += velocity * Time.deltaTime;

            animator.SetFloat(horizontal, velocity.y < 0.5 && velocity.y > -0.5 ? velocity.x : 0);
            animator.SetFloat(vertical, velocity.x < 0.5 && velocity.x > -0.5 ? velocity.y : 0);
            animator.SetBool(isMoving, velocity.sqrMagnitude >= 0.01f);
        
    }
}
