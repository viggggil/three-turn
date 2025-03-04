using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text infoText;
    public GameObject Cellinfo;

    [Header("体力")]
    public Slider staminaSlider;
    public GameObject staminaBar;
    public Text staminaValue;
    private string _staminaValue;
    public Slider staminaSlider2;
    public GameObject staminaBar2;
    public Text staminaValue2;
    private string _staminaValue2;
    public Slider staminaSlider3;
    public GameObject staminaBar3;
    public Text staminaValue3;
    private string _staminaValue3;

    [Header("生命")]
    public Slider healthSlider;
    public GameObject healthBar;
    public Text healthValue;
    private string _healthValue;
    public Slider healthSlider2;
    public GameObject healthBar2;
    public Text healthValue2;
    private string _healthValue2;
    public Slider healthSlider3;
    public GameObject healthBar3;
    public Text healthValue3;
    private string _healthValue3;

    public Button turnNextButton;
    public UnityEvent turnStart;

    [Header("敌人信息")]
    public GameObject EnemyInfo;
    public Text EnemyName;
    public Text EnemyDescribe;
    public GameObject EnemyTip;

    [Header("按钮")]
    public Button battleSceneButton;
    public GameObject bsButton;
    public Button eventButton1;
    public GameObject eButton1;
    public Text eButton1text;
    public Button eventButton2;
    public GameObject eButton2;
    public Text eButton2text;
    public Button closePanelButton;

    public Event_Map selectedEvent;
    private bool FirstArriveTown=true;

    private bool isPause;
    public GameObject gamePausePanel;
    public GameObject blockPanel;

    public GameManager GameManager;
    public GameObject[] Players;
    public GameObject[] SelectPlayers;

    [Header("玩家头像")]
    public Image PlayerOne;
    public Image PlayerTwo;
    public Image PlayerThree;
    public Sprite[] Professions;
    public Text ProfessionOne;
    public Text ProfessionTwo;
    public Text ProfessionThree;
    public string[] ProfessionNames;

    [Header("对话框")]
    public GameObject Dialogue;
    public Image Speaker;
    public Text SpeakerName;
    public Text dialogue;
    public string[] dialogues;

    public int dialogueIndex = 0;
    public GameObject GameFailedPanel;
    public GameObject PlayerThree_;
    public int PlayerThreeProfession;
    public GameData GameData;
    public bool BossFight = false;

    public Image[] equips;
    public GameObject[] equips_;
    public Sprite Necklace;
    enum CellType
    {
        grass,
        woods,
        forest,
        flowerfield,
        road,
        mountain,
        sea
    }
    private Dictionary<CellType, string> celldic = new Dictionary<CellType, string>()
    {{CellType.grass,"草坪"},
     {CellType.woods,"森林"},
      {CellType.flowerfield,"花地" },
       { CellType.mountain,"山地"},
        {CellType.sea,"海洋" }
    };
    enum EnemyType
    {
        heresyCleric,
        daggerRobber,
        SwordShieldRobber,
        RobberArcher,
        RobberMage,
        knifeRobber,
        eliteDaggerRobber,
        blackWizard,
        robberBoss,
    }
    enum EventType 
    { 
        fountain,
        well,
        smithy,
        inn,
        bigTree=13,
        tomb=5,
        angel=7,
        start=9,
        bar=11
    }

    private Dictionary<EnemyType, string> EnemyNameDictionary = new Dictionary<EnemyType, string>()
    {
        { EnemyType.heresyCleric,"异端牧师" },
        { EnemyType.daggerRobber,"双刃劫掠者" },
        { EnemyType.SwordShieldRobber,"剑盾劫掠者" },
        { EnemyType.RobberArcher,"劫掠者弓箭手" },
        { EnemyType.RobberMage,"劫掠者法师" },
        { EnemyType.knifeRobber,"劫掠者刺客" },
        { EnemyType.eliteDaggerRobber,"精英双刃劫掠者" },
        { EnemyType.blackWizard,"黑巫师" },
        { EnemyType.robberBoss,"劫掠者首领" }
    };
    private Dictionary<EnemyType, string> EnemyDescribeDictionary = new Dictionary<EnemyType, string>()
    {
        {EnemyType.heresyCleric,"这是信奉邪神的牧师，他会用魔法增益他的友军" },
        { EnemyType.daggerRobber,"这个强盗善使双刃，进攻性很强" },
        { EnemyType.SwordShieldRobber,"这个强盗善于防御，能用盾牌承受大量伤害" },
        { EnemyType.RobberArcher,"这个强盗在用弓箭瞄准你" },
        { EnemyType.RobberMage,"什么？强盗还会魔法？" },
        { EnemyType.knifeRobber,"这个强盗喜欢cos艾吉奥" },
        {EnemyType.blackWizard,"这是一个强大的黑巫师，他会使用魔法力量打击他的敌人" }
    };
    private Dictionary<EventType, string> EventNameDictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"活力喷泉" },
        {EventType.well,"许愿井" },
        {EventType.inn,"鹈鹕镇的客栈" },
        {EventType.smithy,"鹈鹕镇的铁匠铺" },
        {EventType.bigTree,"鹈鹕镇的大树" },
        {EventType.tomb,"坟墓" },
        {EventType.angel,"天使雕像" },
        {EventType.start,"旅途的开始"  },
        {EventType.bar,"酒馆"  }

    };
    private Dictionary<EventType, string> EventDescribeDictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"rpg游戏里常见的泉水，显然是用来恢复的" },
        {EventType.well,"一口神秘的水井，据说丢入硬币会获得随机的回报\n(战斗系统里的装备还没做完)" },
        {EventType.inn,"旅行者可以在这里休息，跳过回合回满生命值\n只需要花费5金币\n经济系统没做所以实际是免费的" },
        {EventType.smithy,"这里可以委托铁匠铸造武器和护甲\n(战斗系统里的装备还没做完)" },
        {EventType.bigTree,"树下的告示栏写有可供接取的任务" },
        {EventType.tomb,"你的一位英雄在这里倒下，获得女神的恩赐来复活" },
        {EventType.angel,"这里矗立着一座天使雕像，但你需要清理掉周围的敌人才能与之互动\n（获得可以复活队友一次的道具）" },
        {EventType.start,"点击角色或角色头像选中\n点击或WASD移动\n去西边的村庄开始冒险吧"  },
        {EventType.bar,"一个八方来客都可聚集在此借酒浇愁的友善之地。对了，我们有提过他们是用拳头来解决分歧的吗？"  }
    };
    private Dictionary<EventType, string> EventButton1Dictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"饮用" },
        {EventType.well,"投入硬币" },
        {EventType.inn,"休息" },
        {EventType.smithy,"选项一" },
        {EventType.bigTree,"接取任务" },
        {EventType.tomb,"复活" },
        {EventType.angel,"祈祷" },
        {EventType.start,"好的"  },
        {EventType.bar,"战斗！"  }
    };
    private Dictionary<EventType, string> EventButton2Dictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"原地休息" },
        {EventType.smithy,"选项二" }
    };

    void Start()
    {
        Cellinfo.SetActive(false);
        turnNextButton.onClick.AddListener(TurnStart);
        battleSceneButton.onClick.AddListener(EnterBattleScene);
        closePanelButton.onClick.AddListener(CloseInfo);
        eventButton1.onClick.AddListener(EventChoiceOne);
        eventButton2.onClick.AddListener(EventChoiceTwo);
        isPause = false;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        blockPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void ContinueGame()
    {
        gamePausePanel.SetActive(false);
        blockPanel.SetActive(false);
        Time.timeScale = 1.0f;
        isPause = false;
    }
    private void PauseGame()
    {
        gamePausePanel.SetActive(true);
        blockPanel.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }
    public void DisplayInfo(Vector2 position , int type)
    {
        RectTransform rt = Cellinfo.GetComponent<RectTransform>();
        Cellinfo.SetActive(true);
        Camera uiCamera = null;
        Vector3 globalmouseposition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, position, uiCamera, out globalmouseposition);
        Cellinfo.transform.position = globalmouseposition ;
        infoText.text = celldic[(CellType)type];
    }
    public void CloseInfo()
    {
        EnemyTip.SetActive(false);
        Cellinfo.SetActive(false);
        EnemyInfo.SetActive(false);
        blockPanel.SetActive(false);
        GameManager.CloseSelect();
    }
    public void DisplayEnemyInformation(int type, bool flag)
    {
        blockPanel.SetActive(true);
        EnemyInfo.SetActive(true);
        eButton1.SetActive(false);
        eButton2.SetActive(false);
        bsButton.SetActive(false);
        EnemyDescribe.text = EnemyDescribeDictionary[(EnemyType)type];
        EnemyName.text = EnemyNameDictionary[(EnemyType)type];
        if (flag)
        {
            bsButton.SetActive(true);
            EnemyTip.SetActive(true);
        }
    }
    public void DisplayEventInformation(int type,GameObject thisEvent,bool flag)
    {
        blockPanel.SetActive(true);
        selectedEvent = thisEvent.GetComponent<Event_Map>();
        if (type >= 2 && type <= 4 && FirstArriveTown)
        {
            FirstArriveTown = false;
            Dialogue.SetActive(true);
            Dialogue__();
        }
        else if (type >= 2 && type <= 4 && BossFight)
        {
            EnemyInfo.SetActive(true);
            eButton1.SetActive(false);
            eButton2.SetActive(false);
            bsButton.SetActive(false);
            EnemyDescribe.text = EnemyDescribeDictionary[(EnemyType)8];
            EnemyName.text = EnemyNameDictionary[(EnemyType)8];
        }
        else
        {
            eButton1.SetActive(false);
            eButton2.SetActive(false);
            EnemyInfo.SetActive(true);
            EnemyName.text = EventNameDictionary[(EventType)type];
            EnemyDescribe.text = EventDescribeDictionary[(EventType)type];
            bsButton.SetActive(false);
            if (flag)
            {
                eButton1.SetActive(true);
                eButton1text.text = EventButton1Dictionary[(EventType)type];
                if (type % 2 == 0)
                {
                    eButton2.SetActive(true);
                    eButton2text.text = EventButton2Dictionary[(EventType)(type)];
                }
            }
        }
        
    }
    public void EventChoiceOne()
    {
        selectedEvent.EventOne();
    }
    public void EventChoiceTwo()
    {
        selectedEvent.EventTwo();
    }
    public void UpdateStaminaSlider(float currentStamina, float maxStamina,int ID)
    {
        if (ID == 1)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
            _staminaValue = string.Format("{0}/{1}", currentStamina, maxStamina);
            staminaValue.text = _staminaValue;
        }else if (ID == 2)
        {
            staminaSlider2.maxValue = maxStamina;
            staminaSlider2.value = currentStamina;
            _staminaValue2 = string.Format("{0}/{1}", currentStamina, maxStamina);
            staminaValue2.text = _staminaValue2;
        }else if (ID==3)
        {
            staminaSlider3.maxValue = maxStamina;
            staminaSlider3.value = currentStamina;
            _staminaValue3 = string.Format("{0}/{1}", currentStamina, maxStamina);
            staminaValue3.text = _staminaValue3;
        }
    }
    public void UpdateHealthSlider(float currentHealth, float maxHealth, int ID)
    {
        if (ID == 0)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            _healthValue = string.Format("{0}/{1}", currentHealth, maxHealth);
            healthValue.text = _healthValue;
        }
        else if (ID == 1)
        {
            healthSlider2.maxValue = maxHealth;
            healthSlider2.value = currentHealth;
            _healthValue2 = string.Format("{0}/{1}", currentHealth, maxHealth);
            healthValue2.text = _healthValue2;
        }
        else if (ID == 2)
        {
            healthSlider3.maxValue = maxHealth;
            healthSlider3.value = currentHealth;
            _healthValue3 = string.Format("{0}/{1}", currentHealth, maxHealth);
            healthValue3.text = _healthValue3;
        }
    }
    public void TurnStart()
    {
        turnStart?.Invoke();
    }
    public void EnterBattleScene()
    {

        SceneLoader.Instance.LoadBattleScene();
/*        switch (PlayerTeamState.PlayerState.BattleResult)
        {
            case false:
                {
                    GameManager.BattleFailed();
                    break;
                }
        }*/
    }
    public void SelectPlayerOne()
    {
        Players[0].GetComponent<Carriage>().OnMouseDown();
        SelectPlayers[0].SetActive(true);
        SelectPlayers[1].SetActive(false);
        SelectPlayers[2].SetActive(false);
    }
    public void SelectPlayerTwo()
    {
        Players[1].GetComponent<Carriage>().OnMouseDown();
        SelectPlayers[0].SetActive(false);
        SelectPlayers[1].SetActive(true);
        SelectPlayers[2].SetActive(false);
    }
    public void SelectPlayerThree()
    {
        Players[2].GetComponent<Carriage>().OnMouseDown();
        SelectPlayers[0].SetActive(false);
        SelectPlayers[1].SetActive(false);
        SelectPlayers[2].SetActive(true);
    }
    public void LoadPlayer(int Profession,int playerID)
    {
        if (playerID == 1)
        {
            PlayerOne.sprite = Professions[Profession];
            ProfessionOne.text = ProfessionNames[Profession];
        }
        else if (playerID == 2)
        {
            PlayerTwo.sprite = Professions[Profession];
            ProfessionTwo.text = ProfessionNames[Profession];
        }
        else
        {
            PlayerThree_.SetActive(true);
            PlayerThreeProfession = Profession;
            PlayerThree.sprite = Professions[Profession];
            ProfessionThree.text = ProfessionNames[Profession];
            UpdateHealthSlider(GameData.gsd.curHealth[2], GameData.gsd.maxHealth[2], 2);
        }
    }
    IEnumerator Dialogue_()
    {
        dialogue.text = dialogues[dialogueIndex];
        blockPanel.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        dialogueIndex++;
        GameData.gsd.dialogueIndex = dialogueIndex;
        if (dialogueIndex == 4)
        {
            Dialogue.SetActive(false);
            blockPanel.SetActive(false);
            SpeakerName.text = "酒馆老板";
            Speaker.sprite = Professions[5];
            GameManager.ExposeBar();
        }
        else if (dialogueIndex == 7)
        {
            SpeakerName.text = ProfessionNames[PlayerThreeProfession];
            Speaker.sprite = Professions[PlayerThreeProfession];
            Invoke("Dialogue__", 0.1f);
        }
        else if (dialogueIndex == 9)
        {
            SpeakerName.text = "镇长";
            Speaker.sprite = Professions[4];
            Dialogue.SetActive(false);
            blockPanel.SetActive(false);
        }
        else if (dialogueIndex == 12)
        {
            Dialogue.SetActive(false);
            blockPanel.SetActive(false);
            GameManager.FocusVillage();
        }
        else Invoke("Dialogue__",0.1f);
    }

    public void Dialogue__()
    {
        StartCoroutine(Dialogue_());
    }

    public void MissionStep2()
    {
        Dialogue.SetActive(true);
        Dialogue__();
    }

    public void DisplayEquip()
    {
        equips_[GameManager.selectedID - 1].SetActive(true);
        equips[GameManager.selectedID - 1].sprite = Necklace;
    }
}
