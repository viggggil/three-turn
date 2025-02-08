using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscipt : MonoBehaviour
{
    private PlayerTeamState PlayerTeamState;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerTeamState.PlayerState.maxHealth[1]);
    }
}
