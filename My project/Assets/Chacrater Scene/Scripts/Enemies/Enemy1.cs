using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //Ö»»áÐîÁ¦¹¥»÷

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1()
    {
        Buff Charging = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.isCharge = true,
            removeEffect: (character) => character.isCharge = false
            );
        Buff.AddBuff(charaterProperty, Charging);
    }

    public void skill2(int position)
    {

        if (charaterProperty.isCharge)
        {
            GameObject enemy;
            Spawner.nodeDictionary.TryGetValue(position, out enemy);
            CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
            int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
            enemyProperty.BeDamaged(dmg);
        }
    }
}
