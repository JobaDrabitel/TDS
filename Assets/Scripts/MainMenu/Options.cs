using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
	[SerializeField] private Canvas mainMenu;
	[SerializeField] private Slider musicVolumeSlider;
	[SerializeField] private Text musicVolumeValue;
	[SerializeField] MusicPlayBack musicPlayBack;
	[SerializeField] private Button applyButton;

	private void Start()
	{
		musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume") * 100;
		musicVolumeValue.text = musicVolumeSlider.value.ToString();
	}
	public void OnMusicVolumeSliderChanged()
	{
		musicPlayBack.SetMusicVolume();
		musicVolumeValue.text = musicVolumeSlider.value.ToString();
		PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value / 100);
	}
	public void OnApplyButtonClick()
	{
		musicPlayBack.SetMusicVolume();
		PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value / 100);
		gameObject.SetActive(false);
		mainMenu.gameObject.SetActive(true);
	}
}
