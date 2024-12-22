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
    private List<GameObject> moveList;
    public bool toMove=false;
    public UnityEvent<Vector2> move;
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        moveList = new List<GameObject>();
    }
    void Update()
    {
        
    }

    public void ShowMoveRange()
    {
        foreach (var cell in cells)
        {
            int range = selected.GetComponent<Carriage>().moveRange;
            if(Mathf.Abs(cell.transform.position .x-selected .transform .position .x)
                +Mathf.Abs(cell.transform.position .y-selected.transform .position.y) <= range)
            {
                cell.GetComponent<Cell>().moveCell.SetActive(true);
                moveList.Add(cell);
            }
        }
        toMove = true;
    }

    public bool SelectedCell()
    {
        if(toMove && moveList.Contains(selected))
        {
            move?.Invoke(selected.transform.position);
            CloseSelect();
            return true;
        }
        else
        {
            CloseSelect();
            moveList.Add(selected);
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

 


}
