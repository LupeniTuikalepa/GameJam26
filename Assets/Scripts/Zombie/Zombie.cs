using UnityEngine;

public partial class ZombieFollow : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 3f;
    private float health;

    public void ZombieGetAttacked(float damages)
    {
        health -= damages;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
