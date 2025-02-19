using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data of Attackers",menuName = "AttackerData",order = 1)]
public class DataofAttackers : ScriptableObject
{
    [Header("Attackers' Lists")]
    public List<List<GameObject>> AttackersLists;
    public List<GameObject> ThoseAtkingP0;
    public List<GameObject> ThoseAtkingP1;
    public List<GameObject> ThoseAtkingP2;
    public List<GameObject> ThoseAtkingP3;
    public List<GameObject> ThoseAtkingP4;
    public List<GameObject> ThoseAtkingP5;
    public List<GameObject> ThoseAtkingP6;
    public List<GameObject> ThoseAtkingP7;
    public List<GameObject> ThoseAtkingP8;
    public List<GameObject> ThoseAtkingP9;
    public List<GameObject> ThoseAtkingP10;
    public List<GameObject> ThoseAtkingP11;
    public List<GameObject> ActionOrder;

    public int[] SpeedofPlayers;
    public int[] SortedSpeedofPlayers;

    private void Awake()
    {
        AttackersLists = new List<List<GameObject>>();
        ThoseAtkingP0 = new List<GameObject>();
        ThoseAtkingP1 = new List<GameObject>();
        ThoseAtkingP2 = new List<GameObject>();
        ThoseAtkingP3 = new List<GameObject>();
        ThoseAtkingP4 = new List<GameObject>();
        ThoseAtkingP5 = new List<GameObject>();
        ThoseAtkingP6 = new List<GameObject>();
        ThoseAtkingP7 = new List<GameObject>();
        ThoseAtkingP8 = new List<GameObject>();
        ThoseAtkingP9 = new List<GameObject>();
        ThoseAtkingP10 = new List<GameObject>();
        ThoseAtkingP11 = new List<GameObject>();
        ActionOrder = new List<GameObject>();
        AttackersLists.Add(ThoseAtkingP0);
        AttackersLists.Add(ThoseAtkingP1);
        AttackersLists.Add(ThoseAtkingP2);
        AttackersLists.Add(ThoseAtkingP3);
        AttackersLists.Add(ThoseAtkingP4);
        AttackersLists.Add(ThoseAtkingP5);
        AttackersLists.Add(ThoseAtkingP6);
        AttackersLists.Add(ThoseAtkingP7);
        AttackersLists.Add(ThoseAtkingP8);
        AttackersLists.Add(ThoseAtkingP9);
        AttackersLists.Add(ThoseAtkingP10);
        AttackersLists.Add(ThoseAtkingP11);

        //用于最后决定行动顺序
        SpeedofPlayers = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    
}
