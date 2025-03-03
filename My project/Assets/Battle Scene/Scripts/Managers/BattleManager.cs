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
    public int AC;

    public static Action RandomSpeed;
    public static Action ReadyStageStart;
    public static Action RoundStart;

    public BattleUIManager _battleUIManager;

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
        RoundStart += NodeReset;

        Testlist = new List<GameObject>();
        ThoseAttackingPi = new List<GameObject>[12];
        for (int i = 0; i < 12; i++)
        {
            ThoseAttackingPi[i] = new List<GameObject>();
        }//列表初始化
        //基本对象的载入

        RoundCount = 0;
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
        if (DataofAttackers.ActionCount != DataofAttackers.ActionCount_P)
        {
            // 值发生变化时调用的方法
            OnAcitionCountChanged();
            DataofAttackers.ActionCount_P = DataofAttackers.ActionCount;
        }

        AC = DataofAttackers.ActionCount;
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

    public void NodeReset()
    {
        foreach (GameObject node in spawner.nodeList)
        {
            node.GetComponent<Nodes>().isSelected = false;
            _battleUIManager.CloseAllCircles(); 
        }
    }


    public void ActInOrder()
    {
        ActionOrder[0].GetComponent<ActioninBattleManager>().Act();
        //实际上是所有人都行动了
        //foreach (GameObject ThisCharacter in ActionOrder)
        //{
        //    ThisCharacter.GetComponent<ActioninBattleManager>().Act();
        //}
    }

    public void OnAcitionCountChanged()
    {
         

        if (DataofAttackers.ActionCount < ActionOrder.Count)
        {
            if (ActionOrder[DataofAttackers.ActionCount] != null)
            {
                ActionOrder[DataofAttackers.ActionCount].GetComponent<ActioninBattleManager>().Act();
            }
            else
            {
                DataofAttackers.ActionCount++;
            }
        }
        else
        {
            NextRound();
        }
    }

    private void Initialization()
    {
        ActionOrder.Clear();

        DataofAttackers.ActionCount = 0;
        DataofAttackers.ActionCount_P = 0;

        RoundCount++;

        _battleUIManager.UpdateRoundText();

        ActionOrder.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        ActionOrder.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (GameObject character in ActionOrder)
        {
            character.GetComponent<ActioninBattleManager>().GetRandomSpeed();
            //buff
        }

        foreach (List<GameObject> AtkersList in ThoseAttackingPi)
        {
            AtkersList.Clear();
        }

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
    {//确认操作后把该移进相关列表的移进去
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

    public void NextRound()
    {
        if (IsBattleOver())
        {
            if (PlayerWon())
            {
                _battleUIManager.ShowGameWonPanel();
            }
            else
            {
                _battleUIManager.ShowGameLosePanel();
            }
        }
        else
        {
            Initialization();
        }
    }

    private void OnDestroy()
    {
        ActionOrder.Clear();
        ReadyStageStart -= Initialization;
        RoundStart -= ActInOrder;
    }

    public bool IsBattleOver()
    {//判断战斗是否结束
        bool PlayerAllDead = true;
        bool EnemyAllDead = true;
        for (int i = 0; i < 6; i++)
        {
            if (spawner.nodeList[i].GetComponent<Nodes>().isPlayerHere)
            {
                PlayerAllDead = false;
            }
        }

        for (int i = 6; i < 12; i++)
        {
            if (spawner.nodeList[i].GetComponent<Nodes>().isEnemyHere)
            {
                EnemyAllDead = false;
            }
        }


        return (PlayerAllDead || EnemyAllDead);
    }

    //判断玩家有没有获胜,仅在战斗结束之后使用！
    public bool PlayerWon()
    {//判断玩家有没有获胜,仅在战斗结束之后使用！
        bool PlayerAllDead = true;
        for (int i = 0; i < 6; i++)
        {
            if (spawner.nodeList[i].GetComponent<Nodes>().isPlayerHere)
            {
                PlayerAllDead = false;
            }
        }

        return (!PlayerAllDead);
    }
    
}
