using UnityEngine;

public class ZombieFollow : MonoBehaviour
{
    [SerializeField, Range(1f, 100f)]
    private float speed = 1f;
    Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if(player == null) return;
        Vector3 direction = player.position - transform.position;
        Vector3 velocity = direction.normalized * speed;
        transform.position  += velocity*Time.deltaTime;
        
        //faire un rayon ou le joueur est detecter 
        //choper les positions du zombie et ajouter un rayon de base pour detecter le joueur 
        
    }
}
