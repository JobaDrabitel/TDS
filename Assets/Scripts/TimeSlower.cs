using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlower : MonoBehaviour
{
	[SerializeField] private Player player;
	private bool _isTimeSlowed = false;
	private float _basefixedDeltaTime;
	private void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			Time.timeScale = 0.1f;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			_isTimeSlowed = true;
		}
	}
	public void Start()
	{
		_basefixedDeltaTime = Time.fixedDeltaTime;
	}
	public void TimeSlow()
	{
		if (player.TimeSlowPoints > 0)
		{
			Time.timeScale = 0.1f;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			_isTimeSlowed = true;
		}
	}
	public void NormalTime()
	{
		Time.timeScale = 1f;
		Time.fixedDeltaTime = _basefixedDeltaTime;
		_isTimeSlowed = false;
	}
	public IEnumerator TimeSlowPointsDecrease()
	{
		if (_isTimeSlowed)
		{
			while (player.TimeSlowPoints > 0)
			{
				player.DecreaseTimeSlowPoints();
				yield return new WaitForSeconds(0.01f);
				Debug.Log(player.TimeSlowPoints);
				yield return new WaitUntil(() => _isTimeSlowed || player.TimeSlowPoints == 0);
				if (player.TimeSlowPoints == 0)
					NormalTime();
			}
		}
		else
			yield return null;
	}
	public void OnTimeSlowButtonClick()
	{
		if (!_isTimeSlowed)
		{
			TimeSlow();
			StartCoroutine(TimeSlowPointsDecrease());
		}
		else
			NormalTime();
	}

}
