using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    [SerializeField] public int type;
    public bool TooNear;
    private GameManager GameManager;
    private UIManager UIManager;
    public GameObject moveCell;
    public GameObject fog;
    private bool isSelected;
    public bool isOn=false;
    public bool isFog = true;
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        fog.SetActive(true);
    }
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (!isOn&&!isFog)
        {
            GameManager.selected = this.gameObject;
            if (!GameManager.SelectedCell(type))
            {
                if (!isSelected)
                {
                    moveCell.SetActive(true);
                    isSelected = true;
                }
                else
                {
                    moveCell.SetActive(false);
                    GameManager.CloseSelect();
                    UIManager.CloseInfo();
                    isSelected = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Carriage temp = collision.GetComponent<Carriage>();
        if (temp)TooNear = true;
        Event_Map temp2 = collision.GetComponent<Event_Map>();
        if (temp2) TooNear = true;
        Enemy_Map temp3 = collision.GetComponent<Enemy_Map>();
        if (temp3) TooNear = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Carriage temp = collision.GetComponent<Carriage>();
        if (temp)TooNear = false;
        Event_Map temp2 = collision.GetComponent<Event_Map>();
        if (temp2) TooNear =false;
        Enemy_Map temp3 = collision.GetComponent<Enemy_Map>();
        if (temp3) TooNear = false;
    }
}
