using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy4 : MonoBehaviour
{
    //自爆兵

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1()//一般在几个回合后使用
    {
        for (int i = 3; i < 6; i++)
        {
            GameObject enemy;
            Spawner.nodeDictionary.TryGetValue(i, out enemy);
            if(enemy == null) 
                continue;
            CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
            enemyProperty = enemy.GetComponent<CharacterProperty>();
            PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR,
                charaterProperty.CritVaule, enemyProperty.CritResis, charaterProperty.CritDMGRate, enemyProperty.CritDMGResisRate);
        }
        charaterProperty.HP = 0;
    }
}
