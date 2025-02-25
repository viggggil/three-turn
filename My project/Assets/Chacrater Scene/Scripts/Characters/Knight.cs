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
    public void skill1(int position)//攻击一个敌人
    {
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        enemyProperty.BeDamaged(dmg);
    }

    public bool skill1Range(int position)
    {
        if(position > 6 && position < 9 || position < 12 && Spawner.nodeDictionary[position - 3] != null)
        {
            skill1(position);
            return true;
        }
        return false;
    }

    public void skill2(int position)//攻击前方一排敌人
    {
        GameObject enemy;
        int dmg;
        if(position < 9)
        {
            for (int i = 6; i < 9; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out enemy);
                if (enemy == null)
                    continue;
                CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
                dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
                enemyProperty.BeDamaged(dmg);
            }
        }
        else if(position < 10) 
        {
            for (int i = 9; i < 12; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out enemy);
                if (enemy == null)
                    continue;
                CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
                dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty.PR, charaterProperty.CritVaule,
                enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
                enemyProperty.BeDamaged(dmg);
            }
        }
    }

    public bool skill2Range(int position)
    {
        for (int i = 6; i < 10; i++)
        {
            if(i == 9)
            {
                skill2(position);
                return true;
            }
            if (position > 6 && position < 9 || position < 12 && Spawner.nodeDictionary[i] != null)
                continue;
        }
        return false;
    }

    public bool skill3()
    {
        Buff DefendBuff = new Buff(
           name: "",
           duration: 2,
           buffType: BuffType.Buff,
           applyEffect: (character) => { character.PR = (int)(character.PR * 1.6f);character.MR = (int)(character.MR * 1.2f); },
           removeEffect: (character) => { character.PR = (int)(character.PR / 1.6f); character.MR = (int)(character.MR / 1.2f); }
        );
        Buff.AddBuff(charaterProperty, DefendBuff);
        return true;
    }

}