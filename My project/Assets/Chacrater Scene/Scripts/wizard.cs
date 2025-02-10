using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizard : MonoBehaviour
{
    CharaterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharaterProperty>();
    }

    public void skill1(GameObject enemy)//攻击一个敌人
    {
        CharaterProperty enemyProperty = enemy.GetComponent<CharaterProperty>();
        PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.MR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        Buff BurnBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.HP -= 30,
            removeEffect: null
            );
        Buff.AddBuff(enemyProperty, BurnBuff);
    }

    public void skill2(GameObject teammate)//提高队友的魔抗，持续两回合
    {
        CharaterProperty teammateProperty = teammate.GetComponent<CharaterProperty>();
        Buff MRBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Buff,
            applyEffect: (character) => character.MR += 40,
            removeEffect: null
        );
        Buff.AddBuff(teammateProperty, MRBuff);
    }

}
