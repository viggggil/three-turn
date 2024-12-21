using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject selected;
    private List<GameObject> moveList;
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
    }

    public void SelectedCell()
    {
        CloseSelect();
        moveList.Add(selected);
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
