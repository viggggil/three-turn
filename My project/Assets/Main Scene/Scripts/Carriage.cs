using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Carriage : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager GameManager;
    public int moveRange;
    public int curRange;
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        GameManager.selected = this.gameObject;
        GameManager.ShowMoveRange();
    }

    public void Move(Vector2 direction)
    {
        transform.position = direction;
    }
}
