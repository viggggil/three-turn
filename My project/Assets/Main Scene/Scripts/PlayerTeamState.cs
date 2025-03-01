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
        public static bool[] isHere;//有没有参与战斗
        public static int BattleResult; //传出0表示失败，1表示胜利
        public static bool[,] equips;
        public static int EnemyType;
        public static int[] Professions;//职业
        public static CharacterProperty[] characterProperties;

        static PlayerState()
        {
            //暂时更改一下
            isHere = new bool[3] { true,true,false };
            equips = new bool[3, 30];
            characterProperties = new CharacterProperty[3];
            BattleResult = -1;
            //这里也有更改
            Professions = new int[3] { 0, 1, 2 };
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


    public void ChangeSelected(int ID)
    {
        selectedID = ID;
    }
    void Update()
    {

    }

    public void LoadCharacterProperties(GameObject[] arr)
    {
        for(int i = 0; i < 3; i++)
        {
            CharacterProperty temp = arr[i].GetComponent<CharacterProperty>();
            if (temp)
            {
                temp.maxHealth = GameData.gsd.maxHealth[i];
                temp.Health = GameData.gsd.curHealth[i];
                PlayerState.characterProperties[i] = arr[i].GetComponent<CharacterProperty>();
            }
            else PlayerState.characterProperties[i] = null;
        }

    }
}
