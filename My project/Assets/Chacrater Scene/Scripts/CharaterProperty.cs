using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CharaterProperty : MonoBehaviour
{
    [SerializeField] private int maxHealth;//初始生命值
    [SerializeField] private int attackPower;//攻击力
    [SerializeField] private int physicalResistivity;//物理抗性
    [SerializeField] private int magicalResistivity;//魔法抗性
    [SerializeField] private int speed;//速度
    [SerializeField] private int criticalHitValue;//暴击值
    [SerializeField] private int criticalHitResistivity;//暴击抵抗
    [SerializeField] private float criticalHitDamageRate;//暴击伤害率
    [SerializeField] private float criticalHitDamageResistivityRate;//暴击伤害抵抗率
    [SerializeField] private int accurateValue;//命中值
    [SerializeField] private int evasiveValue;//闪避值

    public int HP { get; set; }
    public int ATK { get; set; }
    public int PR { get; set; }
    public int MR { get; set; }
    public int Speed { get; set; }
    public int CritVaule { get; set; }
    public int CritResis { get; set; }
    public float CritDMGRate {  get; set; }
    public float CritDMGResisRate { get; set; }

    private void Start()
    {
        HP = maxHealth;
        ATK = attackPower;
        PR = physicalResistivity;
        MR = magicalResistivity;
        Speed = speed;
        CritVaule = criticalHitValue;
        CritResis = criticalHitResistivity;
        CritDMGRate = criticalHitDamageRate;
        CritDMGResisRate = criticalHitDamageResistivityRate;
    }
}
