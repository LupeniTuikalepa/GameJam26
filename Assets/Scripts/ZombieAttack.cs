using System.Collections;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField]
    private ZombieFollow zombieFollow;
    [SerializeField]
    private float strength = 1f;
    [SerializeField]
    private float colliderRadius = 0.5f;
    [SerializeField]
    private LayerMask playerMask;
    private bool canAttack;
    [SerializeField]
    private float timeBeforeNextAttack = 1f;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
    }

    public void Attack()
    {
        canAttack = false;
        Vector2 center = (Vector2)transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, colliderRadius, playerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D currentHit = hits[i];
            Debug.Log(currentHit.name);
            if (!currentHit.TryGetComponent(out PlayerMovement player)) continue;
            zombieFollow.TriggerAttack();
            player.PlayerGetAttacked(strength);
        }
        StartCoroutine(HandleCanAttack());
    }

    private IEnumerator HandleCanAttack()
    {
        yield return new WaitForSeconds(timeBeforeNextAttack);
        canAttack = true;
    }

    void FixedUpdate()
    {
        if (canAttack) Attack();
    }

    void OnDrawGizmos()
    {
        Vector2 center = (Vector2)transform.position;
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(center, colliderRadius);
    }
}
