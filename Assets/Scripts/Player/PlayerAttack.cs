using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private float strength = 1f;
    [SerializeField]
    private float colliderRange = 1f;
    [SerializeField]
    private float colliderRadius = 1f;
    [SerializeField]
    private LayerMask zombieMask;
    private bool canAttack;
    [SerializeField]
    private float timeBeforeNextAttack = 0.4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
    }


    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed || !canAttack) return;
        canAttack = false;
        Vector2 center = (Vector2)transform.position + playerMovement.FacingDirection * colliderRange;
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, colliderRadius, zombieMask);
        playerMovement.TriggerAttack();
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D currentHit = hits[i];
            if (!currentHit.TryGetComponent(out ZombieFollow zombie)) continue;
            zombie.ZombieGetAttacked(strength);
        }
        StartCoroutine(HandleCanAttack());
    }

    private IEnumerator HandleCanAttack()
    {
        yield return new WaitForSeconds(timeBeforeNextAttack);
        canAttack = true;
    }

    /* void OnDrawGizmos()
    {
        Vector2 center = (Vector2)transform.position + playerMovement.FacingDirection * colliderRange;
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(center, colliderRadius);
    } */
}
