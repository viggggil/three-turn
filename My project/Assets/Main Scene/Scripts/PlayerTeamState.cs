using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeamState : MonoBehaviour
{
    public static PlayerTeamState Instance { get; private set; }
    public int selectedID;
    public static class PlayerState
    {
        public static bool[] isHere;
        public static int[] curHealth;
        public static int[] maxHealth;
        public static int[] curMagic;
        public static int[] maxMagic;
        static PlayerState()
        {
            isHere = new bool[3] { false, false, false };
            curHealth = new int[3];
            maxHealth = new int[3];
            curMagic = new int[3];
            maxMagic = new int[3];
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
