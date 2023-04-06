using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Text bulletInGun;
    [SerializeField] private Text allBullets;
    [SerializeField] private Text timeSlowPoints;
    [SerializeField] private Text moonShines;
    [SerializeField] private Player player;
    [SerializeField] private Slider timeSlowBar;
    [SerializeField] private Canvas pauseScreen;
    [SerializeField] private Canvas deathScreen;
    [SerializeField] private Button continueButton;
    [SerializeField] private Slider deathScreenSlider;
    [SerializeField] private Text playerScore;
    private Weapon _playerWeapon;


    private void Start()
    {
        pauseScreen.gameObject.SetActive(false);
        SetPlayerWeapon();
        SetTimeSlowPoints(player.TimeSlowPoints);
        SetBullets();
        SetTimeSlowBar();
        SetPlayerScore();
    }
 
    public void SetPlayerWeapon() => _playerWeapon = player.CurrentWeapon.GetComponent<Weapon>();
    public void SetBullets()
    {
        bulletInGun.text = _playerWeapon.Ammo.ToString();
        allBullets.text = player.Bullets.ToString();
    }
    public void SetTimeSlowPoints(int points) => timeSlowPoints.text = points.ToString();
    public void SetTimeSlowBar() => timeSlowBar.value = player.TimeSlowPoints;
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        pauseScreen.gameObject.SetActive(true);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        pauseScreen.gameObject.SetActive(false);
    }
    public void ShowDeathScreen()
    {
        Time.timeScale = 0.0001f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        deathScreen.gameObject.SetActive(true);
        SetDeathScreenSlider();
    }
    public void OnRessurectButtonClick()
    {
        if (player.CanBeResurrected())
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            deathScreen.gameObject.SetActive(false);
            deathScreenSlider.value = 10;
        }
        else
            continueButton.GetComponentInChildren<Text>().text = "Not enough moonshines!!";
    }
    public void SetMoonshines() => moonShines.text = player.PlayerData.Moonshines.ToString();
    public void SetDeathScreenSlider()
    {
        if (deathScreen.gameObject.activeInHierarchy == true)
        {
            StartCoroutine(DecreaseDeathSliderValue());
            ContinueGame();
            
        }
    }
    public IEnumerator DecreaseDeathSliderValue()
    {
        while (deathScreenSlider.value > 0)
        {
            yield return new WaitForSeconds(1f);
            deathScreenSlider.value--;
        }
    }
    public void SetPlayerScore()
    {
        playerScore.text = player.PlayerData.LevelPoints.ToString();
    }
    public void OnExitButtonClick() => SceneManager.LoadScene(0);
}

   

