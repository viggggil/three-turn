using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //异端牧师

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1(int position)//给队友加buff
    {
        GameObject teammate;
        Spawner.nodeDictionary.TryGetValue(position, out teammate);
        CharacterProperty teammateProperty = teammate.GetComponent<CharacterProperty>();
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
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        charaterProperty.HP += (int)(dmg * 0.2f);
        enemyProperty.BeDamaged(dmg);
    }

}
