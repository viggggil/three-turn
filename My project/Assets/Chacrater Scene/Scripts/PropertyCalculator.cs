using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyCalculator : MonoBehaviour
{
    public int CriticalHitRateCalculation(int critVaule, int critResis)
    {
        int CriticalHitRate = critVaule - critResis > 0 
            ? (int)((critVaule - critResis) / (66f + critVaule - critResis) * 100) : 0;
        return CriticalHitRate;
    }

    public bool CriticalHitDeterminationCalculation(int CriticalHitRate)
    {
        int randomValue = Random.Range(0, 100);
        return randomValue < CriticalHitRate;
    }

    public float CriticalHitDamageMultiplierCalculation(float criticalHitDamageRate, float criticalHitDamageResistivityRate)
    {
        float criticalHitDamageMultiplier = criticalHitDamageRate - criticalHitDamageResistivityRate;
        return criticalHitDamageMultiplier;
    }

    public int DamageValueCalculation(int ATK,int Resistivity, int critVaule, int critResis, float criticalHitDamageRate, float criticalHitDamageResistivityRate)
    {
        int damageValue = (int)(ATK * (100f - Resistivity) / 100f); ;
        if (CriticalHitDeterminationCalculation(CriticalHitRateCalculation(critVaule, critResis)))
            damageValue = (int)(damageValue * CriticalHitDamageMultiplierCalculation(criticalHitDamageRate, criticalHitDamageResistivityRate));
        return damageValue;
    }
}
