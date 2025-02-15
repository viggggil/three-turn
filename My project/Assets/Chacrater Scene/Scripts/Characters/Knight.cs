using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : MonoBehaviour
{
    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }
    public void skill1(GameObject enemy)//攻击一个敌人
    {
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
    }

    public void skill2(GameObject enemy1,GameObject enemy2,GameObject enemy3)//攻击前方三个敌人
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
    }

    public void skill3()
    {
        charaterProperty.PR = (int)(charaterProperty.PR * 1.6f);
        charaterProperty.MR = (int)(charaterProperty.MR * 1.2f);
    }
}
