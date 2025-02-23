using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cleric : MonoBehaviour
{
    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1(int position)
    {
        GameObject teammate;
        Spawner.nodeDictionary.TryGetValue(position, out teammate);
        CharacterProperty teammateProperty = teammate.GetComponent<CharacterProperty>();
        teammateProperty.HP += charaterProperty.ATK;
    }

    public void skill2(int position)
    {
        GameObject teammate;
        if(position < 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out teammate);
                CharacterProperty teammateProperty = teammate.GetComponent<CharacterProperty>();
                teammateProperty.HP += (int)(charaterProperty.ATK * 0.6f);
            }
        }
        else
        {
            for (int i = 3; i < 6; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out teammate);
                CharacterProperty teammateProperty = teammate.GetComponent<CharacterProperty>();
                teammateProperty.HP += (int)(charaterProperty.ATK * 0.6f);
            }
        }
    }
}
