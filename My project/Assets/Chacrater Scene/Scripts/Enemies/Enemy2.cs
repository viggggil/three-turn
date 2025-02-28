using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //异端牧师

    CharacterProperty charaterProperty;
    ActioninBattleManager actioninBattleManager;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
        actioninBattleManager = this.GetComponent<ActioninBattleManager>();
    }

    public void skill1(int position)//给队友加buff
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty teammateProperty = node.GetComponentInChildren<CharacterProperty>();
        Buff PowerBuff = new Buff(
           name: "",
           duration: 2,
           buffType: BuffType.Buff,
           applyEffect: (character) => { character.ATK = (int)(character.ATK * 1.2f); },
           removeEffect: (character) => { character.ATK = (int)(character.ATK / 1.2f); }
           );
    }

    public void skill2(int position)//灵魂汲取
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
        dmg = actioninBattleManager.AtkJudger(dmg);
        charaterProperty.HP += (int)(dmg * 0.2f);
        enemyProperty.BeDamaged(dmg);
    }

}
