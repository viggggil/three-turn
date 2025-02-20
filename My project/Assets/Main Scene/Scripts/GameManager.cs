using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject[] Enemies;
    public GameObject[] Events;
    public int[] EventsNumber;
    public GameObject selected;
    public GameObject[] Players;
    public int selectedID;
    private List<GameObject> moveList;
    public bool toMove=false;
    public GameData GameData;
    public SceneLoader SceneLoader;
    private int serialNumber;
    private GameObject[] tempEvents;
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        moveList = new List<GameObject>();
        serialNumber = 0;
        GenerateEvents();
        ClearFog();
    }
    void Update()
    {
        foreach (var Player in Players)
        {
            if (Player.GetComponent<Carriage>().playerID == 1) Player.GetComponent<Carriage>().LoadPlayer(SceneLoader.PlayerOneProfession);
            else if (Player.GetComponent<Carriage>().playerID == 2) Player.GetComponent<Carriage>().LoadPlayer(SceneLoader.PlayerTwoProfession);
            else continue;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (toMove)
            {
                Vector3 Direction = Players[selectedID - 1].transform.position - new Vector3(0, -1, 0);
                Players[selectedID - 1].GetComponent<Carriage>().Move(Direction);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (toMove)
            {
                Vector3 Direction = Players[selectedID - 1].transform.position - new Vector3(1, 0, 0);
                Players[selectedID - 1].GetComponent<Carriage>().Move(Direction);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (toMove)
            {
                Vector3 Direction = Players[selectedID - 1].transform.position - new Vector3(0, 1, 0);
                Players[selectedID - 1].GetComponent<Carriage>().Move(Direction);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (toMove)
            {
                Vector3 Direction = Players[selectedID - 1].transform.position - new Vector3(-1, 0, 0);
                Players[selectedID - 1].GetComponent<Carriage>().Move(Direction);
            }
        }
    }
    public void ShowMoveRange()
    {
        CloseSelect();
        foreach (var cell in cells)
        {
            if(Mathf.Abs(cell.transform.position.x - Players[selectedID-1] .transform .position .x)
                +Mathf.Abs(cell.transform.position .y- Players[selectedID-1].transform .position.y) <= 1)
            {
                cell.GetComponent<Cell>().moveCell.SetActive(true);
                moveList.Add(cell);
            }
        }

        toMove = true;
    }
    public bool TestDistance(GameObject thisEvent)
    {
        if (Mathf.Abs(Players[selectedID - 1].transform.position.x - thisEvent.transform.position.x)
                + Mathf.Abs(Players[selectedID - 1].transform.position.y - thisEvent.transform.position.y) <= 1) return true;
        return false;
    }

    public bool[] TestDistance_(GameObject thisEnemy)
    {
        bool[] result = new bool[3] { false,false,false};
        if (Mathf.Abs(Players[selectedID - 1].transform.position.x - thisEnemy.transform.position.x)
                + Mathf.Abs(Players[selectedID - 1].transform.position.y - thisEnemy.transform.position.y) >1) return result;
        for (int i = 0; i < 3; i++)
        {
            if (Mathf.Abs(Players[i].transform.position.x - thisEnemy.transform.position.x)
                + Mathf.Abs(Players[i].transform.position.y - thisEnemy.transform.position.y) <= 3) result[i] = true;
        }
        foreach (var cell in cells)
        {
            if (Mathf.Abs(cell.transform.position.x - thisEnemy.transform.position.x)
                + Mathf.Abs(cell.transform.position.y - thisEnemy.transform.position.y) <= 3)
            {
                cell.GetComponent<Cell>().moveCell.SetActive(true);
                moveList.Add(cell);
            }
        }
        return result;
    }

    public bool TestDistance__(GameObject thisEvent)
    {
        foreach (var cell in cells)
        {
            if (Mathf.Abs(cell.transform.position.x - thisEvent.transform.position.x)
                + Mathf.Abs(cell.transform.position.y - thisEvent.transform.position.y) <= 2)
            {
                cell.GetComponent<Cell>().moveCell.SetActive(true);
                moveList.Add(cell);
            }
        }
        foreach (var Enemy in Enemies)
        {
            if(Mathf.Abs(Enemy.transform.position.x - thisEvent.transform.position.x)
                + Mathf.Abs(Enemy.transform.position.y - thisEvent.transform.position.y) <=2) return false;
        }
        return true;
    }

    public void ClearFog()
    {
        foreach (var cell in cells)
        {
            if (Mathf.Abs(cell.transform.position.x - Players[selectedID-1].transform.position.x)
                + Mathf.Abs(cell.transform.position.y - Players[selectedID-1].transform.position.y) <= 4)
            {
                cell.GetComponent<Cell>().fog.SetActive(false);
            }
        }
    }

    public bool SelectedCell(int type)
    {
        if(toMove && moveList.Contains(selected)&& type<4)
        {
            Players[selectedID - 1].GetComponent<Carriage>().Move(selected.transform.position);
            return true;
        }
        else
        {
            CloseSelect();
            moveList.Add(selected);
            toMove = false;
            Players[selectedID - 1].GetComponent<Carriage>().isSelected = false;
            return false;
        }
    }

    public void ChangeSelected(int ID)
    {
        Players[selectedID - 1].GetComponent<Carriage>().isSelected = false;
        Players[ID-1].GetComponent<Carriage>().isSelected = true;
        selectedID = ID;
    }
    public void CloseSelect()
    {
        if(moveList.Count > 0)
        {
            foreach (var cell in moveList)
            {
                cell.GetComponent<Cell>().moveCell.SetActive(false);
            }
        }
        moveList.Clear();
    }

    public void TurnStart()
    {
        foreach (var player in Players)
        {
            Carriage temp = player.GetComponent<Carriage>();
            if (temp.playerID != -1) temp.TurnStart();
        }
    }

    public void BattleFailed()
    {
        foreach(var player in Players)
        {
            Carriage temp = player.GetComponent<Carriage>();
            if (PlayerTeamState.PlayerState.isHere[temp.playerID - 1]) temp.Dead();
        }
    }

    public void GenerateEvents()
    {
        foreach (var cell in cells)
        {
            if (serialNumber >= 10) break;
            if (cell.GetComponent<Cell>().type >= 4||cell.GetComponent<Cell>().TooNear) continue;
            int randomNumber = Random.Range(0, 501);
            if (randomNumber > 10) continue;
            Vector3 position = cell.transform.position - new Vector3(0, 0, 0.1f);
            int type = 0;
            while (EventsNumber[type] < randomNumber) type++;
            GameData.gsd.Epositions[serialNumber]= position;
            GameData.gsd.types[serialNumber] = type;
            GameObject event_ = Instantiate(Events[type], position, Quaternion.identity);
            event_.GetComponent<Event_Map>().GetNearbyTaggedObjects(serialNumber);
            serialNumber++;
        }
    }

    public void LoadEvent()
    {
        serialNumber = 0;
        tempEvents = GameObject.FindGameObjectsWithTag("Event");
        foreach(var event_ in tempEvents)
        {
            Destroy(event_);
        }
        while (serialNumber<10 && GameData.gsd.Epositions[serialNumber].x != 0)
        {
            if (GameData.gsd.types[serialNumber]==-1)
            {
                serialNumber++;
                continue;
            }
            Vector3 position =GameData.gsd.Epositions[serialNumber];
            GameObject event_ = Instantiate(Events[GameData.gsd.types[serialNumber]], position, Quaternion.identity);
            serialNumber++;
        }
    }


}
