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

    public int RoundCount;

    public static Action RandomSpeed;
    public static Action ReadyStageStart;
    public static Action RoundStart;

    public List<GameObject> ActionOrder;
    public List<GameObject> Testlist;
    [Header("Attackers' Lists")]
    public List<GameObject>[] ThoseAttackingPi;

    static public int round;


    private void Awake()
    {
        Spawner = GameObject.FindWithTag("Spawner");
        spawner = Spawner.GetComponent<Spawner>();//找到Spawner并获得它的脚本
        
        ReadyStageStart += Initialization;
        RoundStart += ActInOrder;

        Testlist = new List<GameObject>();
        ThoseAttackingPi = new List<GameObject>[12];
        for (int i = 0; i < 12; i++)
        {
            ThoseAttackingPi[i] = new List<GameObject>();
        }//列表初始化
        //基本对象的载入

        RoundCount = 1;
    }

    private void Start()
    {
        /*加载阶段*/
        spawner.LoadPlayers();
        spawner.LoadEnemies();

        //确认载入后回合开始
        //RandomSpeed?.Invoke();

        //当战斗还没结束时(没有变量在存这个)

            /*回合准备阶段*/
            ReadyStageStart?.Invoke();
            //(包含每个人的buff生效和速度重新随机)

            /*回合进行阶段*/
            //通过按钮激活


        








    }

    public void RoundStartNow()
    {
        RoundStart?.Invoke();
    }


    private void Update()
    {
        //仅供测试
        //dataofAttackers.ThoseAttackingP7.AddRange( dataofAttackers.ThoseAttackingPi[7]);
    }

    public void BattleIsOver()
    {

    }

    public void SortListsOfAttackers()
    {
        foreach (List<GameObject> AttackersLists in dataofAttackers.ThoseAttackingPi)
        {
            AttackersLists.Sort((b, a) =>
            {//按速度的降序
                CharacterProperty propA = a.GetComponent<CharacterProperty>();
                CharacterProperty propB = b.GetComponent<CharacterProperty>();

                return propA.SpeedThisRound.CompareTo(propB.SpeedThisRound);
            });//按照每个人的速度进行排序
        }
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

    public void ActionConfirmed()
    {
        foreach (GameObject Atkers in ActionOrder)
        {
            if (Atkers.GetComponent<CharacterProperty>().OnTheAttack)
            {/*********************这里也有一大堆东西要补充********************/

                List<int> AtkRangeOfthis = new List<int>();

                switch (Atkers.GetComponent<CharacterProperty>().Profession)
                {//这层switch区分各个职业
                    case 0:
                        switch (Atkers.GetComponent<CharacterProperty>().AtkTargetPosition)
                        {
                            case 0:
                                //调用返回攻击范围的函数
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            default:
                                break;
                        }
                        break;

                    case 1:
                        switch (Atkers.GetComponent<CharacterProperty>().AtkTargetPosition)
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            default:
                                break;
                        }
                        break;
                        
                    case 2:
                        switch (Atkers.GetComponent<CharacterProperty>().AtkTargetPosition)
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            default:
                                break;
                        }
                        break;
                        
                    case 3:
                        switch (Atkers.GetComponent<CharacterProperty>().AtkTargetPosition)
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            default:
                                break;
                        }
                        
                        break;
                    default:
                        break;
                }
                //AtkRangeOfthis.AddRange(某个算范围的函数)

                foreach (int Range in AtkRangeOfthis)
                {
                    ThoseAttackingPi[Range].Add(gameObject);
                }
            }
        }
        //调用所有要攻击的人的函数
    }

    private void OnDestroy()
    {
        ActionOrder.Clear();
        ReadyStageStart -= Initialization;
        RoundStart -= ActInOrder;
    }

    public bool IsBattleOver()
    {//判断战斗是否结束
        bool isover = false;
        foreach (GameObject node in spawner.nodeList)
        {
            if (node.GetComponent<Nodes>().isEnemyHere)
            {//任何一名敌人在场，isover不能为true
                isover = true;
            }
            else if (node.GetComponent<Nodes>().isPlayerHere)
            {
                isover = true;
            }
            else
            {
                isover = false;
            }
        }

        return isover;
    }

    //判断玩家有没有获胜,仅在战斗结束之后使用！
    public bool PlayerWon()
    {//判断玩家有没有获胜,仅在战斗结束之后使用！
        bool isPlayerWon = false;

        foreach (GameObject node in spawner.nodeList)
        {
            if (node.GetComponent<Nodes>().isPlayerHere)
            {
                isPlayerWon = true;
            }
            else
            {
                isPlayerWon = false;
            }
        }

        return isPlayerWon;
    }
    
}
