using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProperty : MonoBehaviour
{
    [SerializeField] public int maxHealth;//初始生命值
    [SerializeField] public  int health;//生命值
    [SerializeField] private int code;//代码
    [SerializeField] private int attackPower;//攻击力
    [SerializeField] private int defensePower;//防御值
    [SerializeField] private int physicalResistivity;//物理抗性
    [SerializeField] private int magicalResistivity;//魔法抗性
    [SerializeField] private int minRandomSpeed;//最小速度
    [SerializeField] private int maxRandomSpeed;//最大速度
    [SerializeField] private int originalSpeed;//最大速度
    [SerializeField] private int speedThisRound;//最终速度(本轮)
    [SerializeField] private int critHitValue;//暴击值
    [SerializeField] private float critDMGRate;//暴击伤害率
    [SerializeField] private int position;//位置
    [SerializeField] private int targetPosition;//点击选择的要移动到的位置
    [SerializeField] private bool ontheDefense;//正在防御
    [SerializeField] private bool ontheMovement;//正在移动
    [SerializeField] private bool ontheAttack;//正在攻击
    [SerializeField] private bool isDying;//正在死去
    [SerializeField] public int SerialNumber;//编号
    [SerializeField] public int atkTargetPosition;//点击选择的要攻击的位置
    [SerializeField] private int skillCode;//要使用的技能的编号
    [SerializeField] private int residualStunRound = 0;//还要眩晕多少个回合
    [SerializeField] public int profession;//职业 0骑士1弓箭手2牧师3法师

    [SerializeField] public GameObject HPBar;
    [SerializeField] public Transform barPosition;
    [SerializeField] public Image _HPbar;

    public ActioninBattleManager AIBManager;

    public GameObject temp;
    public List<Buff> Buffs { get; private set; }

    public int Health { get; set; }
    public int Code { get; set; }
    public int HP { get; set; }
    public int ATK { get; set; }

    public int DEF { get; set; }
    public int ResidualStunRound { get; set; }
    public int PR { get; set; }
    public int MR { get; set; }
    public int MinRandomSpeed { get; set; }
    public int MaxRandomSpeed { get; set; }
    public int OriginalSpeed { get; set; }
    public int SpeedThisRound { get; set; }
    public int CritVaule { get; set; }
    public int CritResis { get; set; }
    public float CritDMGRate { get; set; }

    public bool isMarked { get; set; }
    public bool isCharge { get; set; }
    public bool isdizzy { get; set; }

    public bool isCursed { get; set; }

    public int Position { get; set; }

    public int TargetPosition { get; set; }

    public int AtkTargetPosition { get; set; }

    public int SkillCode { get; set; }

    public bool OnTheMovement { get; set; }

    public bool OnTheDefense { get; set; }

    public bool OnTheAttack { get; set; }
    public bool IsDying { get; set; }

    public int Profession { get; set; }
    private void Awake()
    {
        HP = maxHealth;
        ATK = attackPower;
        DEF = defensePower;
        PR = physicalResistivity;
        MR = magicalResistivity;
        MinRandomSpeed = minRandomSpeed;
        MaxRandomSpeed = maxRandomSpeed;
        SpeedThisRound = speedThisRound;
        CritVaule = critHitValue;
        CritDMGRate = critDMGRate;
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
        IsDying = isDying;
        TargetPosition = targetPosition;
        OnTheAttack = ontheAttack;
        AtkTargetPosition = atkTargetPosition;
        SkillCode = skillCode;
        Profession = profession;
        OriginalSpeed = originalSpeed;
        ResidualStunRound = residualStunRound;
        barPosition = gameObject.transform;

        AIBManager = GetComponent<ActioninBattleManager>();
    }

    private void Start()
    {
        temp = GameObject.Find("GameData");
        if (!temp) this.enabled = false;
        if(!temp)CreateBar();
    }

    private void Update()
    {
        if(!temp)_HPbar.fillAmount = Mathf.Lerp(_HPbar.fillAmount, (float)HP / (float)Health, Time.deltaTime * 5f);
    }

    public void CreateBar()
    {
        Vector3 thisBarPosition = new Vector3(barPosition.position.x, barPosition.position.y + 2f, barPosition.position.z);
        GameObject newBar = Instantiate(HPBar, thisBarPosition, Quaternion.identity);
        newBar.transform.SetParent(gameObject.transform);

        HealthBar healthbar = newBar.GetComponent<HealthBar>();
        _HPbar = healthbar.fillAmountImage;
    }

    public void BeDamaged(int damage)
    {
        this.HP -= damage;

        AIBManager.PlayHurtAnimation();
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
