using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


//挂在玩家和敌人身上的管理战斗场景中的行动的类
public class ActioninBattleManager : MonoBehaviour
{
    public CharacterProperty characterProperty;

    

    //public BattleManager battleManager;//用UI拖来的
    public DataofAttackers dataofAttackers;

    public int profession;
    

    private void Awake()
    {
        characterProperty = GetComponent<CharacterProperty>();


        BattleManager.RandomSpeed += RandomSpeed;
        BattleManager.ReadyStageStart += ExitDefense;

        
        //给每个玩家一个初始速度
    }

    private void Start()
    {
        GetRandomSpeed();

        profession = characterProperty.Profession;
    }

    public void Act()
    {
        if (characterProperty.OnTheAttack)
        {
            //播放动画

            switch (profession)
            {
                case 0://骑士
                    GetComponent<Knight>().Skills(characterProperty.SkillCode,characterProperty.AtkTargetPosition);
                    break;
                case 1://待补充
                    break;
                default:
                    break;
            }

            characterProperty.OnTheAttack = false;
        }
        else if (characterProperty.OnTheDefense)
        {
            //播放动画
        }
        else if (characterProperty.OnTheMovement)
        {
            //播放动画
        }
        else
        {
            //Do nothing
        }
    }





    public void GetRandomSpeed()
    {
        characterProperty.SpeedThisRound = characterProperty.OriginalSpeed + UnityEngine.Random.Range(characterProperty.MinRandomSpeed, characterProperty.MaxRandomSpeed);
    }

    private void RandomSpeed()
    {//用于订阅的提供具体速度的函数（已弃用）
        characterProperty.SpeedThisRound = UnityEngine.Random.Range(characterProperty.MinRandomSpeed, characterProperty.MaxRandomSpeed);

        
    }


    //这是回合准备阶段准备攻击速度判断的方法
    public void GonnaAttack(params int[] AtkRange)
    {//打算使用攻击性行动
        foreach (int i in AtkRange)
        {
            dataofAttackers.ThoseAttackingPi[i].Add(gameObject);
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
    private int AtkJudger(int OriginalDmg)
    {//仅针对于要进行攻击行动的角色，首次进行的伤害计算
        int maxSpeedofAttakers = 0;
        int diffofSpeed = 0;

        if (dataofAttackers.ThoseAttackingPi[characterProperty.Position] == null)
        {//如果说有人在打自己
            foreach(GameObject i in dataofAttackers.ThoseAttackingPi[characterProperty.Position])
            {//对于所有要打他自己位置的对象：
                if (i.GetComponent<CharacterProperty>().SpeedThisRound > maxSpeedofAttakers)
                {//检查每个人的速度，取出最大值
                    maxSpeedofAttakers = i.GetComponent<CharacterProperty>().SpeedThisRound;
                }
            }

            if (maxSpeedofAttakers <= characterProperty.SpeedThisRound)
            {//攻击自己的没有比自己速度快的
                return OriginalDmg;
            }
            else
            {//有人比自己速度快,快多少？
                diffofSpeed = maxSpeedofAttakers - characterProperty.SpeedThisRound;
                //速度最快者和自己速度的差
                return Convert.ToInt32(OriginalDmg * (0.8f - diffofSpeed * 0.1f));
            }//最后返回一个根据速度差设计了等差数列的
        }

        else
        {//没人在打自己
            return OriginalDmg;
        }//原伤害，不打折
    }

    public void GonnaDefense()
    {
        characterProperty.OnTheDefense = true;
    }

    private void ExitDefense()
    {//每个回合开始清除防御状态
        characterProperty.OnTheDefense = false;
    }

    private void GonnaMove()
    {
        //Hint Layout

    }

    private void OnDestroy()
    {
        BattleManager.RandomSpeed += RandomSpeed;
    }
}
