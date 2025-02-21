using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Map : MonoBehaviour
{
    // Start is called before the first frame update
    private UIManager UIManager;
    private GameManager GameManager;
    private PlayerTeamState PlayerTeamState;
    public int SerialNumber;
    [SerializeField] public int type;
    void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        bool[] arr = GameManager.TestDistance_(this.gameObject);
        bool flag = true;
        if (arr[0] == false && arr[1] == false && arr[2] == false) flag = false;
        else
        {
            PlayerTeamState.PlayerState.isHere = arr;
        }
        UIManager.DisplayEnemyInformation(type,flag);
    }
}
