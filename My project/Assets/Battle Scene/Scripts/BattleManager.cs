using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject Spawner;

    Spawner spawner;

    private List<List<GameObject>> battleLists;//这是存储所有列表的集合
    private List<GameObject> battleList1;
    private List<GameObject> battleList2;
    private List<GameObject> battleList3;

    static public int round;

    private bool isGameOver;

    private void Awake()
    {
        Spawner = GameObject.FindWithTag("Spawner");
        spawner = Spawner.GetComponent<Spawner>();//找到Spawner并获得它的脚本
        
        battleLists = new List<List<GameObject>>();
        battleList1 = new List<GameObject>();
        battleList2 = new List<GameObject>();
        battleList3 = new List<GameObject>();//列表初始化
        //基本对象的载入
        
       
    }

    private void Start()
    {
        /*加载阶段*/
        spawner.LoadPlayers();
        spawner.LoadEnemies();

        //确认载入后回合开始

        /*回合准备阶段*/
    }

    private void Attack(int damage, int diffofSpeed, params int[] AtkRange)
    {//打算使用攻击性行动

    }

    private int DamageCalculator(int rawDamage,int uncertainty,int diffofSpeed)
    {
        int ultimateDmg;
        if (Mathf.Abs(diffofSpeed) == 0f)
        {
            ultimateDmg = rawDamage;
        }
        else
        {
            ultimateDmg = 0;
        }
        return ultimateDmg;
    }
}
