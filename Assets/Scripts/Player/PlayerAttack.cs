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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 center = (Vector2)transform.position + playerMovement.FacingDirection * colliderRange;
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, colliderRadius, zombieMask);
        playerMovement.TriggerAttack();
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D currentHit = hits[i];
            if (!currentHit.TryGetComponent(out ZombieFollow zombie)) continue;
            zombie.GetAttacked(strength);
        }
    }

    void OnDrawGizmos()
    {
        Vector2 center = (Vector2)transform.position + playerMovement.FacingDirection * colliderRange;
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(center, colliderRadius);
    }
}
