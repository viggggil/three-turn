using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    GameObject actionPanel;

    Nodes nodes;

    private void Awake()
    {
        actionPanel = GameObject.FindWithTag("ActionPanel");
    }

    

    public void OnPlayerNodeClick()
    {
        if (!actionPanel.activeInHierarchy)
        {
            actionPanel.SetActive(true);
        }
        else if(actionPanel.activeInHierarchy)
        {
            actionPanel.SetActive(false);
        }
    }
}
