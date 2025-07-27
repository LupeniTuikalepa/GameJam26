using UnityEngine;

public partial class ZombieFollow : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 3f;
    private float health;

    public void GetAttacked(float damages)
    {
        health -= damages;
        Debug.Log(health);
        if (health <= 0)
        {
            Debug.Log("test");
            Destroy(gameObject);
        }
    }

}
