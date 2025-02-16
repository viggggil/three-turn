using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


//挂在玩家和敌人身上的管理战斗场景中的行动的类
public class ActioninBattleManager : MonoBehaviour
{
    public BattleManager battleManager;

    private void Awake()
    {
        
    }



    private void GonnaAttack(int damage, int diffofSpeed, params int[] AtkRange)
    {//打算使用攻击性行动
        //首先将双方移入比较速度大小的列表

        //先看攻击的范围内有多少个敌人
        int enemyCount = 0;
        string targetTag = "Enemy";
        foreach (int i in AtkRange)
        {//对于每一个在攻击范围内的对象：
            //先获取所有的子物体transform
            Transform[] allChildren = Spawner.nodeDictionary[i].GetComponentsInChildren<Transform>();
            //然后遍历
            foreach (Transform child in allChildren) 
            {
                if (child.CompareTag(targetTag))
                {
                    enemyCount++;
                }
            }
        }

        //然后根据敌人人数来确定用哪种方法来加入排序列表

        if (AtkRange.Length == 1)
        {
            
        }
    }
}
