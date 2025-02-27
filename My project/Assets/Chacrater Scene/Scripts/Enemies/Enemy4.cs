using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy4 : MonoBehaviour
{
    //双刃劫掠者

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1(int position)//双刃斩击（使用该技能时触发两次）
    {
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.7f), enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        enemyProperty.BeDamaged(dmg);
    }

    public void skill2(int position)//破甲斩
    {
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.5f), enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        enemyProperty.BeDamaged(dmg);
        Buff armorBreakBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.PR -= 20,
            removeEffect: (character) => character.PR -= 20
            );
        Buff.AddBuff(enemyProperty, armorBreakBuff);
    }
}
