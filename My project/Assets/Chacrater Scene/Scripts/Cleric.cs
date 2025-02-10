using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : MonoBehaviour
{
    CharaterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharaterProperty>();
    }

    public void skill1(GameObject teammate)
    {
        CharaterProperty teammateProperty = teammate.GetComponent<CharaterProperty>();
        teammateProperty.HP += charaterProperty.ATK;
    }

    public void skill2(GameObject teammate1, GameObject teammate2, GameObject teammate3)
    {
        CharaterProperty teammateProperty1 = teammate1.GetComponent<CharaterProperty>();
        CharaterProperty teammateProperty2 = teammate2.GetComponent<CharaterProperty>();
        CharaterProperty teammateProperty3 = teammate3.GetComponent<CharaterProperty>();
        teammateProperty1.HP += (int)(charaterProperty.ATK * 0.6f);
        teammateProperty2.HP += (int)(charaterProperty.ATK * 0.6f);
        teammateProperty3.HP += (int)(charaterProperty.ATK * 0.6f);
    }
}
