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

    public Slider magicSlider;
    public GameObject magicBar;
    public Text magicValue;
    private string _magicValue;
    public Slider magicSlider2;
    public GameObject magicBar2;
    public Text magicValue2;
    private string _magicValue2;
    public Slider magicSlider3;
    public GameObject magicBar3;
    public Text magicValue3;
    private string _magicValue3;

    public Button turnNextButton;
    public UnityEvent turnStart;

    public GameObject EnemyInfo;
    public Text EnemyName;
    public Text EnemyDescribe;
    public GameObject EnemyTip;

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

    private bool isPause;
    public GameObject gamePausePanel;
    public GameObject blockPanel;

    public GameManager GameManager;
    public GameObject[] Players;
    public GameObject[] SelectPlayers;

    enum CellType
    {
        grass,
        woods,
        forest,
        flowerfield,
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
        axeman
    }
    enum EventType 
    { 
        fountain,
        well,
        smithy,
        inn,
        bigTree,
        tomb,
        angel=7
    }
    private Dictionary<EnemyType, string> EnemyNameDictionary = new Dictionary<EnemyType, string>()
    {
        {EnemyType.axeman,"还没取好名字的怪" }
    };
    private Dictionary<EnemyType, string> EnemyDescribeDictionary = new Dictionary<EnemyType, string>()
    {
        {EnemyType.axeman,"怪物的描述" }
    };
    private Dictionary<EventType, string> EventNameDictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"活力喷泉" },
        {EventType.well,"许愿井" },
        {EventType.inn,"克里特村的客栈" },
        {EventType.smithy,"克里特村的铁匠铺" },
        {EventType.bigTree,"村口的大树" },
        {EventType.tomb,"坟墓" },
        {EventType.angel,"天使雕像" }

    };
    private Dictionary<EventType, string> EventDescribeDictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"rpg游戏里常见的泉水，显然是用来恢复的" },
        {EventType.well,"一口神秘的水井，据说丢入硬币会获得随机的回报" },
        {EventType.inn,"旅行者可以在这里休息，跳过回合回满生命值\n只需要花费5金币" },
        {EventType.smithy,"这里可以委托铁匠铸造武器和护甲\n铁匠拿出两张图纸让你挑选" },
        {EventType.bigTree,"树下的告示栏写有可供接取的任务" },
        {EventType.tomb,"你的一位英雄在这里倒下，获得女神的恩赐来复活" },
        {EventType.angel,"这里矗立着一座天使雕像，但你需要清理掉周围的敌人才能与之互动" }
    };
    private Dictionary<EventType, string> EventButton1Dictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"饮用" },
        {EventType.well,"投入硬币" },
        {EventType.inn,"休息" },
        {EventType.smithy,"选项一" },
        {EventType.bigTree,"接取任务一" },
        {EventType.tomb,"复活" },
        {EventType.angel,"祈祷" }
    };
    private Dictionary<EventType, string> EventButton2Dictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"原地休息" },
        {EventType.smithy,"选项二" },
        {EventType.bigTree,"接取任务二" }
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
        if (ID == 1)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            _healthValue = string.Format("{0}/{1}", currentHealth, maxHealth);
            healthValue.text = _healthValue;
        }
        else if (ID == 2)
        {
            healthSlider2.maxValue = maxHealth;
            healthSlider2.value = currentHealth;
            _healthValue2 = string.Format("{0}/{1}", currentHealth, maxHealth);
            healthValue2.text = _healthValue2;
        }
        else if (ID == 3)
        {
            healthSlider3.maxValue = maxHealth;
            healthSlider3.value = currentHealth;
            _healthValue3 = string.Format("{0}/{1}", currentHealth, maxHealth);
            healthValue3.text = _healthValue3;
        }
    }
    public void UpdateMagicSlider(float currentMagic, float maxMagic, int ID)
    {
        if (ID == 1)
        {
            magicSlider.maxValue = maxMagic;
            magicSlider.value = currentMagic;
            _magicValue = string.Format("{0}/{1}", currentMagic, maxMagic);
            magicValue.text = _magicValue;
        }
        else if (ID == 2)
        {
            magicSlider2.maxValue = maxMagic;
            magicSlider2.value = currentMagic;
            _magicValue2 = string.Format("{0}/{1}", currentMagic, maxMagic);
            magicValue2.text = _magicValue2;
        }
        else if (ID == 3)
        {
            magicSlider3.maxValue = maxMagic;
            magicSlider3.value = currentMagic;
            _magicValue3 = string.Format("{0}/{1}", currentMagic, maxMagic);
            magicValue3.text = _magicValue3;
        }
    }
    public void TurnStart()
    {
        turnStart?.Invoke();
    }
    public void EnterBattleScene()
    {
        SceneLoader.Instance.LoadBattleScene();
        switch (PlayerTeamState.PlayerState.BattleResult)
        {
            case 0:
                {
                    GameManager.BattleFailed();
                    break;
                }
        }
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
}
