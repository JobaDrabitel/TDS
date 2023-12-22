using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
	private string[] cheatCode;
	private int index;
	void Start()
	{
		cheatCode = new string[] { "t", "g", "m" };
		index = 0;
	}

	void Update()
	{
		if (Input.anyKeyDown)
		{
			if (Input.GetKeyDown(cheatCode[index]))
			{
				index++;
			}
			else
			{
				index = 0;
			}
		}

		if (index == cheatCode.Length)
		{
			gameObject.GetComponent<Player>().ChangeInvulnerability();
			Debug.Log("TGM!");
			index = 0;
		}
	}
}
