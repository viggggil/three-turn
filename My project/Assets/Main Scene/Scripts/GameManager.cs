using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject[] Enemies;
    public GameObject[] Events;
    public GameObject selected;
    public GameObject[] Players;
    public int selectedID;
    private List<GameObject> moveList;
    public bool toMove=false;
    public GameData GameData;
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Events = GameObject.FindGameObjectsWithTag("Event");
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        moveList = new List<GameObject>();
        ClearFog();
    }
    void Update()
    {
        
    }

    public void RefreshEvents()
    {
        foreach (var Event in Events)
        {
            Event_Map temp = Event.GetComponent<Event_Map>();
            if (GameData.gsd.Events[temp.SerialNumber])
            {
                Event.SetActive(false);
            }
            else Event.SetActive(true);
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
            CloseSelect();
            return true;
        }
        else
        {
            CloseSelect();
            moveList.Add(selected);
            toMove = false;
            return false;
        }
        
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

 


}
