using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] GameObject actionPanel;
    [SerializeField] GameObject skillsPanel;

    public DataofNodes dataofNodes;
    
    public List<Image> Circles;
    public List<Button> Buttons;


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

    public void DisableAllButtons()
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

    public void OnAttackButtonClick()
    {
        //禁止按别的node了
        DisableAllButtons();
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
    }
}
