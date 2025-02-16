using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeamState : MonoBehaviour
{
    public static PlayerTeamState Instance { get; private set; }
    public GameData GameData;
    public int selectedID;
    public static class PlayerState
    {
        [Header("PlayerStateInfo")]
        public static bool[] isHere;//判断人物会不会进入战斗场景
        public static bool[] actAssigned;//判断人物是否被分配了行动
        public static bool[] haveActed;//判断人物是不是已经有过操作了
        public static bool[] isDead;//有没有回合内死亡
        public static int[] curHealth;
        public static int[] maxHealth;
        public static int[] curMagic;
        public static int[] maxMagic;
        public static int[] playerPosition;//位置
        public static int[] minSpeed;//速度
        public static int[] maxSpeed;
        public static int[] playerKind;//职业,目前怎么写待定
        public static int[] pla;
        static PlayerState()
        {
            isHere = new bool[3] { false, false, false };
            isDead = new bool[3] { false, false, false };
            curHealth = new int[3] { 5, 5, 5 };
            maxHealth = new int[3] { 5, 5, 5 };
            curMagic = new int[3] { 5, 5, 5 };
            maxMagic = new int[3] { 5, 5, 5 };
            playerPosition = new int[3];
            minSpeed = new int[3] { 1, 2, 3 };
            maxSpeed = new int[3] { 3, 4, 5 };
        }
    }
    public UIManager UIManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void LoadGameData()
    {
        PlayerState.curHealth = GameData.gsd.curHealth;
        PlayerState.maxHealth = GameData.gsd.maxHealth;
        PlayerState.curMagic = GameData.gsd.curMagic;
        PlayerState.maxMagic = GameData.gsd.maxMagic;
        for(int i = 1; i <= 3; i++)
        {
            UIManager.UpdateHealthSlider(PlayerState.curHealth[i - 1], PlayerState.maxHealth[i - 1], i);
            UIManager.UpdateMagicSlider(PlayerState.curMagic[i - 1], PlayerState.maxMagic[i - 1], i);
        }
        PlayerState.minSpeed = GameData.gsd.playerSpeed;
        PlayerState.maxSpeed = GameData.gsd.playerSpeed;
    }

    public void UpdateHealth(int serialNumber,int change)
    {
        PlayerState.curHealth[serialNumber] -= change;
        UIManager.UpdateHealthSlider(PlayerState.curHealth[serialNumber], PlayerState.maxHealth[serialNumber], serialNumber);
    }
    public void UpdateHealth(int change)
    {
        PlayerState.curHealth[selectedID] += change;
        if(PlayerState.curHealth[selectedID] > PlayerState.maxHealth[selectedID])
        {
            PlayerState.curHealth[selectedID] = PlayerState.maxHealth[selectedID];
        }
        UIManager.UpdateHealthSlider(PlayerState.curHealth[selectedID], PlayerState.maxHealth[selectedID], selectedID);
    }

    public void ChangeSelected(int ID)
    {
        selectedID = ID;
    }
    void Update()
    {
        
    }
}
