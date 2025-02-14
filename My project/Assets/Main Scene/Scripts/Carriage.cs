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
    private PlayerTeamState PlayerTeamState;
    public GameData GameData;
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        CameraFollower = GameObject.Find("CameraFollower").GetComponent<CameraFollower>();
        PlayerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        if(playerID!=-1)TurnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        UIManager.CloseInfo();
        CameraFollower.ChangeSelected(this.gameObject);
        PlayerTeamState.ChangeSelected(playerID);
        if (!isSelected){ 
        GameManager.selectedID = playerID;
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
            GameManager.ClearFog();
            curStamina -= 1;
            StaminaUpdate?.Invoke(curStamina, maxStamina,playerID);
            GameData.SaveStaminaAndPosition(curStamina,transform.position, playerID);
            isSelected = false;
        }
       
    }

    public void UpdateStaminaAndPosition(int[] arr, Vector3[]arr2)
    {
        if (playerID == -1) return;
        curStamina = arr[playerID - 1];
        transform.position = arr2[playerID - 1];
        StaminaUpdate?.Invoke(curStamina, maxStamina, playerID);
        GameManager.selectedID = playerID;
        GameManager.ClearFog();
    }
    public void TurnStart()
    {
        curStamina = maxStamina;
        StaminaUpdate?.Invoke(curStamina, maxStamina,playerID);
        GameData.SaveStaminaAndPosition(curStamina,transform.position, playerID);
    }
}
