using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy3 : MonoBehaviour
{
    //∫⁄Œ◊ ¶

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1(int position)//¡ÈªÍ ¯∏ø
    {
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        Buff dizzy = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.isdizzy = true,
            removeEffect: (character) => character.isdizzy = false
            );
        Buff.AddBuff(enemyProperty, dizzy);
    }
    public void skill2(int position)//¡ÈªÍ ’∏Ó
    {
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        enemyProperty.BeDamaged(dmg);
        Buff dizzy = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.ATK += (int)(dmg * 0.3f),
            removeEffect: (character) => character.ATK -= (int)(dmg * 0.3f)
            );
        Buff.AddBuff(charaterProperty, dizzy);
    }
}
