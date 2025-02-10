using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : MonoBehaviour
{
    CharaterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharaterProperty>();
    }
    public void skill1(GameObject enemy)//攻击一个敌人
    {
        CharaterProperty enemyProperty = enemy.GetComponent<CharaterProperty>();
        PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
    }

    public void skill2(GameObject enemy1,GameObject enemy2,GameObject enemy3)//攻击前方三个敌人
    {
        CharaterProperty enemyProperty1 = enemy1.GetComponent<CharaterProperty>();
        CharaterProperty enemyProperty2 = enemy2.GetComponent<CharaterProperty>();
        CharaterProperty enemyProperty3 = enemy3.GetComponent<CharaterProperty>();
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty1.PR, charaterProperty.CritVaule,
            enemyProperty1.CritResis, charaterProperty.CritDMGRate, enemyProperty1.CritDMGResisRate);
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty2.PR, charaterProperty.CritVaule,
            enemyProperty2.CritResis, charaterProperty.CritDMGRate, enemyProperty2.CritDMGResisRate);
        PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty3.PR, charaterProperty.CritVaule,
            enemyProperty3.CritResis, charaterProperty.CritDMGRate, enemyProperty3.CritDMGResisRate);
    }

    public void skill3()
    {
        charaterProperty.PR = (int)(charaterProperty.PR * 1.6f);
        charaterProperty.MR = (int)(charaterProperty.MR * 1.2f);
    }
}
