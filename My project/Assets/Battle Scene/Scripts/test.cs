using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public PlayerTeamState PlayerTeamState;
    public SceneLoader SceneLoader;
    void Start()
    {
        SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        PlayerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
    }

    public void TestButton()
    {
        PlayerTeamState.PlayerState.BattleResult = 1;
        SceneLoader.LoadMainScene();
    }

    // Update is called once per frame
    
}
