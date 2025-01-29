using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject selected;
    public GameObject selectedPlayer;
    private List<GameObject> moveList;
    public bool toMove=false;
    public UnityEvent turnStart;
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        moveList = new List<GameObject>();
        ClearFog();
    }
    void Update()
    {
        
    }

    public void ShowMoveRange()
    {
        CloseSelect();
        foreach (var cell in cells)
        {
            if(Mathf.Abs(cell.transform.position .x-selectedPlayer .transform .position .x)
                +Mathf.Abs(cell.transform.position .y-selectedPlayer.transform .position.y) <= 1)
            {
                cell.GetComponent<Cell>().moveCell.SetActive(true);
                moveList.Add(cell);
            }
        }
        toMove = true;
    }

    

    public void ClearFog()
    {
        foreach (var cell in cells)
        {
            if (Mathf.Abs(cell.transform.position.x - selectedPlayer.transform.position.x)
                + Mathf.Abs(cell.transform.position.y - selectedPlayer.transform.position.y) <= 4)
            {
                cell.GetComponent<Cell>().fog.SetActive(false);
            }
        }
    }

    public bool SelectedCell(int type)
    {
        if(toMove && moveList.Contains(selected)&& type<4)
        {
            selectedPlayer.GetComponent<Carriage>().Move(selected.transform.position);
            ClearFog();
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
        turnStart?.Invoke();
    }


 


}
