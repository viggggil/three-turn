using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy6 : MonoBehaviour
{
    //ΩŸ¬”’ﬂπ≠º˝ ÷

    CharacterProperty charaterProperty;
    ActioninBattleManager actioninBattleManager;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
        actioninBattleManager = this.GetComponent<ActioninBattleManager>();
    }

    public void skill1(int position)
    {
        GameObject node;
        int dmg;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        if (enemyProperty.isMarked)
        {
            dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 2.5f), enemyProperty.MR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
            enemyProperty.BeDamaged(dmg);
        }
        else
        {
            dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.MR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
            enemyProperty.BeDamaged(dmg);
        }
    }

    public List<int> skill(int position)
    {
        List<int> list = new List<int>();
        list.Add(position);
        return list;
    }

    //public void skill2(int position)
    //{
    //    GameObject enemy;
    //    Spawner.nodeDictionary.TryGetValue(position, out enemy);
    //    CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
    //    Buff MarkBuff = new Buff(
    //        name: "",
    //        duration: 2,
    //        buffType: BuffType.Buff,
    //        applyEffect: (character) => character.isMarked = true,
    //        removeEffect: (character) => character.isMarked = false
    //    );
    //    Buff.AddBuff(enemyProperty, MarkBuff);
    //}
}
