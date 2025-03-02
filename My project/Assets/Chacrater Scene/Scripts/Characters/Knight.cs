using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Knight : MonoBehaviour
{
    CharacterProperty charaterProperty;
    ActioninBattleManager actioninBattleManager;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
        actioninBattleManager = this.GetComponent<ActioninBattleManager>();
    }

    public void Skills(int skillCode,int position)
    {//技能在这里生效，这两个参数之后有逻辑传入
        switch (skillCode)
        {
            case 0:
                skill1(position);
                break;
            case 1:
                skill2Apply(position);
                break;
            case 2:
                skill3();
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
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
        dmg = actioninBattleManager.AtkJudger(dmg);
        enemyProperty.BeDamaged(dmg);
    }

    public bool skill1Range(int position)
    {
        if(position > 6 && position < 9 || position < 12 && Spawner.nodeDictionary[position - 3] != null)
        {
             //skill1(position);
            return true;
        }
        return false;
    }

    public List<int> skill2(int position)//攻击前方一排敌人
    {
        GameObject node;
        Nodes _node;
        List<int> list = new List<int>();
        if(position < 9)
        {
            for (int i = 6; i < 9; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out node);
                _node = node.GetComponent<Nodes>();
                if (!_node.isPlayerHere)
                    continue;
                CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
                list.Add(enemyProperty.Position);
            }
        }
        else if(position < 10) 
        {
            for (int i = 9; i < 12; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out node);
                _node = node.GetComponent<Nodes>();
                if (!_node.isPlayerHere)
                    continue;
                CharacterProperty enemyProperty = node.GetComponent<CharacterProperty>();
                list.Add(enemyProperty.Position);
            }
        }
        return list;
    }

    public void skill2Apply(int position)
    {
        int dmg;
        GameObject node;
        Nodes _node;
        CharacterProperty enemyProperty;
        if (position < 9) 
        {
            for (int i = 6; i < 9; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out node);
                _node = node.GetComponent<Nodes>();
                if (!_node.isPlayerHere)
                    continue;
                enemyProperty = node.GetComponentInChildren<CharacterProperty>();
                dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty.PR, charaterProperty.CritVaule,
                 charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
                dmg = actioninBattleManager.AtkJudger(dmg);
                enemyProperty.BeDamaged(dmg);
            }
        }
        else if (position < 10)
        {
            for (int i = 9; i < 12; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out node);
                _node = node.GetComponent<Nodes>();
                if (!_node.isPlayerHere)
                    continue;
                enemyProperty = node.GetComponent<CharacterProperty>();
                dmg = PropertyCalculator.DamageValueCalculation((int)(charaterProperty.ATK * 0.6f), enemyProperty.PR, charaterProperty.CritVaule,
                 charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
                dmg = actioninBattleManager.AtkJudger(dmg);
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
                return true;
            }
            if (position > 6 && position < 9 || position < 12 && Spawner.nodeDictionary[i] != null)
                continue;
        }
        return false;
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

    public bool skill3Range()
    {
        return true;
    }

}