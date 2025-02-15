using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }
    public void skill1(GameObject enemy)//攻击一个敌人
    {
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        if(enemyProperty.isMarked)
            PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 1.6f), enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        else
            PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
    }

    public void skill2(GameObject enemy1,GameObject enemy2)//攻击前方两个敌人
    {
        CharacterProperty enemyProperty1 = enemy1.GetComponent<CharacterProperty>();
        CharacterProperty enemyProperty2 = enemy2.GetComponent<CharacterProperty>();
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.8f), enemyProperty1.MR, charaterProperty.CritVaule,
            enemyProperty1.CritResis, charaterProperty.CritDMGRate, enemyProperty1.CritDMGResisRate);
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.5f), enemyProperty2.MR, charaterProperty.CritVaule,
            enemyProperty2.CritResis, charaterProperty.CritDMGRate, enemyProperty2.CritDMGResisRate);
    }

    public void skill3(GameObject enemy)
    {
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        Buff MarkBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Buff,
            applyEffect: (character) => character.isMarked = true,
            removeEffect: (character) => character.isMarked = false
        );
        Buff.AddBuff(enemyProperty, MarkBuff);
    }
}
