using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public DataofAttackers dataofAttackers;

    GameObject Spawner;

    Spawner spawner;

    public static Action RandomSpeed;
    public static Action ReadyStageStart;
    public static Action RoundStart;

    public List<GameObject> ActionOrder;
    //private List<List<GameObject>> battleLists;//这是存储所有列表的集合
    //private List<GameObject> battleList1;
    //private List<GameObject> battleList2;
    //private List<GameObject> battleList3;

    static public int round;

    private bool isGameOver;

    private void Awake()
    {
        Spawner = GameObject.FindWithTag("Spawner");
        spawner = Spawner.GetComponent<Spawner>();//找到Spawner并获得它的脚本

        ReadyStageStart += Initialization;
        RoundStart += ActInOrder;
        //battleLists = new List<List<GameObject>>();
        //battleList1 = new List<GameObject>();
        //battleList2 = new List<GameObject>();
        //battleList3 = new List<GameObject>();//列表初始化
        //基本对象的载入


    }

    private void Start()
    {
        /*加载阶段*/
        spawner.LoadPlayers();
        spawner.LoadEnemies();

        //确认载入后回合开始
        //RandomSpeed?.Invoke();
        /*回合准备阶段*/
        ReadyStageStart?.Invoke();

        /*回合进行阶段*/

        RoundStart?.Invoke();


    }

    public void ActInOrder()
    {
        foreach (GameObject ThisCharacter in ActionOrder)
        {
            ThisCharacter.GetComponent<ActioninBattleManager>().Act();
        }
    }

    private void Initialization()
    {
        ActionOrder.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        ActionOrder.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        ActionOrder.Sort((b, a) =>
        {
            CharacterProperty propA = a.GetComponent<CharacterProperty>();
            CharacterProperty propB = b.GetComponent<CharacterProperty>();

            return propA.SpeedThisRound.CompareTo(propB.SpeedThisRound);
        });//按照每个人的速度进行排序

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


    //public void Add2toBattleList(GameObject obj1,GameObject obj2)
    //{
    //    // 标记是否找到包含其中一个物体的列表
    //    bool foundList = false;

    //    // 遍历所有列表
    //    foreach (List<GameObject> list in battleLists)
    //    {
    //        if (list.Contains(obj1))
    //        {
    //            // 如果列表包含obj1，将obj2添加到该列表
    //            if (!list.Contains(obj2))
    //            {
    //                list.Add(obj2);
    //            }
    //            foundList = true;
    //            break;
    //        }
    //        else if (list.Contains(obj2))
    //        {
    //            // 如果列表包含obj2，将obj1添加到该列表
    //            if (!list.Contains(obj1))
    //            {
    //                list.Add(obj1);
    //            }
    //            foundList = true;
    //            break;
    //        }
    //    }

    //    // 如果没有找到包含其中一个物体的列表，创建一个新列表
    //    if (!foundList)
    //    {
    //        List<GameObject> newList = new List<GameObject>();
    //        newList.Add(obj1);
    //        newList.Add(obj2);
    //        battleLists.Add(newList);
    //    }


    //    //测试代码，没有问题后应该删除
    //    foreach (List<GameObject> list in battleLists)
    //    {
    //        foreach (GameObject obj in list)
    //        {
    //            Debug.Log(obj.name);
    //        }
    //    }

    //}
    private void OnDestroy()
    {
        ActionOrder.Clear();
        ReadyStageStart -= Initialization;
        RoundStart -= ActInOrder;
    }

    
    
}
