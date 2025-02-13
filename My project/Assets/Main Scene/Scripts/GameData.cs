using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameData : MonoBehaviour
{
    public UnityEvent<int[]> StaminaUpdate;

    public GameSaveData gsd=new GameSaveData();
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
        public GameSaveData()
        {
            isHere = new bool[3] { false, false, false };
            isDead = new bool[3] { false, false, false };
            curHealth = new int[3];
            maxHealth = new int[3] { 5, 5, 5 };
            curMagic = new int[3];
            maxMagic = new int[3];
            curStamina = new int[3];
            maxStamina = new int[3];
        }
        
    }

    public void Start()
    {
    }

    public void SaveStamina(int currentStamina, int ID)
    {
        gsd.curStamina[ID - 1] = currentStamina;
    }

    public void LoadStamina()
    {
        StaminaUpdate?.Invoke(gsd.curStamina);
    }
}
