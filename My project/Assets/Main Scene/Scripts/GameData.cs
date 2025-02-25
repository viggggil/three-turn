using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameData : MonoBehaviour
{
    public UnityEvent<int[], Vector3[]> PlayersUpdate;

    public GameSaveData gsd=new GameSaveData();

    public PlayerTeamState playerTeamState;

    public GameManager GameManager;
    public class GameSaveData
    {
        public bool[] isHere;
        public  bool[] actAssigned;
        public bool[] haveActed;
        public  bool[] isDead;
        public  int[] curHealth;
        public  int[] maxHealth;
        public  int[] curMagic;
        public  int[] maxMagic;
        public  int[] curStamina;
        public  int[] maxStamina;
        public  int[] minSpeed;
        public int[] maxSpeed;
        public Vector3[] positions;
        public Vector3[] Epositions;
        public int[] types;
        public Vector3[] Epositions2;
        public int[] types2;
        public int TurnNumber;
        public int[] Professions;
        public GameSaveData()
        {
            isHere = new bool[3] { false, false, false };
            isDead = new bool[3] { false, false, false };
            curHealth = new int[3] { 5, 5, 5 };
            maxHealth = new int[3] { 5, 5, 5 };
            curMagic = new int[3] { 5, 5, 5 };
            maxMagic = new int[3] { 5, 5, 5 };
            curStamina = new int[3] { 5, 5, 5 };
            maxStamina = new int[3] { 5, 5, 5 };
            positions = new Vector3[3];
            Epositions = new Vector3[10];
            types = new int[10];
            Epositions2 = new Vector3[10];
            types2 = new int[10];
            TurnNumber = 1;
            Professions = new int[3] { SceneLoader.PlayerOneProfession, SceneLoader.PlayerTwoProfession, -1 };
        }
    }



    public void Start()
    {
        playerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SaveStaminaAndPosition(int currentStamina,Vector3 position, int ID)
    {
        gsd.curStamina[ID - 1] = currentStamina;
        gsd.positions[ID - 1] = position;
    }

    public void LoadStaminaAndPosition()
    {
        PlayersUpdate?.Invoke(gsd.curStamina,gsd.positions);
    }

    public void SavePlayerTeamState()
    {
        gsd.curHealth = PlayerTeamState.PlayerState.curHealth;
        gsd.maxHealth = PlayerTeamState.PlayerState.maxHealth;
    }
}
