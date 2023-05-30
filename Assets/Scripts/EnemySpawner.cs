using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private GameObject[] enemies;
    private float spawnDelay = 1f;
    private int spawnCount = 0;

    private void Start()
    {
        foreach (var enemy in enemies)
            enemy.SetActive(false);
       StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            var enemy = GetRandomEnemy();
            enemy.SetActive(true);
            enemy.GetComponent<GunnerEnemy>().AI.target = player.transform;
            enemy.GetComponent<KniferEnemy>().AI.target = player.transform;
            enemy.transform.parent = null;
            var spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
            enemy.transform.position = spawnpoint.transform.position;
            spawnCount++;
            if (spawnCount > enemies.Length * spawnCount + 1 && spawnDelay > 4f)
                spawnDelay -= 0.5f;  
           
        }
    }
    private GameObject GetRandomEnemy() => enemies[Random.Range(0, enemies.Length)];
}
