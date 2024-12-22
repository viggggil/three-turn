using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    [SerializeField] public int type;
    private GameManager GameManager;
    private UIManager UIManager;
    public GameObject moveCell;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GameManager.selected = this.gameObject;
        if(!GameManager.SelectedCell()) moveCell.SetActive(true);
        Vector2 sceenpos = Camera.main.WorldToScreenPoint(transform .position );
        UIManager.displayinfo(sceenpos, type);
    }
}
