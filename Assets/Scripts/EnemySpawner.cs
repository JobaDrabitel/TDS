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
        foreach (var enemy in enemies)
            enemy.SetActive(true);
       StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            var spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
            var enemy = Instantiate(GetRandomEnemy(), spawnpoint);
            enemy.SetActive(true);
            try
            {
                enemy.GetComponent<GunnerEnemy>().AI.target = player.transform;
            }
            catch { }
            enemy.transform.parent = null;
            enemy.transform.position = spawnpoint.transform.position;
            spawnCount++;
            if (spawnCount > enemies.Length * spawnCount + 1 && spawnDelay > 4f)
                spawnDelay -= 0.5f;  
           
        }
    }
    private GameObject GetRandomEnemy() => enemies[Random.Range(0, enemies.Length)];
}
