using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    public GameSaveManager GameSaveManager;
    public static int PlayerOneProfession;
    public static int PlayerTwoProfession;

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
        GameSaveManager = GameObject.Find("GameSaveManager").GetComponent<GameSaveManager>();
        GameSaveManager.LoadGame();
    }
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
