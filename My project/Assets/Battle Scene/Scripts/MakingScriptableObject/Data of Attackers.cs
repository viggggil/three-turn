using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Data of Attackers",menuName = "AttackerData",order = 1)]
public class DataofAttackers : ScriptableObject
{
    [Header("Attackers' Lists")]
    public List<GameObject>[] ThoseAttackingPi;
    public List<GameObject> ActionOrder;
    //以下代码是为了方便监视
    //public List<GameObject> ThoseAttackingP6;
    public List<GameObject> Samplelist;
    //public List<GameObject> ThoseAttackingP8;
    //public List<GameObject> ThoseAttackingP9;
    //public List<GameObject> ThoseAttackingP10;
    //public List<GameObject> ThoseAttackingP11;
    public static int ActionCount = 0;
    public static int ActionCount_P = 0;
    public int AC;


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

        Samplelist = new List<GameObject>();

        //用于最后决定行动顺序
        SpeedofPlayers = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    public void ActionCountPlus()
    {
        ActionCount++;
    }

    
}
