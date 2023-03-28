using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Weapon _playerWeapon;


    private void Start()
    {
        pauseScreen.gameObject.SetActive(false);
        SetPlayerWeapon();
        SetTimeSlowPoints(player.TimeSlowPoints);
        SetBullets();
        SetTimeSlowBar();
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
            player.CanBeResurrected();
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            deathScreen.gameObject.SetActive(false);
        }
        else
            continueButton.GetComponentInChildren<Text>().text = "Not enough moonshines!!";
    }
    public void SetMoonshines() => moonShines.text = player.PlayerData.Moonshines.ToString();
    public void SetDeathScreenSlider()
    {
        if (deathScreen.gameObject.activeInHierarchy == true)
        {
            while (deathScreenSlider.value>0)
            {
                StartCoroutine(DecreaseDeathSliderValue());
                if (deathScreenSlider.value <= 0)
                    ContinueGame();
            }
        }
    }
    public IEnumerator DecreaseDeathSliderValue()
    {
        yield return new WaitForSeconds(1f);
        deathScreenSlider.value--;
    }
}

   

