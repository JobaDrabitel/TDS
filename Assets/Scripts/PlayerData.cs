using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject _bulletType;
    public int _bulletsInGun = 6;
    public PlayerUI bulletsUI;

    [Header("Health Settings")]
    public PlayerUI healthBar;
    

    [Header("Atack Settings")]
   

    [Header("Move Settings")]


    [Header("Level Settings")]
    public static int totalPoints = 0;
    public static int levelPoints = 0;
    public int currentLevel = 1;

    public void SavePlayer()
    {
        SaveManagerController.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerSaver player = SaveManagerController.LoadPlayer();
        totalPoints = player.points;
        currentLevel = player.currentLevel;
        //position.x = player.position[0];
        //position.y = player.position[1];
    }
}
