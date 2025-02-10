using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    CharaterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharaterProperty>();
    }
    public void skill1(GameObject enemy)//攻击一个敌人
    {
        CharaterProperty enemyProperty = enemy.GetComponent<CharaterProperty>();
        if(enemyProperty.Marked)
            PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 1.6f), enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        else
            PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
    }

    public void skill2(GameObject enemy1,GameObject enemy2)//攻击前方两个敌人
    {
        CharaterProperty enemyProperty1 = enemy1.GetComponent<CharaterProperty>();
        CharaterProperty enemyProperty2 = enemy2.GetComponent<CharaterProperty>();
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.8f), enemyProperty1.MR, charaterProperty.CritVaule,
            enemyProperty1.CritResis, charaterProperty.CritDMGRate, enemyProperty1.CritDMGResisRate);
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.5f), enemyProperty2.MR, charaterProperty.CritVaule,
            enemyProperty2.CritResis, charaterProperty.CritDMGRate, enemyProperty2.CritDMGResisRate);
    }

    public void skill3(GameObject enemy)
    {
        CharaterProperty enemyProperty = enemy.GetComponent<CharaterProperty>();
        Buff MarkBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Buff,
            applyEffect: (character) => character.Marked = true,
            removeEffect: (character) => character.Marked = false
        );
        Buff.AddBuff(enemyProperty, MarkBuff);
    }
}
