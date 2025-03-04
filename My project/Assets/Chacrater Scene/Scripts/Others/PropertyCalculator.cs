using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyCalculator : MonoBehaviour
{
    public static bool CriticalHitRateCalculation(int critVaule)
    {
        int randomValue = Random.Range(0, 100);
        return randomValue < critVaule;
    }

    public static float CriticalHitDamageCalculation(bool isCrit,float critDMGRate)
    {
        return isCrit ? critDMGRate : 1f;
    }

    public static int DamageValueCalculation(int ATK, int Risis, int critValue, float critDMGRate, int defensePower,bool ontheDefense)
    {
        int damageValue = (int)((int)(ATK * (100 -  Risis) / 100f) * CriticalHitDamageCalculation(CriticalHitRateCalculation(critValue), critDMGRate));
        //return ontheDefense ? damageValue : damageValue - defensePower;
        if (damageValue - defensePower >= 0)
        {
            return ontheDefense ? damageValue : damageValue - defensePower;
        }
        else
        {
            return 0;
        }
    }
}
