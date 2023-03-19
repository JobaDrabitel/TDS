using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Text bulletInGun;
    [SerializeField] private Text allBullets;
    [SerializeField] private Text healthBar;
    [SerializeField] private Player player;
    [SerializeField] private Slider timeSlowBar;
    private Weapon playerWeapon;


    private void Start()
    {
        SetPlayerWeapon();
        SetHealth(player.Health);
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
    public void SetHealth(int health)
    {
        healthBar.text = health.ToString();
    }
    public void SetTimeSlowBar()
    {
        timeSlowBar.value = player.TimeSlowPoints;
    }
}

   

