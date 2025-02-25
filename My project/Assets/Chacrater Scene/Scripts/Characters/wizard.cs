using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class wizard : MonoBehaviour
{
    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public bool skill1(int position)//攻击一个敌人
    {
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.MR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        enemyProperty.BeDamaged(dmg);
        Buff BurnBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.HP -= 10,
            removeEffect: null
            );
        Buff.AddBuff(enemyProperty, BurnBuff);
        return true;
    }

    public bool skill2(int position)//提高队友的魔抗，持续两回合
    {
        GameObject teammate;
        Spawner.nodeDictionary.TryGetValue(position, out teammate);
        CharacterProperty teammateProperty = teammate.GetComponent<CharacterProperty>();
        Buff MRBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Buff,
            applyEffect: (character) => character.MR += 40,
            removeEffect: (character) => character.MR -= 40
        );
        Buff.AddBuff(teammateProperty, MRBuff);
        return true;
    }

    public bool skill3(int position)
    {
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        Buff FreezeBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.isdizzy = true,
            removeEffect: (character) => character.isdizzy = false
        );
        Buff.AddBuff(enemyProperty, FreezeBuff);
        return true;
    }
}
