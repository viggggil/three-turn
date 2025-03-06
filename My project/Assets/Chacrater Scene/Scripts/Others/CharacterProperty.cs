using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProperty : MonoBehaviour
{
    [SerializeField] public int maxHealth;//��ʼ����ֵ
    [SerializeField] public  int health;//����ֵ
    [SerializeField] private int code;//����
    [SerializeField] private int attackPower;//������
    [SerializeField] private int defensePower;//����ֵ
    [SerializeField] private int physicalResistivity;//��������
    [SerializeField] private int magicalResistivity;//ħ������
    [SerializeField] private int minRandomSpeed;//��С�ٶ�
    [SerializeField] private int maxRandomSpeed;//����ٶ�
    [SerializeField] private int originalSpeed;//����ٶ�
    [SerializeField] private int speedThisRound;//�����ٶ�(����)
    [SerializeField] private int critHitValue;//����ֵ
    [SerializeField] private float critDMGRate;//�����˺���
    [SerializeField] private int position;//λ��
    [SerializeField] private int targetPosition;//���ѡ���Ҫ�ƶ�����λ��
    [SerializeField] private bool ontheDefense;//���ڷ���
    [SerializeField] private bool ontheMovement;//�����ƶ�
    [SerializeField] private bool ontheAttack;//���ڹ���
    [SerializeField] private bool isDying;//������ȥ
    [SerializeField] private bool isEnemy;//�ǵ���
    [SerializeField] public int SerialNumber;//���
    [SerializeField] public int atkTargetPosition;//���ѡ���Ҫ������λ��
    [SerializeField] private int skillCode;//Ҫʹ�õļ��ܵı��
    [SerializeField] private int residualStunRound = 0;//��Ҫѣ�ζ��ٸ��غ�
    [SerializeField] public int profession;//ְҵ 0��ʿ1������2��ʦ3��ʦ

    [SerializeField] public GameObject HPBar;
    [SerializeField] public GameObject SpeedPad;
    [SerializeField] public Transform PlayerPosition;
    [SerializeField] public Image _HPbar;

    public ActioninBattleManager AIBManager;

    public GameObject temp;

    GameObject newBar;
    GameObject newPad;

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
    public bool IsEnemy { get; set; }

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
        IsEnemy = isEnemy;
        PlayerPosition = gameObject.transform;

        AIBManager = GetComponent<ActioninBattleManager>();
    }

    private void Start()
    {
        temp = GameObject.Find("GameData");
        if(!temp) CreateBar();
        if(!temp) CreateSpeedPad();
    }

    private void Update()
    {
        if (!temp) _HPbar.fillAmount = Mathf.Lerp(_HPbar.fillAmount, (float)HP / (float)Health, Time.deltaTime * 5f);
    }

    public void CreateBar()
    {
        Vector3 thisBarPosition = new Vector3(PlayerPosition.position.x, PlayerPosition.position.y + 2f, PlayerPosition.position.z);
        newBar = Instantiate(HPBar, thisBarPosition, Quaternion.identity);
        newBar.transform.SetParent(gameObject.transform);

        HealthBar healthbar = newBar.GetComponent<HealthBar>();
        _HPbar = healthbar.fillAmountImage;

        UpdateHPText();
    }

    public void CreateSpeedPad()
    {
        Vector3 thisPadPosition = new Vector3(PlayerPosition.position.x, PlayerPosition.position.y - 0.15f, PlayerPosition.position.z);
        newPad = Instantiate(SpeedPad, thisPadPosition, Quaternion.identity);
        newPad.transform.SetParent(gameObject.transform);


        UpdateSpeedText();
    }

    public void UpdateHPText()
    {
        string HPtext = HP.ToString();
        string MaxHPtext = maxHealth.ToString();
        string FinalText = HPtext + " / " + MaxHPtext;
        newBar.GetComponentInChildren<Text>().text = FinalText;
    }

    public void UpdateSpeedText()
    {//getcomponent优先取先拿到的component
        newPad.GetComponentInChildren<Text>().text = SpeedThisRound.ToString();
    }

    public void BeDamaged(int damage)
    {
        this.HP -= damage;

        AIBManager.PlayHurtAnimation();
        UpdateHPText();
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
