using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //只会蓄力攻击

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1()//蓄力
    {
        Buff Charging = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: null,
            removeEffect: (character) => character.isCharge = true
            );
        Buff.AddBuff(charaterProperty, Charging);
    }

    public void skill2(int position)//攻击
    {

        if (charaterProperty.isCharge)
        {
            GameObject enemy;
            Spawner.nodeDictionary.TryGetValue(position, out enemy);
            CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
            int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
            enemyProperty.BeDamaged(dmg);
            charaterProperty.isCharge = false;
        }
    }
}
