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

    public DataofNodes dataofNodes;
    
    public List<Image> Circles;
    public List<Button> Buttons;
    public List<GameObject> Arrows;


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

    public void ExitDefense()
    {
        CloseExitDefensePanel();
        OpenActionPanel();
        Destroy(Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].transform.Find("Shield").gameObject);
        //把当前选中的node下面的盾牌弄掉
        Spawner.nodeDictionary[dataofNodes.SelectedPNodeCode].GetComponentInChildren<CharacterProperty>().OnTheDefense = false;
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

    public void DisplayArrows()
    {
        DisableAllNodeButtons();

        switch (dataofNodes.SelectedPNodeCode)
        {
            case 0:
                if (!Spawner.nodeDictionary[1].GetComponent<Nodes>().isPlayerHere)
                {
                    Arrows[1].SetActive(true);
                    Buttons[1].enabled = true;
                }
                if (!Spawner.nodeDictionary[3].GetComponent<Nodes>().isPlayerHere)
                {

                }
                break;

            default:
                break;
        }

        
    }
    
}
