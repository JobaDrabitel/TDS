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
        points = PlayerData.totalPoints;
        currentLevel = playerData.currentLevel;
        //position[0] = playerData.position.x;
        //position[1] = playerData.position.y;
    }
}
