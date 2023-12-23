using System;
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
		StartCoroutine(SpawnEnemy());
	}
	private IEnumerator SpawnEnemy()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnDelay);
			var spawnpoint = spawnpoints[UnityEngine.Random.Range(0, spawnpoints.Length)];
			var enemy = Instantiate(GetRandomEnemy(), spawnpoint);
			enemy.SetActive(true);
			try
			{
				enemy.GetComponent<GunnerEnemy>().AI.target = player.transform;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				enemy.GetComponent<KniferEnemy>().AI.target = player.transform;
			}
			enemy.transform.parent = null;
			enemy.transform.position = spawnpoint.transform.position;
			spawnCount++;
			if (spawnCount > enemies.Length * spawnCount + 1 && spawnDelay > 4f)
				spawnDelay -= 0.5f;

		}
	}
	private GameObject GetRandomEnemy() => enemies[UnityEngine.Random.Range(0, enemies.Length)];
}
