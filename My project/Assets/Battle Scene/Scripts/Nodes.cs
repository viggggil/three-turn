using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    GameObject actionPanel;
    public GameObject enemySkillsPanel;
    GameObject BattleUIManager;
    public GameObject Bars;

    public DataofNodes dataofNodes;

    BattleUIManager battleUIManager;

    [SerializeField] int nodeCode;

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

    public void OnPlayerNodeClick()
    {//node被点击时候的方法
        if (isPlayerHere)
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
                    battleUIManager.Circles[dataofNodes.SelectedPNodeCode].gameObject.SetActive(false);//关别人的
                    dataofNodes.isPNodesSelected[dataofNodes.SelectedPNodeCode] = false;
                    battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
                    dataofNodes.isPNodesSelected[nodeCode] = true;
                    isSelected = true;
                }
            }

            dataofNodes.SelectedPNodeCode = nodeCode;




            //for (int i = 0; i < 12; i++)
            //{

            //    if (dataofNodes.isNodesSelected[i])
            //    {//某一个已经被选中了
            //        othersSelected = true;
            //        if(i == nodeCode)
            //        {//选的就是它
            //            battleUIManager.CloseActionPanel();
            //            battleUIManager.Circles[nodeCode].gameObject.SetActive(false);//关自己的
            //            dataofNodes.isNodesSelected[nodeCode] = false;
            //            isSelected = false;
            //            dataofNodes.anySelected = false;
            //        }
            //        else
            //        {//选的不是它
            //            battleUIManager.Circles[i].gameObject.SetActive(false);//关别人的
            //            dataofNodes.isNodesSelected[i] = false;
            //            battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
            //            dataofNodes.isNodesSelected[nodeCode] = true;
            //            isSelected = true;
            //        }
            //    }
            //}


            //if (!othersSelected)
            //{//每个都没有被选中
            //    battleUIManager.OpenActionPanel();
            //    battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
            //    dataofNodes.isNodesSelected[nodeCode] = true;
            //    isSelected = true;
            //    dataofNodes.anySelected = true;
            //}


        }
    }


    public void OnEnemyNodeClick()
    {//node被点击时候的方法
        if (isEnemyHere)
        {
            if (!dataofNodes.anyESelected)
            {
                battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
                dataofNodes.isENodesSelected[nodeCode-6] = true;
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
                    dataofNodes.isENodesSelected[nodeCode-6] = false;
                    isSelected = false;
                    dataofNodes.anyESelected = false;
                    enemySkillsPanel.gameObject.SetActive(false);
                }
                else
                {
                    battleUIManager.Circles[dataofNodes.SelectedENodeCode].gameObject.SetActive(false);//关别人的
                    dataofNodes.isENodesSelected[dataofNodes.SelectedENodeCode-6] = false;
                    battleUIManager.Circles[nodeCode].gameObject.SetActive(true);//开自己的
                    dataofNodes.isENodesSelected[nodeCode-6] = true;
                    isSelected = true;
                }
            }

            dataofNodes.SelectedENodeCode = nodeCode;



        }
    }


}
