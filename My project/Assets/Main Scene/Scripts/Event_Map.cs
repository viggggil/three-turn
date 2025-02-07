using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Map : MonoBehaviour
{
    private UIManager UIManager;
    private PlayerTeamState PlayerTeamState;
    [SerializeField] public int type;
    void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        PlayerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        UIManager.DisplayEventInformation(type,this.gameObject);
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
        }

    }

    public void EventTwo()
    {

    }
}
