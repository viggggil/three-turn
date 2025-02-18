using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Map : MonoBehaviour
{
    private UIManager UIManager;
    private PlayerTeamState PlayerTeamState;
    private GameManager GameManager;
    private GameData GameData;
    [SerializeField] public int SerialNumber;
    [SerializeField] public int type;
    void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        PlayerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (this.enabled)
        {
            bool flag = GameManager.TestDistance(this.gameObject);
            UIManager.DisplayEventInformation(type, this.gameObject, flag);
        }
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
            case 5:
                {
                    if (PlayerTeamState.PlayerState.equips[GameManager.selectedID - 1, 1])
                    {
                        GetComponent<Carriage>().Reborn();
                        PlayerTeamState.PlayerState.equips[GameManager.selectedID - 1, 1] = false;
                        UIManager.CloseInfo();
                        break;
                    }
                    else break;
                }
            case 7:
                {
                    if (GameManager.TestDistance__(this.gameObject))
                    {
                        PlayerTeamState.PlayerState.equips[GameManager.selectedID - 1, 1] = true;
                        GameManager.CloseSelect();
                        UIManager.CloseInfo();
                        GameData.gsd.Events[SerialNumber] = true;
                        this.gameObject.SetActive(false);
                        break;
                    }
                    else
                    {
                        UIManager.EnemyDescribe.text = "你听到附近怪物的嚎叫，这使你无法安心祈祷";
                        break;
                    }
                }
        }

    }

    public void EventTwo()
    {

    }

    public void GetNearbyTaggedObjects()
    {
        List<GameObject> results = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position,
            1f
        );
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Cell"))
            {
                gameObject.GetComponent<Cell>().TooNear = true;
            }
        }
    }
}
