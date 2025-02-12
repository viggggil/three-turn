using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    Nodes nodes;

    private void Start()
    {
        nodes = GetComponent<Nodes>();
    }
    public void OnNodeClick()
    {
        if (gameObject.activeInHierarchy && nodes.isPlayerNode)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false && nodes.isPlayerNode);
        }
    }
}
