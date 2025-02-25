using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerTeamState;

public class PlayerTeamState : MonoBehaviour
{
    public static PlayerTeamState Instance { get; private set; }
    public GameData GameData;
    public int selectedID;
    public static class PlayerState
    {
        [Header("PlayerStateInfo")]
        public static bool[] isHere;//�ж�����᲻�����ս������
        public static int[] curHealth;
        public static int[] maxHealth;
        public static int BattleResult; //传出0表示失败，1表示逃跑，2表示胜利
        public static bool[,] equips;
        public static CharacterProperty[] characterProperties;

        static PlayerState()
        {
            isHere = new bool[3] { false, false, false };
            curHealth = new int[3] { 5, 5, 5 };
            maxHealth = new int[3] { 5, 5, 5 };
            equips = new bool[3, 30];
            characterProperties = new CharacterProperty[3];
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
        for (int i = 1; i <= 3; i++)
        {
            UIManager.UpdateHealthSlider(PlayerState.curHealth[i - 1], PlayerState.maxHealth[i - 1], i);
        }
    }

    public void UpdateHealth(int serialNumber, int change)
    {
        PlayerState.curHealth[serialNumber] -= change;
        UIManager.UpdateHealthSlider(PlayerState.curHealth[serialNumber], PlayerState.maxHealth[serialNumber], serialNumber);
    }
    public void UpdateHealth(int change)
    {
        PlayerState.curHealth[selectedID] += change;
        if (PlayerState.curHealth[selectedID] > PlayerState.maxHealth[selectedID])
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

    public void LoadCharacterProperties(GameObject[] arr)
    {
        PlayerState.characterProperties[0] = arr[0].GetComponent<CharacterProperty>();
        PlayerState.characterProperties[1] = arr[1].GetComponent<CharacterProperty>();
        PlayerState.characterProperties[2] = arr[2].GetComponent<CharacterProperty>();
    }
}
