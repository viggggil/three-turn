using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Carriage : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager GameManager;
    public int moveRange;
    public int curRange;
    private bool isSelected;
    public UIManager UIManager;
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
        UIManager.CloseInfo();
        if (!isSelected){ 
        GameManager.selected = this.gameObject;
        GameManager.ShowMoveRange();
            isSelected = true;
        }
        else
        {
            GameManager.CloseSelect();
            isSelected = false;
        }
    }

    public void Move(Vector2 direction)
    {
        transform.position = direction;
        transform.position-=new Vector3 (0,0,0.01f);
    }
}
