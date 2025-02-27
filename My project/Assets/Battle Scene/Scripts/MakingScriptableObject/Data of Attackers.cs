using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data of Attackers",menuName = "AttackerData",order = 1)]
public class DataofAttackers : ScriptableObject
{
    [Header("Attackers' Lists")]
    public List<GameObject>[] ThoseAttackingPi;
    public List<GameObject> ActionOrder;

    public int[] SpeedofPlayers;
    public int[] SortedSpeedofPlayers;

    private void Awake()
    {
        ThoseAttackingPi = new List<GameObject>[12];
        for (int i = 0; i < 12; i++)
        {
            ThoseAttackingPi[i] = new List<GameObject>();
        }
        ActionOrder = new List<GameObject>();
        

        //用于最后决定行动顺序
        SpeedofPlayers = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    
}
