using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    public GameSaveManager GameSaveManager;
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
        childObjects[0] = players[0].transform.GetChild(0).gameObject;
        childObjects[1] = players[1].transform.GetChild(0).gameObject;
        childObjects[2] = players[2].transform.GetChild(0).gameObject;
        PlayerTeamState.LoadCharacterProperties(childObjects);
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
