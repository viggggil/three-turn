using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    public GameSaveManager GameSaveManager;
    public GameManager GameManager;
    public static int PlayerOneProfession=0;
    public static int PlayerTwoProfession=0;
    public GameObject[] players;
    public PlayerTeamState PlayerTeamState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void LoadCurrentSave()
    {
        SceneManager.LoadScene("MainScene");
        Invoke("LoadCurrentSave_", 0.5f);
    }

    public void LoadCurrentSave_()
    {
        GameSaveManager = GameObject.Find("GameSaveManager").GetComponent<GameSaveManager>();
        GameSaveManager.LoadGame();
    }
    public void LoadBattleScene()
    {
        GameObject[] childObjects=new GameObject[3];
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerTeamState= GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
        players = GameManager.Players;
        for (int i = 0; i < 3; i++)
        {
            childObjects[i] = players[i].transform.GetChild(1).gameObject;
        }
        PlayerTeamState.LoadCharacterProperties(childObjects);
        GameSaveManager = GameObject.Find("GameSaveManager").GetComponent<GameSaveManager>();
        GameSaveManager.SaveGame();
        SceneManager.LoadScene("BattleScene");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
        GameSaveManager.LoadGame();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
        Invoke("LoadCurrentSave_", 0.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
