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
    public bool skill1(int position)//攻击一个敌人
    {
        GameObject enemy;
        int dmg;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        if (enemyProperty.isMarked)
        {
            dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 1.6f), enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
            enemyProperty.BeDamaged(dmg);
        }
        else
        {
            dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
            enemyProperty.BeDamaged(dmg);
        }
        return true;
    }

    public bool skill2()//攻击前方两个敌人
    {
        int position = charaterProperty.Position;
        GameObject enemy;
        CharacterProperty enemyProperty;
        int dmg;
        for (int i = 0; i < 2; i++)
        {
            Spawner.nodeDictionary.TryGetValue(position % 3 + 6 + 3 * i, out enemy);
            if (enemy == null)
                continue;
            enemyProperty = enemy.GetComponent<CharacterProperty>();
            dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.8f), enemyProperty.MR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
            enemyProperty.BeDamaged(dmg);
        }
        return true;
    }

    public bool skill3(int position)
    {
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        Buff MarkBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Buff,
            applyEffect: (character) => character.isMarked = true,
            removeEffect: (character) => character.isMarked = false
        );
        Buff.AddBuff(enemyProperty, MarkBuff);
        return true;
    }
}
