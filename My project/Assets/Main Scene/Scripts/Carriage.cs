using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;


public class Carriage : MonoBehaviour,IMove_
{
    // Start is called before the first frame update
    private GameManager GameManager;
    public int maxStamina;
    public int curStamina;
    public bool isSelected;
    public UIManager UIManager;
    public UnityEvent<float,float,int> StaminaUpdate;
    public CameraFollower CameraFollower;
    public int playerID;
    private PlayerTeamState PlayerTeamState;
    public GameData GameData;
    public GameObject tomb;
    public GameObject player;
    public Event_Map _tomb;
    public GameObject[] Professions;
    public Animator Animator;
    public AudioManager AudioManager;

    private Dictionary<int, int> ProfessionToMaxStamina = new Dictionary<int, int>()
    {
        {0,5 },{1,7},{2,4},{3,4 }
    };
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        CameraFollower = GameObject.Find("CameraFollower").GetComponent<CameraFollower>();
        PlayerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (playerID!=-1)TurnStart();
        tomb.SetActive(false);
        _tomb = GetComponent<Event_Map>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        if (this.enabled)
        {
            UIManager.CloseInfo();
            CameraFollower.ChangeSelected(this.gameObject);
            PlayerTeamState.ChangeSelected(playerID);
            if (!isSelected)
            {
                GameManager.ChangeSelected(playerID);
                GameManager.ShowMoveRange();
                UIManager.SelectPlayers[0].SetActive(false);
                UIManager.SelectPlayers[1].SetActive(false);
                UIManager.SelectPlayers[2].SetActive(false);
                UIManager.SelectPlayers[playerID-1].SetActive(true);
                isSelected = true;
            }
            else
            {
                GameManager.CloseSelect();
                isSelected = false;
            }
        }
    }

    public void Move(Vector2 direction)
    {
        if (curStamina >=1)
        {
            transform.DOMove(direction, 1.0f);
            AudioManager.PlaySFX(AudioManager.run);
            if (direction.x > player.transform.position.x)
            {
                player.GetComponent<RectTransform>().localScale = new Vector3(-1.25f, 1.25f, 1.25f);
            }
            if (direction.x < player.transform.position.x)
            {
                player.GetComponent<RectTransform>().localScale = new Vector3(1.25f, 1.25f, 1.25f);
            }
            Animator.SetBool("Move",true);
            Invoke("Move_", 1f);
        }
    }

    public void Move_()
    {
        Animator.SetBool("Move", false);
        GameManager.ClearFog();
        curStamina -= 1;
        StaminaUpdate?.Invoke(curStamina, maxStamina, playerID);
        GameData.SaveStaminaAndPosition(curStamina, transform.position, playerID);
        GameManager.CloseSelect();
        if (curStamina >= 1) Invoke("NextStep", 0.2f);
    }

    public void NextStep()
    {
        GameManager.ShowMoveRange();
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

    public void Dead()
    {
        tomb.SetActive(true);
        player.SetActive(false);
        GameData.UpdateHealth( playerID - 1,-10000);
        _tomb.enabled = true;
        this.enabled = false;
    }

    public void Reborn()
    {
        tomb.SetActive(false);
        player.SetActive(true);
        _tomb.enabled = false;
        this.enabled = true;
    }

    public void LoadPlayer(int Profession)
    {
        if (Profession == -1) return;
        player=Instantiate(Professions[Profession], transform.position, Quaternion.identity);
        player.transform.parent = transform;
        player.GetComponent<RectTransform>().Translate(new Vector3(0f, -0.3f, 0f));
        player.GetComponent<RectTransform>().localScale = new Vector3(1.25f, 1.25f, 1.25f);
        GameObject childObject = player.transform.GetChild(0).gameObject;
        Animator = childObject.GetComponent<Animator>();
        player.GetComponent<CharacterProperty>().SerialNumber = playerID - 1;
        player.GetComponent<CharacterProperty>().Profession= Profession;
        UIManager.LoadPlayer(Profession, playerID);
        GameData.gsd.Professions[playerID - 1] = Profession;
        PlayerTeamState.PlayerState.Professions[playerID - 1] = Profession;
    }
}
