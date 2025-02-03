using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Carriage : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager GameManager;
    public int maxStamina;
    public int curStamina;
    private bool isSelected;
    public UIManager UIManager;
    public UnityEvent<float,float,int> StaminaUpdate;
    public CameraFollower CameraFollower;
    public int playerID;
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        CameraFollower= GameObject.Find("CameraFollower").GetComponent<CameraFollower>();
        TurnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        UIManager.CloseInfo();
        CameraFollower.ChangeSelected(this.gameObject);
        if (!isSelected){ 
        GameManager.selectedPlayer = this.gameObject;
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
        if (curStamina >=1)
        {
            transform.position = direction;
            transform.position -= new Vector3(0, 0, 0.01f);
            curStamina -= 1;
            StaminaUpdate?.Invoke(curStamina, maxStamina,playerID);
            isSelected = false;
        }
       
    }

    public void TurnStart()
    {
        curStamina = maxStamina;
        StaminaUpdate?.Invoke(curStamina, maxStamina,playerID);
    }
}
