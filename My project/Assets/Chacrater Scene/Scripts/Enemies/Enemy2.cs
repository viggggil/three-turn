using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //敌人中的辅助

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1(int position)//给队友加攻击力
    {
        GameObject teammate;
        Spawner.nodeDictionary.TryGetValue(position, out teammate);
        CharacterProperty teammateProperty = teammate.GetComponent<CharacterProperty>();
        teammateProperty.HP += charaterProperty.ATK;
    }

    public void skill2(int position)//眩晕敌人
    {
        GameObject enemy;
        Spawner.nodeDictionary.TryGetValue(position, out enemy);
        CharacterProperty enemyProperty = enemy.GetComponent<CharacterProperty>();
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
