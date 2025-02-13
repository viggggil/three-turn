using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
public class GameSaveManager : MonoBehaviour
{
    public GameData gameData;
    
    public void SaveGame()
    {
        Debug.Log(Application.persistentDataPath);
        if (!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/PlayerTeamState.txt");
        var json = JsonUtility.ToJson(gameData.gsd);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_SaveData/PlayerTeamState.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_SaveData/PlayerTeamState.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file),gameData.gsd);
            gameData.LoadStamina();
            file.Close();
        }
    }
}
