using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8 : MonoBehaviour
{
    //ΩŸ¬”’ﬂ∑® ¶

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

    public void skill1(int position)//¡ÈªÍ ¯∏ø
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty enemyProperty = node.GetComponentInChildren<CharacterProperty>();
        Buff dizzy = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.isdizzy = true,
            removeEffect: (character) => character.isdizzy = false
            );
        Buff.AddBuff(enemyProperty, dizzy);
    }
}
