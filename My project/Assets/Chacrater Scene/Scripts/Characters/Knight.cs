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

    public void skill2(int[] position)//攻击前方三个敌人
    {
        GameObject enemy;
        int dmg;
        for(int i =  0; i < position.Length; i++)
        {
            Spawner.nodeDictionary.TryGetValue(position[i], out enemy);
            CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
            dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty.PR, charaterProperty.CritVaule,
            enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
            enemyProperty.BeDamaged(dmg);
        }
    }

    public void skill3()
    {
        Buff DefendBuff = new Buff(
           name: "",
           duration: 2,
           buffType: BuffType.Buff,
           applyEffect: (character) => { character.PR = (int)(character.PR * 1.6f);character.MR = (int)(character.MR * 1.2f); },
           removeEffect: (character) => { character.PR = (int)(character.PR / 1.6f); character.MR = (int)(character.MR / 1.2f); }
        );
        Buff.AddBuff(charaterProperty, DefendBuff);
    }
}
