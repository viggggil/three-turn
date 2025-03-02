using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class wizard : MonoBehaviour
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
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.MR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
        dmg = actioninBattleManager.AtkJudger(dmg);
        enemyProperty.BeDamaged(dmg);
        Buff BurnBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.HP -= 10,
            removeEffect: null
            );
        Buff.AddBuff(enemyProperty, BurnBuff);
    }

    public void skill2(int position)//提高队友的魔抗，持续两回合
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty teammateProperty = node.GetComponentInChildren<CharacterProperty>();
        Buff MRBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Buff,
            applyEffect: (character) => character.MR += 40,
            removeEffect: (character) => character.MR -= 40
        );
        Buff.AddBuff(teammateProperty, MRBuff);
    }

    public void skill3(int position)
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        Buff FreezeBuff = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.isdizzy = true,
            removeEffect: (character) => character.isdizzy = false
        );
        Buff.AddBuff(enemyProperty, FreezeBuff);
    }

    public bool skillRange()
    {
        return true;
    }
}
