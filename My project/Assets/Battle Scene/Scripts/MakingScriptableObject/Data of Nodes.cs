using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "DataofNodes", menuName = "nodesData", order = 2)]
public class DataofNodes : ScriptableObject
{
    public bool[] isPNodesSelected;
    public bool[] isENodesSelected;

    public bool anyPSelected;
    public bool anyESelected;

    public int SelectedPNodeCode;
    public int SelectedENodeCode;

    private void OnEnable()
    {
        isPNodesSelected = new bool[6] { false, false, false, false, false, false };
        isENodesSelected = new bool[6] { false, false, false, false, false, false };
        anyPSelected = false;
        anyESelected = false;
        SelectedPNodeCode = 12;
        SelectedENodeCode = 12;
    }
}
