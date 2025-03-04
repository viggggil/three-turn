using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Unity.VisualScripting.FullSerializer;
public class GameSaveManager : MonoBehaviour
{
    public GameData gameData;
    public PlayerTeamState playerTeamState;
    public GameManager GameManager;
    public UIManager UIManager;
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
            gameData.LoadStaminaAndPosition();
            GameManager.LoadEvent();
            GameManager.LoadEnemy();
            GameManager.TurnNumber = gameData.gsd.TurnNumber;
            gameData.UpdateHealth(0,0);
            gameData.UpdateHealth(1, 0);
            gameData.UpdateHealth(2, 0);
            UIManager.dialogueIndex = gameData.gsd.dialogueIndex;
            if (PlayerTeamState.PlayerState.BattleResult == 1) {
                PlayerTeamState.PlayerState.BattleResult = -1;
                GameManager.BattleWin();
            }
            if (PlayerTeamState.PlayerState.BattleResult == 0) {
                PlayerTeamState.PlayerState.BattleResult = -1;
                GameManager.BattleFailed();
            }
            GameManager.LoadPlayer(true);
            file.Close();
        }
    }
    public void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
}
