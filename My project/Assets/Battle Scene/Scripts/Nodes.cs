using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class Nodes : MonoBehaviour
{
    GameObject actionPanel;
    public GameObject enemySkillsPanel;
    GameObject BattleUIManager;
    public GameObject Bars;
    public GameObject Shield;
    public GameObject Arrow;
    public GameObject Sword;

    public DataofNodes dataofNodes;

    BattleUIManager battleUIManager;

    [SerializeField] int nodeCode;

    private List<int> SampleList;

    public bool isPlayerHere;
    public bool isEnemyHere;//在角色移动之后记得修改
    private bool isUnderAtk;//跟角色的移动有关，如果遭受攻击则该格不能移到；
    public bool isPlayerNode;
    public bool isSelected;
    public bool othersSelected = false;


    private void Awake()
    {
        actionPanel = GameObject.FindWithTag("ActionPanel");
        BattleUIManager = GameObject.FindWithTag("BattleUIManager");
        battleUIManager = BattleUIManager.GetComponent<BattleUIManager>();
        SampleList = new List<int>();
    }

    private void Update()
    {
        if (isPlayerHere || isEnemyHere)
        {
            Bars.SetActive(true);
        }
        else
        {
            Bars.SetActive(false);
        }
    }

    public void OpenActionPanel()
    {
        if (!dataofNodes.anyPSelected)
        {
            battleUIManager.OpenActionPanel();
            battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
            dataofNodes.isPNodesSelected[nodeCode] = true;
            isSelected = true;
            dataofNodes.anyPSelected = true;
        }


        else
        {
            for (int i = 0; i < dataofNodes.isPNodesSelected.Length; i++)
            {
                if (dataofNodes.isPNodesSelected[i] == true)
                {
                    dataofNodes.SelectedPNodeCode = i;
                    break;
                }//结束之后i应该是选择了的node的下标
            }

            if (dataofNodes.SelectedPNodeCode == nodeCode)
            {
                battleUIManager.CloseActionPanel();
                battleUIManager.Circles[nodeCode].gameObject.SetActive(false);//关自己的
                dataofNodes.isPNodesSelected[nodeCode] = false;
                isSelected = false;
                dataofNodes.anyPSelected = false;
            }
            else
            {
                battleUIManager.OpenActionPanel();
                battleUIManager.Circles[dataofNodes.SelectedPNodeCode].gameObject.SetActive(false);//关别人的
                dataofNodes.isPNodesSelected[dataofNodes.SelectedPNodeCode] = false;
                battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
                dataofNodes.isPNodesSelected[nodeCode] = true;
                isSelected = true;
            }
        }

    }

    public void OnPlayerNodeClick() 
    {//node被点击时候的方法
        if (isPlayerHere)
        {
            if (Spawner.nodeDictionary[nodeCode].GetComponentInChildren<CharacterProperty>().OnTheDefense)
            {//如果已经为该角色选择了防御
                battleUIManager.OpenExitDefensePanel();
                battleUIManager.CloseActionPanel();
                battleUIManager.DisableAllNodeButtons();
                battleUIManager.Circles[dataofNodes.SelectedPNodeCode].gameObject.SetActive(false);//关别人的
                dataofNodes.isPNodesSelected[dataofNodes.SelectedPNodeCode] = false;
                battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
                dataofNodes.isPNodesSelected[nodeCode] = true;
                isSelected = true;
            }

            else if (Spawner.nodeDictionary[nodeCode].GetComponentInChildren<CharacterProperty>().OnTheMovement)
            {//如果已经为该角色选择了移动
                battleUIManager.OpenExitMovementPanel();
                battleUIManager.ShowMoveTargetPoint();//顺带显示要去的地方
                battleUIManager.CloseActionPanel();
                battleUIManager.DisableAllNodeButtons();
                battleUIManager.Circles[dataofNodes.SelectedPNodeCode].gameObject.SetActive(false);//关别人的
                dataofNodes.isPNodesSelected[dataofNodes.SelectedPNodeCode] = false;
                battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
                dataofNodes.isPNodesSelected[nodeCode] = true;
                isSelected = true;
            }

            else if (Spawner.nodeDictionary[nodeCode].GetComponentInChildren<CharacterProperty>().OnTheAttack)
            {//如果已经为该角色选择了攻击
                battleUIManager.OpenExitAttackPanel();
                battleUIManager.ShowAtkRange();//顺带显示攻击范围
                battleUIManager.CloseActionPanel();
                battleUIManager.DisableAllNodeButtons();
                battleUIManager.Circles[dataofNodes.SelectedPNodeCode].gameObject.SetActive(false);//关别人的
                dataofNodes.isPNodesSelected[dataofNodes.SelectedPNodeCode] = false;
                battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
                dataofNodes.isPNodesSelected[nodeCode] = true;
                isSelected = true;
            }
            else
            {
                OpenActionPanel();
            }

            dataofNodes.SelectedPNodeCode = nodeCode;



        }

        else
        {//这里没有玩家
            if (dataofNodes.SbisMoving)
            {//有玩家打算移动到这里,这里其实算是接着BattleUIManager往下写
                //return code
                Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().TargetPosition = nodeCode;
                battleUIManager.DisableAllArrows();
                battleUIManager.EnableAllNodeButtons();
                battleUIManager.CloseExitMovementPanel();
                Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<Nodes>().DisplayArrow();
                Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().OnTheMovement = true;

                dataofNodes.SbisMoving = false;
            }
        }
    }


    public void OnEnemyNodeClick()/*这里还有一大堆东西要补充*/
    {//node被点击时候的方法
        if (isEnemyHere)
        {
            if (dataofNodes.SbisAttacking)
            {//确定攻击阶段
                switch (Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().Profession)
                {//看选中的人物的职业
                    case 0://骑士
                        switch (Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().SkillCode)
                        {//根据用的是哪个技能和点的哪里来判断能否成功指派行动
                            case 0://技能1
                                if (Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<Knight>().skill1Range(nodeCode))
                                {//把这个点代进去，如果在范围内，指派成功
                                    Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().AtkTargetPosition = nodeCode;
                                    battleUIManager.SkillSelectionSucceeded();
                                    Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().OnTheAttack = true;
                                    //回去改UI

                                    //临时加入
                                    SampleList.Add(Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().atkTargetPosition);
                                }
                                else
                                {
                                    battleUIManager.SkillSelectionFailed();
                                    //也是回去改UI
                                }
                                break;

                            case 1:
                                if (Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<Knight>().skill2Range(nodeCode))
                                {//把这个点代进去，如果在范围内，指派成功
                                    Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().AtkTargetPosition = nodeCode;
                                    battleUIManager.SkillSelectionSucceeded();
                                    Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().OnTheAttack = true;
                                    //回去改UI
                                }
                                else
                                {
                                    battleUIManager.SkillSelectionFailed();
                                    //也是回去改UI
                                }
                                break;
                            case 2:
                                if (Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<Knight>().skill3Range())
                                {//把这个点代进去，如果在范围内，指派成功
                                    Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().AtkTargetPosition = nodeCode;
                                    battleUIManager.SkillSelectionSucceeded();
                                    Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().OnTheAttack = true;
                                    //回去改UI
                                }
                                else
                                {
                                    battleUIManager.SkillSelectionFailed();
                                    //也是回去改UI
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case 1://弓箭手
                        break;

                    case 2://牧师
                        break;

                    case 3://法师
                        break;

                    default:
                        break;
                }
                


            }
            else
            {
                if (!dataofNodes.anyESelected)
                {
                    battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
                    dataofNodes.isENodesSelected[nodeCode - 6] = true;
                    isSelected = true;
                    dataofNodes.anyESelected = true;
                    enemySkillsPanel.gameObject.SetActive(true);
                }


                else
                {
                    for (int i = 0; i < dataofNodes.isENodesSelected.Length; i++)
                    {
                        if (dataofNodes.isENodesSelected[i] == true)
                        {
                            dataofNodes.SelectedENodeCode = i + 6;
                            break;
                        }//结束之后i应该是选择了的node的下标
                    }

                    if (dataofNodes.SelectedENodeCode == nodeCode)
                    {
                        battleUIManager.CloseActionPanel();
                        battleUIManager.Circles[nodeCode].gameObject.SetActive(false);//关自己的
                        dataofNodes.isENodesSelected[nodeCode - 6] = false;
                        isSelected = false;
                        dataofNodes.anyESelected = false;
                        enemySkillsPanel.gameObject.SetActive(false);
                    }
                    else
                    {
                        battleUIManager.Circles[dataofNodes.SelectedENodeCode].gameObject.SetActive(false);//关别人的
                        dataofNodes.isENodesSelected[dataofNodes.SelectedENodeCode - 6] = false;
                        battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
                        dataofNodes.isENodesSelected[nodeCode - 6] = true;
                        isSelected = true;
                    }
                }

                dataofNodes.SelectedENodeCode = nodeCode;


            }
        }
    }


    public void DisplayShield()
    {
        Vector3 position = gameObject.transform.position;
        GameObject ThatShield =
        GameObject.Instantiate(Shield,position + new Vector3 (0.6f,0f,0f ) , Quaternion.identity, gameObject.transform);
        ThatShield.transform.name = "Shield";
    }

    public void DisplayArrow()
    {
        Vector3 position = gameObject.transform.position;
        GameObject ThatArrow =
        GameObject.Instantiate(Arrow, position + new Vector3(0.6f, 0f, 0f), Quaternion.identity, gameObject.transform);
        ThatArrow.transform.name = "Arrow";
    }

    public void DisplaySword()
    {
        Vector3 position = gameObject.transform.position;
        GameObject ThatSword =
        GameObject.Instantiate(Sword, position + new Vector3(0.6f, -0.3f, 0f), Quaternion.identity, gameObject.transform);
        ThatSword.transform.name = "Sword";
    }

    public GameObject FindChildCharacter()
    {
        GameObject Child = Spawner.nodeDictionary[nodeCode].GetComponentInChildren<CharacterProperty>().gameObject;
        if ( Child != null )
            return Child;
        else return null;
    }

    
}
