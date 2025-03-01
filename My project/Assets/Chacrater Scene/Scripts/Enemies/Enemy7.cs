using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy7 : MonoBehaviour
{
    //½ÙÂÓÕß´Ì¿Í

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
    public void skill1(int position)
    {
        GameObject node;
        int dmg;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        if (enemyProperty.isMarked)
        {
            dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 1.6f), enemyProperty.MR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
            enemyProperty.BeDamaged(dmg);
        }
        else
        {
            dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.MR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
            enemyProperty.BeDamaged(dmg);
        }
    }

    //public void skill2(int position)
    //{
    //    Buff Charging = new Buff(
    //        name: "",
    //        duration: 2,
    //        buffType: BuffType.Debuff,
    //        applyEffect: null,
    //        removeEffect: (character) => character.isCharge = true
    //        );
    //    Buff.AddBuff(charaterProperty, Charging);
    //}
}
