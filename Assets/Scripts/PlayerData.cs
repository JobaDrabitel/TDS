using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Level Settings")]
    private static int _totalPoints = 0;
    private static int _levelPoints = 0;
    private int currentLevel = 1;
    private int _moonshines = 0;
    private int _usedMoonshines = 0;
    public int LevelPoints { get => _levelPoints; }
    public int TotalPoints { get => _totalPoints; }
    public int CurrentLevel { get => currentLevel; }
    public int Moonshines { get => _moonshines; }
    public int UsedMoonshines { get => _usedMoonshines; }

    public void SavePlayer()
    {
        SaveManagerController.SavePlayer(this);
        Debug.Log("Game Saved");
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
        return _levelPoints;
    }
    public int SumPoints()
    {
        _totalPoints += _levelPoints;
        return _totalPoints;
    }
    public void AddMoonshines()=>_moonshines++;
    public void DecreaseMoonshines()=>_moonshines-=_usedMoonshines+1;
    public void IncreaseUsedMoonshines()=>_usedMoonshines++;
    public void ResetUsedMoonshines() => _usedMoonshines = 0;
}
