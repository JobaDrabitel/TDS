using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Vector3 offset;

	void Update()
	{
		transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
	}
}
