using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{
    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1(GameObject enemy)
    {
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        int dmg;
        if (enemyProperty.isCursed)
        {
            dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 1.5f), enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        }
        else
        {
            dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        }
        charaterProperty.HP += (int)(dmg * 0.3f);
    }

    public void skill2(GameObject enemy)
    {
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.3f), enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        Buff Curse = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.ATK -= 20,
            removeEffect: (character) => character.ATK += 20
            );
        Buff.AddBuff(enemyProperty, Curse);
    }
}
