using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CharacterProperty : MonoBehaviour
{
    [SerializeField] private int maxHealth;//初始生命值
    [SerializeField] private int health;//初始生命值
    [SerializeField] private int code;//代码
    [SerializeField] private int attackPower;//攻击力
    [SerializeField] private int defensePower;//防御值
    [SerializeField] private int physicalResistivity;//物理抗性
    [SerializeField] private int magicalResistivity;//魔法抗性
    [SerializeField] private int minSpeed;//最小速度
    [SerializeField] private int maxSpeed;//最大速度
    [SerializeField] private int speedThisRound;//最终速度(本轮)
    [SerializeField] private int criticalHitValue;//暴击值
    [SerializeField] private int criticalHitResistivity;//暴击抵抗
    [SerializeField] private float criticalHitDamageRate;//暴击伤害率
    [SerializeField] private float criticalHitDamageResistivityRate;//暴击伤害抵抗率
    [SerializeField] private int accurateValue;//命中值
    [SerializeField] private int evasiveValue;//闪避值
    [SerializeField] private int position;//位置
    [SerializeField] private int targetPosition;//位置
    [SerializeField] private bool ontheDefense;//正在防御
    [SerializeField] private bool ontheMovement;//正在移动
    [SerializeField] private bool ontheAttack;//正在攻击
    [SerializeField] private int SerialNumber;//编号


    public List<Buff> Buffs { get; private set; }

    public int Health { get; set; }
    public int Code { get; set; }
    public int HP { get; set; }
    public int ATK { get; set; }

    public int DEF { get; set; }
    public int PR { get; set; }
    public int MR { get; set; }
    public int MinSpeed { get; set; }
    public int MaxSpeed { get; set; }
    public int SpeedThisRound { get; set; }
    public int CritVaule { get; set; }
    public int CritResis { get; set; }
    public float CritDMGRate { get; set; }
    public float CritDMGResisRate { get; set; }

    public bool isMarked { get; set; }
    public bool isCharge { get; set; }
    public bool isdizzy { get; set; }

    public bool isCursed { get; set; }

    public int Position { get; set; }

    public int TargetPosition { get; set; }

    public bool OnTheMovement { get; set; }

    public bool OnTheDefense { get; set; }

    public bool OnTheAttack { get; set; }
    private void Awake()
    {
        HP = maxHealth;
        ATK = attackPower;
        DEF = defensePower;
        PR = physicalResistivity;
        MR = magicalResistivity;
        MinSpeed = minSpeed;
        MaxSpeed = maxSpeed;
        SpeedThisRound = speedThisRound;
        CritVaule = criticalHitValue;
        CritResis = criticalHitResistivity;
        CritDMGRate = criticalHitDamageRate;
        CritDMGResisRate = criticalHitDamageResistivityRate;
        isMarked = false;
        isCharge = false;
        isdizzy = false;
        isCursed = false;
        Buffs = new List<Buff>();
        Position = position;
        Code = code;
        Health = health;
        OnTheDefense = ontheDefense;
        OnTheMovement = ontheMovement;
        TargetPosition = targetPosition;
        OnTheAttack = ontheAttack;
    }

    public void BeDamaged(int damage)
    {
        this.HP -= damage;
    }

    public void AddBuff(Buff buff)
    {
        Buffs.Add(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        Buffs.Remove(buff);
    }

    public void UpdateBuffs()
    {
        foreach (var buff in Buffs.ToList())
        {
            buff.Apply(this);
            if (buff.IsExpired())
            {
                RemoveBuff(buff);
            }
        }
    }
}
