using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter
{
    //[SerializeField] private static PlayerData _playerData;
  public static int AddPoints(int multiplier)
    {
        PlayerData.levelPoints += 100 * multiplier;
        Bullet.Multiplier = 0;
        return PlayerData.levelPoints;
    }
    public int SumPoints()
    {
        PlayerData.totalPoints += PlayerData.levelPoints;
        return PlayerData.totalPoints;
    }
}
