using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] GameObject actionPanel;
    [SerializeField] GameObject skillsPanel;
    [SerializeField] GameObject enemySkillsPanel;
    [SerializeField] GameObject exitDefensePanel;
    [SerializeField] GameObject exitAttackPanel;
    [SerializeField] GameObject exitMovementPanel;
    [SerializeField] GameObject exitMovementPanelAfter;

    public DataofNodes dataofNodes;
    
    public List<Image> Circles;
    public List<Button> Buttons;
    public List<GameObject> Arrows;
    public List<GameObject> ArrowsofEnemies;


    private void Awake()
    {
        List<Image> list = new List<Image>();
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
        skillsPanel.SetActive(true);
        //导入技能
    }

    public void CloseSkillsPanel()
    {
        skillsPanel.SetActive(false);
        OpenActionPanel();
        EnableAllButtons();
    }

    public void DisableAllNodeButtons()
    {
        foreach (Button button in Buttons)
        {
            button.enabled = false;
        }
    }

    public void EnableAllButtons()
    {
        foreach (Button button in Buttons)
        {
            button.enabled = true;
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
            EnableAllButtons();

            dataofNodes.SbisMoving = false;
        }
        else
        {//属于这个人已经确定了移动的状态
            CloseExitMovementPanel();
            EnableAllButtons();
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
        EnableAllButtons();
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
        EnableAllButtons();
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
        //读取攻击范围,假设下文的AtkRange就是攻击范围


        List<int> AtkRange = new List<int>();
        AtkRange.Add(0);
        //读取攻击范围
        //foreach (int i in AtkRange)
        //{
        //    ArrowsofEnemies[i].SetActive(true);
        //}

        CloseSkillsPanel();
        EnableAllButtons();
        CloseActionPanel();
        DisplaySword();

        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().OnTheAttack = true;
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
}
