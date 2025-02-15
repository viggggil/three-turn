using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //µÐÈËÖÐµÄ¸¨Öú

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1(GameObject teammate)
    {
        CharacterProperty teammateProperty = teammate.GetComponent<CharacterProperty>();
        teammateProperty.HP += charaterProperty.ATK;
    }

    public void skill2(GameObject enemy)
    {
        Buff dizzy = new Buff(
            name: "",
            duration: 2,
            buffType: BuffType.Debuff,
            applyEffect: (character) => character.isdizzy = true,
            removeEffect: (character) => character.isdizzy = false
            );
        Buff.AddBuff(charaterProperty, dizzy);
    }
}
