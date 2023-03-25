using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Level Settings")]
    private static int _totalPoints = 0;
    private static int _levelPoints = 0;
    private int currentLevel = 1;
    public int TotalPoints { get => _totalPoints; }
    public int CurrentLevel { get => currentLevel; }

    public void SavePlayer()
    {
        SaveManagerController.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerSaver player = SaveManagerController.LoadPlayer();
        _totalPoints = player.points;
        currentLevel = player.currentLevel;
    }
    public static int AddPoints(int multiplier)
    {
        _levelPoints += 100 * multiplier;
        Bullet.Multiplier = 0;
        return _levelPoints;
    }
    public int SumPoints()
    {
        _totalPoints += _levelPoints;
        return _totalPoints;
    }
}
