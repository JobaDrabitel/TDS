using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackDaniels : MonoBehaviour
{
	private bool _isMirrored = false;
	public IEnumerator MirrorDelay()
	{
		yield return new WaitForSeconds(1f);

		_isMirrored = false;
	}
	public void MirrorItem()
	{
		if (!_isMirrored)
		{
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
			_isMirrored = true;
			StartCoroutine(MirrorDelay());
		}
	}
	private void Update()
	{
		MirrorItem();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Player player = collision.gameObject.GetComponent<Player>();
		if (player != null)
		{
			player.IncreaseTimeSlowPoints(100);
			Destroy(gameObject);
		}
	}
}
