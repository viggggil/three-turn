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
    public GameObject fog;
    private bool isSelected;
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GameManager.selected = this.gameObject;
        if (!GameManager.SelectedCell() ){
            if (!isSelected)
            {
                moveCell.SetActive(true);
                Vector2 sceenpos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.5f, 0, 0));
                UIManager.DisplayInfo(sceenpos, type);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
          
        }
    }
}
