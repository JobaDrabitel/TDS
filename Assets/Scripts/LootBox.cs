using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
	[SerializeField] private GameObject[] items;
	private void OnDestroy()
	{

	}
	public void Break()
	{
		int random = Random.Range(0, items.Length);
		GameObject drop = Instantiate(items[random], transform);
		drop.transform.parent = null;
		Destroy(gameObject);
	}
}
