using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManagerController
{
    public static void SavePlayer(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "C:/Users/legen/TPS/Saves/Save.fun";
        FileStream fs = new FileStream(path, FileMode.Create);
        PlayerSaver data = new PlayerSaver(playerData);
        formatter.Serialize(fs, data);
        fs.Close();
        Debug.Log("Game saved!");
    }
    public static PlayerSaver LoadPlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();   
        string path = "C:/Users/legen/TPS/Saves/Save.fun";
        Stream fs = new FileStream(path, FileMode.Open);
        if (File.Exists(path))
        {
            Debug.Log("File Loaded!");
            PlayerSaver player = formatter.Deserialize(fs) as PlayerSaver;
            fs.Close();
            return player;   
        }
        else
        {
            Debug.Log("Error");
            fs.Close();
            return null;
        }

    }
}
