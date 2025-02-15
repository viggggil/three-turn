using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    //×Ô±¬±ø

    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void skill1(GameObject[] enemy)
    {
        CharacterProperty[] enemyProperty = new CharacterProperty[6];
        for (int i = 0; i < enemy.Length; i++)
        {
            enemyProperty[i] = enemy[i].GetComponent<CharacterProperty>();
            PropertyCalculator.DamageValueCalculation(charaterProperty.ATK, enemyProperty[i].PR,
                charaterProperty.CritVaule, enemyProperty[i].CritResis, charaterProperty.CritDMGRate, enemyProperty[i].CritDMGResisRate);
        }
    }
}
