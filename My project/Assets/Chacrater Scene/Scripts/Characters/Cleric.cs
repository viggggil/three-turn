using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : MonoBehaviour
{
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

    public void skill2(GameObject teammate1, GameObject teammate2, GameObject teammate3)
    {
        CharacterProperty teammateProperty1 = teammate1.GetComponent<CharacterProperty>();
        CharacterProperty teammateProperty2 = teammate2.GetComponent<CharacterProperty>();
        CharacterProperty teammateProperty3 = teammate3.GetComponent<CharacterProperty>();
        teammateProperty1.HP += (int)(charaterProperty.ATK * 0.6f);
        teammateProperty2.HP += (int)(charaterProperty.ATK * 0.6f);
        teammateProperty3.HP += (int)(charaterProperty.ATK * 0.6f);
    }
}
