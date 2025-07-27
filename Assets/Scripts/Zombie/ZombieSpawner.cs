using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject SmallZombiePrefab;

    [SerializeField] 
    private float SmallZombieInterval = 3.5f;
    void Start()
    {
        StartCoroutine(spawnZombie(SmallZombieInterval, SmallZombiePrefab));
    }

    private IEnumerator spawnZombie(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f,5), Random.Range(-6f, 6f), 0),Quaternion.identity);
        StartCoroutine(spawnZombie(interval, enemy));
    }
}
