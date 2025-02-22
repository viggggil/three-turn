using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEditor.PackageManager;
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
    public int[] EnemyNumber;
    public GameObject selected;
    public GameObject[] Players;
    public int selectedID=2;
    private List<GameObject> moveList;
    public bool toMove=false;
    public GameData GameData;
    public SceneLoader SceneLoader;
    private int serialNumber;
    private int serialNumber2;
    private GameObject[] tempEvents;

    public int TurnNumber = 1;
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        moveList = new List<GameObject>();
        serialNumber = 0;
        GenerateEvents();
        GenerateEnemies();
        LoadPlayer();
        ClearFog();
        selectedID = 1;
        ClearFog();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (toMove)
            {
                Vector3 Direction = Players[selectedID - 1].transform.position - new Vector3(0, -1, 0);
                foreach (var cell in cells)
                {
                    if (Mathf.Abs(cell.transform.position.x - Direction.x)
                        + Mathf.Abs(cell.transform.position.y - Direction.y)<=0.1)
                    {
                        cell.GetComponent<Cell>().OnMouseDown();
                        break;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (toMove)
            {
                Vector3 Direction = Players[selectedID - 1].transform.position - new Vector3(1, 0, 0);
                foreach (var cell in cells)
                {
                    if (Mathf.Abs(cell.transform.position.x - Direction.x)
                        + Mathf.Abs(cell.transform.position.y - Direction.y) <= 0.1)
                    {
                        cell.GetComponent<Cell>().OnMouseDown();
                        break;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (toMove)
            {
                Vector3 Direction = Players[selectedID - 1].transform.position - new Vector3(0, 1, 0);
                foreach (var cell in cells)
                {
                    if (Mathf.Abs(cell.transform.position.x - Direction.x)
                        + Mathf.Abs(cell.transform.position.y - Direction.y) <= 0.1)
                    {
                        cell.GetComponent<Cell>().OnMouseDown();
                        break;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (toMove)
            {
                Vector3 Direction = Players[selectedID - 1].transform.position - new Vector3(-1, 0, 0);
                foreach (var cell in cells)
                {
                    if (Mathf.Abs(cell.transform.position.x - Direction.x)
                        + Mathf.Abs(cell.transform.position.y - Direction.y) <= 0.1)
                    {
                        cell.GetComponent<Cell>().OnMouseDown();
                        break;
                    }
                }
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
                cell.GetComponent<Cell>().isFog = false;
                if (Mathf.Abs(cell.transform.position.x - Players[selectedID - 1].transform.position.x)
                + Mathf.Abs(cell.transform.position.y - Players[selectedID - 1].transform.position.y) <= 0.1)
                {
                    cell.GetComponent<Cell>().TooNear = true;
                }
            }
            
        }
    }
    public bool SelectedCell(int type)
    {
        if(toMove && moveList.Contains(selected)&& type<5)
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
        TurnNumber++;
        GameData.gsd.TurnNumber = TurnNumber;
        if (TurnNumber % 3 == 0)
        {
            ReTig();
            GenerateEvents();
            GenerateEnemies();
        }
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
        tempEvents = GameObject.FindGameObjectsWithTag("Event");
        serialNumber = 0;
        foreach (var event_ in tempEvents)
        {
            Destroy(event_);
        }
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
            event_.GetComponent<Event_Map>().SerialNumber = serialNumber;
            foreach(var cell_ in cells)
            {
                if (Mathf.Abs(cell_.transform.position.x - position.x)
                + Mathf.Abs(cell_.transform.position.y - position.y) <= 1)
                {
                    cell_.GetComponent<Cell>().TooNear=true;
                }
            }
            serialNumber++;
        }
    }
    public void GenerateEnemies()
    {
        GameObject[] tempEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        serialNumber2 = 0;
        foreach (var enemy in tempEnemies)
        {
            Destroy(enemy);
        }
        foreach (var cell in cells)
        {
            if (serialNumber2 >= 10) break;
            if (cell.GetComponent<Cell>().type >= 4 || cell.GetComponent<Cell>().TooNear) continue;
            int randomNumber = Random.Range(0, 501);
            if (randomNumber > 10) continue;
            Vector3 position = cell.transform.position - new Vector3(0, 0, 0.1f);
            int type = 0;
            while (EnemyNumber[type] < randomNumber) type++;
            GameData.gsd.Epositions2[serialNumber2] = position;
            GameData.gsd.types2[serialNumber2] = type;
            GameObject enemy = Instantiate(Enemies[type], position, Quaternion.identity);
            enemy.GetComponent<Enemy_Map>().SerialNumber = serialNumber2;
            foreach (var cell_ in cells)
            {
                if (Mathf.Abs(cell_.transform.position.x - position.x)
                + Mathf.Abs(cell_.transform.position.y - position.y) <= 1)
                {
                    cell_.GetComponent<Cell>().TooNear = true;
                }
            }
            serialNumber2++;
        }
    }
    public void LoadEvent()
    {
        ReTig();
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

    public void ReTig()
    {
        foreach(var cell in cells)
        {
            cell.GetComponent<Cell>().TooNear = false;
        }
    }
    public void LoadEnemy()
    {
        serialNumber2 = 0;
        GameObject[] tempEnemies= GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in tempEnemies)
        {
            Destroy(enemy);
        }
        while (serialNumber2 < 10 && GameData.gsd.Epositions2[serialNumber2].x != 0)
        {
            if (GameData.gsd.types2[serialNumber2] == -1)
            {
                serialNumber2++;
                continue;
            }
            Vector3 position = GameData.gsd.Epositions2[serialNumber2];
            GameObject event_ = Instantiate(Enemies[GameData.gsd.types2[serialNumber2]], position, Quaternion.identity);
            serialNumber2++;
        }
    }
    public void LoadPlayer()
    {
        foreach (var Player in Players)
        {
            if (Player.GetComponent<Carriage>().playerID == 1) Player.GetComponent<Carriage>().LoadPlayer(GameData.gsd.Professions[0]);
            else if (Player.GetComponent<Carriage>().playerID == 2) Player.GetComponent<Carriage>().LoadPlayer(GameData.gsd.Professions[1]);
            else Player.GetComponent<Carriage>().LoadPlayer(GameData.gsd.Professions[2]);
        }
    }

}
