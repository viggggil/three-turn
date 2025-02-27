using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    //敌人中的术士

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1(GameObject enemy1, GameObject enemy2, GameObject enemy3)
    {
        CharacterProperty enemyProperty1 = enemy1.GetComponent<CharacterProperty>();
        CharacterProperty enemyProperty2 = enemy2.GetComponent<CharacterProperty>();
        CharacterProperty enemyProperty3 = enemy3.GetComponent<CharacterProperty>();
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty1.PR, charaterProperty.CritVaule,
            enemyProperty1.CritResis, charaterProperty.CritDMGRate, enemyProperty1.CritDMGResisRate);
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty2.PR, charaterProperty.CritVaule,
            enemyProperty2.CritResis, charaterProperty.CritDMGRate, enemyProperty2.CritDMGResisRate);
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty3.PR, charaterProperty.CritVaule,
            enemyProperty3.CritResis, charaterProperty.CritDMGRate, enemyProperty3.CritDMGResisRate);
        Buff BurnBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.HP -= 30,
            removeEffect: null
            );
        Buff.AddBuff(enemyProperty1, BurnBuff);
        Buff.AddBuff(enemyProperty2, BurnBuff);
        Buff.AddBuff(enemyProperty3, BurnBuff);
    }

    public void skill2()
    {
        Buff ATKplusBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.ATK += 30,
            removeEffect: (character) => character.ATK -= 30
            );
        Buff.AddBuff(charaterProperty, ATKplusBuff);
    }
}
