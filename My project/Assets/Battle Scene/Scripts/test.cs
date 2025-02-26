using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public PlayerTeamState PlayerTeamState;
    void Start()
    {
        PlayerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
        Debug.Log(PlayerTeamState.PlayerState.characterProperties[0].maxHealth);
    }

    // Update is called once per frame
    
}
