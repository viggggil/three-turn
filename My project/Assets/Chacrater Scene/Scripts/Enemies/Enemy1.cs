using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //½ÙÂÓÕßÊ×Áì

    CharacterProperty charaterProperty;
    ActioninBattleManager actioninBattleManager;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
        actioninBattleManager = this.GetComponent<ActioninBattleManager>();
    }

    public void skill1(int position)
    {
        
    }

    public void skill2(int position)//¹¥»÷
    {

        if (charaterProperty.isCharge)
        {
            GameObject node;
            Spawner.nodeDictionary.TryGetValue(position, out node);
            CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
            int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
                 charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
            dmg = actioninBattleManager.AtkJudger(dmg);
            enemyProperty.BeDamaged(dmg);
            charaterProperty.isCharge = false;
        }
    }
}
