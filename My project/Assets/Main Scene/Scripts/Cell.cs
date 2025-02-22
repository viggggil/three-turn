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
}
