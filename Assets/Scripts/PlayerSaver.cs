using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaver 
{
    public int points;
    public int currentLevel;
    public float[] position;
  public PlayerSaver(PlayerData playerData)
    {
        points = playerData.TotalPoints;
        currentLevel = playerData.CurrentLevel;
       
    }
}
