using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Text bulletInGun;
    [SerializeField] private Text allBullets;
    [SerializeField] private Text timeSlowPoints;
    [SerializeField] private Player player;
    [SerializeField] private Slider timeSlowBar;
    [SerializeField] private Canvas pauseCanvas;
    private Weapon playerWeapon;


    private void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        SetPlayerWeapon();
        SetTimeSlowPoints(player.TimeSlowPoints);
        SetBullets();
        SetTimeSlowBar();
    }
 
    public void SetPlayerWeapon()
    {
        playerWeapon = player.CurrentWeapon.GetComponent<Weapon>();
    }
    public void SetBullets()
    {
        bulletInGun.text = playerWeapon.Ammo.ToString();
        allBullets.text = player.Bullets.ToString();
    }
    public void SetTimeSlowPoints(int points)
    {
        timeSlowPoints.text = points.ToString();
    }
    public void SetTimeSlowBar()
    {
        timeSlowBar.value = player.TimeSlowPoints;
    }
    public void OnPauseButtonClick() => PauseGame();
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        pauseCanvas.gameObject.SetActive(true);
    }
    public void ReturnGame()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        pauseCanvas.gameObject.SetActive(false);
    }
    public void OnContinueButtonClick() => ReturnGame();
}

   

