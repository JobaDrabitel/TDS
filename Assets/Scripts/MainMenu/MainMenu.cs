using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private Button startButton;
	[SerializeField] private Button exitButton;
	[SerializeField] private Button optionsButton;
	[SerializeField] private Canvas options;

	public void OnStartButtonClick()
	{
		SceneManager.LoadScene(1);
	}
	public void OnOptionsButtonClick()
	{
		options.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}
	public void OnExitButtonClick()
	{
		Application.Quit();
	}

}
