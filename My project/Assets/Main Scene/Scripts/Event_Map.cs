using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Map : MonoBehaviour
{
    private UIManager UIManager;
    private PlayerTeamState PlayerTeamState;
    private GameManager GameManager;
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
        bool flag = GameManager.TestDistance(this.gameObject);
        UIManager.DisplayEventInformation(type,this.gameObject,flag);
    }

    public void EventOne()
    {
        switch (type) 
        {
            case 0:
                {
                    PlayerTeamState.UpdateHealth(10000);
                    break;
                }
            case 1:
                {
                    break;
                }
        }

    }

    public void EventTwo()
    {

    }
}
