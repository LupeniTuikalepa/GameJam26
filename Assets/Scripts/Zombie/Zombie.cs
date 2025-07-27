using UnityEngine;

public partial class ZombieFollow : MonoBehaviour
{
    private const string isDead = "IsDead";
    [SerializeField]
    private float maxHealth = 3f;
    private float health;

    public void ZombieGetAttacked(float damages)
    {
        health -= damages;
        if (health <= 0)
        {
            animator.SetBool(isDead, true);
        }
    }

    public void DestroyZombie()
    {
        Destroy(gameObject);
    }
}
