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
                    GameData.UpdateStamina(GameManager.selectedID - 1, 1000);
                    GameManager.Players[GameManager.selectedID - 1].GetComponent<Carriage>().curStamina
                        = GameManager.Players[GameManager.selectedID - 1].GetComponent<Carriage>().maxStamina;
                    GameManager.CloseSelect();
                    UIManager.CloseInfo();
                    GameData.gsd.types[SerialNumber] = -1;
                    Destroy(gameObject);
                    break;
                }
            case 1:
                {
                    break;
                }
            case 2:
                {
                    break;
                }
            case 3:
                {
                    break;
                }
            case 4:
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
                        UIManager.DisplayEquip();
                        GameManager.CloseSelect();
                        UIManager.CloseInfo();
                        GameData.gsd.types[SerialNumber] =-1;
                        Destroy(gameObject);
                        break;
                    }
                    else
                    {
                        UIManager.EnemyDescribe.text = "你听到附近怪物的嚎叫，这使你无法安心祈祷";
                        break;
                    }
                }
            case 9:
                {
                    GameManager.CloseSelect();
                    UIManager.CloseInfo();
                    Destroy(gameObject);
                    break;
                }
            case 11:
                {
                    SceneLoader sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
                    sceneLoader.LoadBattleScene();
                    break;
                }
            case 13:
                {
                    GameManager.BranchMission();
                    UIManager.EnemyDescribe.text = "鹈鹕镇的东北方向有一个传播邪教的牧师\n希望你去消灭他";
                    break;
                }
        }

    }

    public void EventTwo()
    {
        switch (type) 
        {
            case 0:
                {
                    GameData.UpdateHealth(GameManager.selectedID - 1, 10000);
                    GameData.UpdateStamina(GameManager.selectedID - 1, -1000);
                    GameManager.Players[GameManager.selectedID - 1].GetComponent<Carriage>().curStamina=0;
                    GameManager.CloseSelect();
                    UIManager.CloseInfo();
                    GameData.gsd.types[SerialNumber] = -1;
                    Destroy(gameObject);
                    break;
                }
        }

            
    }

    public void GetNearbyTaggedObjects(int serialNumber)
    {
        SerialNumber = serialNumber;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Cell"))
            {
                hitCollider.gameObject.GetComponent<Cell>().TooNear = true;
            }
        }
    }

}
