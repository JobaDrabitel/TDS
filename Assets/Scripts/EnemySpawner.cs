using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private GameObject[] enemies;
    private float spawnDelay = 10f;
    private int spawnCount = 0;

    private void Start()
    {
       StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnpoints[Random.Range(0, spawnpoints.Length)]);
            enemy.GetComponent<GunnerEnemy>().AI.target = player.transform;
            enemy.GetComponent<KniferEnemy>().AI.target = player.transform;
            enemy.transform.parent = null;
            spawnCount++;
            if (spawnCount > enemies.Length * spawnCount + 1 && spawnDelay > 4f)
                spawnDelay -= 0.5f;
           
           
        }
    }
}
