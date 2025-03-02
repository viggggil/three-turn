using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] GameObject actionPanel;
    [SerializeField] GameObject knightSkillsPanel;
    [SerializeField] GameObject archerSkillsPanel;
    [SerializeField] GameObject wizardSkillsPanel;
    [SerializeField] GameObject clericSkillsPanel;
    [SerializeField] GameObject enemySkillsPanel;
    [SerializeField] GameObject exitDefensePanel;
    [SerializeField] GameObject exitAttackPanel;
    [SerializeField] GameObject exitMovementPanel;
    [SerializeField] GameObject outOfRangePanel;
    [SerializeField] GameObject gameWonPanel;
    [SerializeField] GameObject gameLosePanel;

    [Header("Texts")]
    [SerializeField] Text RoundText;

    public BattleManager _battleManager;

    public DataofNodes dataofNodes;
    
    public List<Image> Circles;
    public List<Button> Buttons;
    public List<GameObject> Arrows;
    public List<GameObject> ArrowsofEnemies;


    private void Awake()
    {
        List<Image> list = new List<Image>();
    }

    

    public void OpenOutOfRangePanel()
    {
        GameObject.Instantiate(outOfRangePanel);
    }

    public void OpenActionPanel()
    {
        actionPanel.SetActive(true);
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
    }

    public void OpenSkillsPanel()
    {
        if(Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().Profession == 0)
            knightSkillsPanel.SetActive(true);

        else if (Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().Profession == 1)
            archerSkillsPanel.SetActive(true);

        else if (Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().Profession == 2)
            clericSkillsPanel.SetActive(true);

        else
            wizardSkillsPanel.SetActive(true);
        //导入技能
    }

    public void CloseSkillsPanel()
    {
        knightSkillsPanel.SetActive(false);
        archerSkillsPanel.SetActive(false);
        wizardSkillsPanel.SetActive(false);
        clericSkillsPanel.SetActive(false);//节省代码，直接全关
        OpenActionPanel();
        EnableAllNodeButtons();
    }

    public void DisableAllNodeButtons()
    {
        foreach (Button button in Buttons)
        {
            button.enabled = false;
        }
    }

    public void EnableAllNodeButtons()
    {
        foreach (Button button in Buttons)
        {
            button.enabled = true;
        }
    }

    public void EnableEnemyNodeButtons()
    {
        for(int i = 6;i < 12; i++)
        {
            Buttons[i].enabled = true;
        }
    }

    public void DisableAllArrows()
    {
        foreach (GameObject i in Arrows)
        {
            i.gameObject.SetActive(false);
        }
    }

    public void OnAttackButtonClick()
    {
        //禁止按别的node了
        DisableAllNodeButtons();
        //把所有东西复位
        CloseActionPanel();
        if(dataofNodes.SelectedPNodeCode != 12)
            Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponent<Nodes>().isSelected = false;
        if (dataofNodes.SelectedENodeCode != 12)
        {
            Spawner.nodeDictionary[dataofNodes.SelectedENodeCode].GetComponent<Nodes>().isSelected = false;
            Circles[dataofNodes.SelectedENodeCode].gameObject.SetActive(false);
        }
        dataofNodes.isPNodesSelected = new bool[6] { false, false, false, false, false, false };
        dataofNodes.isENodesSelected = new bool[6] { false, false, false, false, false, false };
        dataofNodes.anyESelected = false;
        dataofNodes.SelectedENodeCode = 12;
        //读取技能并且显示在面板里面

        //if （isKnight） {
        int SkillsCount = GetPublicMethodCount(typeof(Knight));

        //待添加
        //}
        //打开技能面板

        OpenSkillsPanel();
        enemySkillsPanel.gameObject.SetActive(false);
    }

    public void OnDefenseButtonClick()
    {
        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<ActioninBattleManager>().GonnaDefense();
        CloseActionPanel();
        DisplayShield();
    }

    public void DisplayShield()
    {
        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponent<Nodes>().DisplayShield();
    }

    public void DisplaySword()
    {
        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponent<Nodes>().DisplaySword();
    }

    public void DisplayArrow()
    {
        //未曾使用过，出于整齐放了一个在这里，实际上因为方便这个函数在Nodes这个类当中直接声明并使用了
    }

    public void ExitDefense()
    {
        CloseExitDefensePanel();
        OpenActionPanel();
        Destroy(Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].transform.Find("Shield").gameObject);
        //把当前选中的node下面的盾牌弄掉
        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().OnTheDefense = false;
    }

    public void ExitMovement()
    {
        if (dataofNodes.SbisMoving)
        {//属于有人正在确定移动的阶段
            exitMovementPanel.gameObject.SetActive(false);
            actionPanel.gameObject.SetActive(true);
            DisableAllArrows();
            EnableAllNodeButtons();

            dataofNodes.SbisMoving = false;
        }
        else
        {//属于这个人已经确定了移动的状态
            CloseExitMovementPanel();
            EnableAllNodeButtons();
            OpenActionPanel();
            Destroy(Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].transform.Find("Arrow").gameObject);
            //把当前选中的node下面的箭头弄掉
            Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().OnTheMovement = false;

            foreach (GameObject i in Arrows)
            {
                i.SetActive(false);
            }//关掉所有的指示箭头
        }
    }

    public void ExitAttack()
    {
        CloseExitAttackPanel();
        OpenActionPanel();
        Destroy(Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].transform.Find("Sword").gameObject);
        //把当前选中的node下面的剑弄掉
        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().OnTheAttack = false;
    }

    public void OpenExitDefensePanel()
    {
        exitDefensePanel.gameObject.SetActive(true);
    }

    public void CloseExitDefensePanel()
    {
        exitDefensePanel.gameObject.SetActive(false);
        EnableAllNodeButtons();
    }

    public void OpenExitMovementPanel()
    {
        exitMovementPanel.gameObject.SetActive(true);
    }

    public void CloseExitMovementPanel()
    {
        exitMovementPanel.gameObject.SetActive(false);
    }

    public void OpenExitAttackPanel()
    {
        exitAttackPanel.gameObject.SetActive(true);
    }

    public void CloseExitAttackPanel()
    {
        exitAttackPanel.gameObject.SetActive(false);
        EnableAllNodeButtons();
    }

    public void OnMoveButtonClick()
    {
        DisableAllNodeButtons();

        dataofNodes.SbisMoving = true;//标志着有人正在确定移动
        actionPanel.gameObject.SetActive(false);
        exitMovementPanel.gameObject.SetActive(true);

        ReadyForMovement();
    }

    public void ShowMoveTargetPoint()
    {
        Arrows[Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().TargetPosition].SetActive(true);
        //显示现在选择的这个人要去哪个位置
    }

    public void ShowAtkRange()
    {
        //读取攻击范围
        //foreach (int i in AtkRange)
        //{
        //    ArrowsofEnemies[i].SetActive(true);
        //}
        //显示现在选择的这个人要去哪个位置
    }

    public void OnSkillClick()
    {
        dataofNodes.SbisAttacking = true;
        //if 是指定敌人的技能 {
        //显示所有的敌人头上的箭头(待更新)
        OpenArrowsOfEnemies();
        //}

        EnableEnemyNodeButtons();
        dataofNodes.SbisAttacking = true;//记得解除

        //从这里开始等待node那边给反应



        //CloseSkillsPanel();
        //EnableAllNodeButtons();
        //CloseActionPanel();
        //DisplaySword();

        
    }

    public void SkillSelectionSucceeded()
    {
        dataofNodes.SbisAttacking = false;
        CloseArrowsOfEnemies();
        CloseSkillsPanel();
        EnableAllNodeButtons();
        CloseActionPanel();
        DisplaySword();
    }

    public void SkillSelectionFailed()
    {
        dataofNodes.SbisAttacking = false;
        OpenOutOfRangePanel();
        CloseArrowsOfEnemies();
        CloseSkillsPanel();
        EnableAllNodeButtons();
        CloseActionPanel();
    }

    public void OpenArrowsOfEnemies()
    {
        foreach (GameObject i in ArrowsofEnemies)
        {
            i.SetActive(true);
        }
    }

    public void CloseArrowsOfEnemies()
    {
        foreach (GameObject i in ArrowsofEnemies)
        {
            i.SetActive(false);
        }
    }


    public void ReadyForMovement()
    {

        switch (dataofNodes.SelectedPNodeCode)
        {//这里面根据选择的哪个node，直接设置了能去哪些node
            case 0:
                IisReadyForSelection(1);
                IisReadyForSelection(3);
                break;
            case 1:
                IisReadyForSelection(0);
                IisReadyForSelection(2);
                IisReadyForSelection(4);
                break;
            case 2:
                IisReadyForSelection(1);
                IisReadyForSelection(5);
                break;
            case 3:
                IisReadyForSelection(0);
                IisReadyForSelection(4);
                break;
            case 4:
                IisReadyForSelection(1);
                IisReadyForSelection(3);
                IisReadyForSelection(5);
                break;
            case 5:
                IisReadyForSelection(2);
                IisReadyForSelection(4);
                break;

            default:
                break;
        }

        
    }
    
    public void IisReadyForSelection(int i)
    {//让某个点变为可选
        if (!Spawner.nodeDictionary[i].GetComponent<Nodes>().isPlayerHere)
        {
            Arrows[i].SetActive(true);
            Buttons[i].enabled = true;
        }
    }

    //下面三个放在按钮用来更改当前人物目前选中的技能

    public void SetSkillCode0()
    {
        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().SkillCode = 0;
    }

    public void SetSkillCode1()
    {
        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().SkillCode = 1;
    }

    public void SetSkillCode2()
    {
        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().SkillCode = 2;
    }

    public int GetPublicMethodCount(Type targetType)
    {//了解一下这个人物有多少个技能
        MethodInfo[] publicMethods = targetType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

        return publicMethods.Length;
    }

    public void ShowGameWonPanel()
    {
        gameWonPanel.SetActive(true);
    }

    public void ShowGameLosePanel()
    {
        gameLosePanel.SetActive(true);
    }

    public void UpdateRoundText()
    {
        RoundText.text = _battleManager.RoundCount.ToString();
    }

    public void CloseAllCircles()
    {
        foreach (Image circle in Circles)
        {
            circle.gameObject.SetActive(false);
        }
    }
}
