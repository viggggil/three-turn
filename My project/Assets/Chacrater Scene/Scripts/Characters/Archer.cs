using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


public class Archer : MonoBehaviour
{
    CharacterProperty charaterProperty;
    ActioninBattleManager actioninBattleManager;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
        actioninBattleManager = this.GetComponent<ActioninBattleManager>();
    }

    public void Skills(int skillCode, int position)
    {//技能在这里生效，这两个参数之后有逻辑传入
        switch (skillCode)
        {
            case 0:
                skill1(position);
                break;
            case 1:
                skill2(position);
                break;
            case 2:
                skill3(position);
                break;
            default:
                break;
        }
    }

    public List<int> skill(int position)
    {
        List<int> list = new List<int>();
        list.Add(position);
        return list;
    }
    public void skill1(int position)//攻击一个敌人
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

    public List<int> skill2Range(int position)//攻击前方两个敌人**
    {
        GameObject node;
        Nodes _node;
        List<int> list = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            Spawner.nodeDictionary.TryGetValue(position % 3 + 6 + 3 * i, out node);
            _node = node.GetComponent<Nodes>();
            if (!_node.isPlayerHere)
                continue;
            CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
            list.Add(enemyProperty.Position);
            
        }
        return list;
    }

    public void skill2(int position)
    {
        position = position % 3 + 6;
        GameObject node;
        CharacterProperty enemyProperty;
        int dmg;
        for (int i = 0; i < 2; i++)
        {
            Spawner.nodeDictionary.TryGetValue(position, out node);
            enemyProperty = node.GetComponentInChildren<CharacterProperty>();
            if (node == null)
                continue;
            dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.8f), enemyProperty.PR, charaterProperty.CritVaule,
                 charaterProperty.CritDMGRate, enemyProperty.DEF,enemyProperty.OnTheDefense);
            enemyProperty.BeDamaged(dmg);
        }

    }

    public bool skill3(int position)
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
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

    public bool skillRange()
    {
        return true;
    }
}
