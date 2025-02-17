using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


//挂在玩家和敌人身上的管理战斗场景中的行动的类
public class ActioninBattleManager : MonoBehaviour
{
    public CharacterProperty characterProperty;

    public BattleManager battleManager;//用UI拖来的
    public DataofAttackers dataofAttackers;

    

    private void Awake()
    {
        characterProperty = GetComponent<CharacterProperty>();

        

        BattleManager.RandomSpeed += RandomSpeed;
    }

    private void Start()
    {

        characterProperty.SpeedThisRound = Random.Range(characterProperty.MinSpeed, characterProperty.MaxSpeed);

    }

    private void RandomSpeed()
    {//用于订阅的提供具体速度的函数
        characterProperty.SpeedThisRound = Random.Range(characterProperty.MinSpeed, characterProperty.MaxSpeed);
    }


    //这是回合准备阶段准备攻击速度判断的方法
    private void GonnaAttack(params int[] AtkRange)
    {//打算使用攻击性行动
        foreach (int i in AtkRange)
        {
            dataofAttackers.AttackersLists[i].Add(gameObject);
        }//如果在攻击某个点，把它移到对应的列表里面


        //先看攻击的范围内有多少个敌人
        //int enemyCount = 0;
        //string targetTag = "Enemy";
        //foreach (int i in AtkRange)
        //{//对于每一个在攻击范围内的对象：
        //    //先获取所有node的子物体transform
        //    Transform[] allChildren = Spawner.nodeDictionary[i].GetComponentsInChildren<Transform>();
        //    //然后遍历
        //    foreach (Transform child in allChildren)
        //    {
        //        if (child.CompareTag(targetTag))
        //        {
        //            enemyCount++;
        //        }
        //    }
        //}

        ////然后根据敌人人数来确定用哪种方法来加入排序列表

        //if (enemyCount == 1)
        //{

        //}
    }

    //这是准备阶段结束要进入进行阶段时计算速度差对攻击伤害导致的影响的方法
    private void AtkJudger(int OriginalDmg)
    {//仅针对于要进行攻击行动的角色
        //if ()
        //{

        //}
    }

    private void OnDestroy()
    {
        BattleManager.RandomSpeed += RandomSpeed;
    }
}
