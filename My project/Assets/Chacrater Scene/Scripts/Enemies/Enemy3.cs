using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy3 : MonoBehaviour
{
    //∫⁄Œ◊ ¶

    CharacterProperty charaterProperty;
    ActioninBattleManager actioninBattleManager;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
        actioninBattleManager = this.GetComponent<ActioninBattleManager>();
    }

    public List<int> skill(int position)
    {
        List<int> list = new List<int>();
        list.Add(position);
        return list;
    }

    //public void skill1(int position)//¡ÈªÍ ¯∏ø
    //{
    //    GameObject node;
    //    Spawner.nodeDictionary.TryGetValue(position, out node);
    //    CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
    //    Buff dizzy = new Buff(
    //        name: "",
    //        duration: 2,
    //        buffType: BuffType.Debuff,
    //        applyEffect: (character) => character.isdizzy = true,
    //        removeEffect: (character) => character.isdizzy = false
    //        );
    //    Buff.AddBuff(enemyProperty, dizzy);
    //}
    public void skill2(int position)//¡ÈªÍ ’∏Ó
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        int dmg = PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty.PR, charaterProperty.CritVaule,
             charaterProperty.CritDMGRate, enemyProperty.DEF, enemyProperty.OnTheDefense);
        dmg = actioninBattleManager.AtkJudger(dmg);
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
