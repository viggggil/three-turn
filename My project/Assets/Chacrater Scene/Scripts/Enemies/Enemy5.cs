using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{
    //Ω£∂‹ΩŸ¬”’ﬂ

    CharacterProperty charaterProperty;
    ActioninBattleManager actioninBattleManager;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
        actioninBattleManager = this.GetComponent<ActioninBattleManager>();
    }

    public void skill1(int position)//
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
        dmg = actioninBattleManager.AtkJudger(dmg);
        enemyProperty.BeDamaged(dmg);
    }

    public void skill2(int position)
    {
        Buff DefendBuff = new Buff(
           name: "",
           duration: 2,
           buffType: BuffType.Buff,
           applyEffect: (character) => { character.PR = (int)(character.PR * 1.6f); character.MR = (int)(character.MR * 1.2f); },
           removeEffect: (character) => { character.PR = (int)(character.PR / 1.6f); character.MR = (int)(character.MR / 1.2f); }
        );
        Buff.AddBuff(charaterProperty, DefendBuff);
    }
}
