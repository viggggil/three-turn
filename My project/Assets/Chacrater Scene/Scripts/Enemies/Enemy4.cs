using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy4 : MonoBehaviour
{
    //Ë«ÈÐ½ÙÂÓÕß

    CharacterProperty charaterProperty;
    ActioninBattleManager actioninBattleManager;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
        actioninBattleManager = this.GetComponent<ActioninBattleManager>();
    }

    public List<int> skill(int position)
    {
        List<int> list = new List<int>();
        list.Add(position);
        return list;
    }
    //public void skill1(int position)//Ë«ÈÐÕ¶»÷
    //{
    //    GameObject node;
    //    Spawner.nodeDictionary.TryGetValue(position, out node);
    //    CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
    //    int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
    //         charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
    //    dmg = actioninBattleManager.AtkJudger(dmg);
    //    enemyProperty.BeDamaged(dmg * 2);
    //}

    public void skill1(int position)//ÆÆ¼×Õ¶
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.5f), enemyProperty.PR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
        dmg = actioninBattleManager.AtkJudger(dmg);
        enemyProperty.BeDamaged(dmg);
        Buff armorBreakBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.PR -= 20,
            removeEffect: (character) => character.PR -= 20
            );
        Buff.AddBuff(enemyProperty, armorBreakBuff);
    }
}
