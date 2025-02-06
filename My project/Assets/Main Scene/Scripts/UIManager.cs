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
    public Button turnNextButton;
    public UnityEvent turnStart;
    public GameObject EnemyInfo;
    public Text EnemyName;
    public Text EnemyDescribe;
    public Button battleSceneButton;
    public GameObject bsButton;
    public Button eventButton1;
    public GameObject eButton1;
    public Text eButton1text;
    public Button eventButton2;
    public GameObject eButton2;
    public Text eButton2text;
    public Button closePanelButton;
    enum CellType
    {
        grass,
        woods,
        forest,
        flowerfield,
        mountain
    }
    private Dictionary<CellType, string> celldic = new Dictionary<CellType, string>()
    {{CellType.grass,"草坪"},
     {CellType.woods,"森林"},
      {CellType.flowerfield,"花地" },
       { CellType.mountain,"山地"}
    };
    enum EnemyType
    {
        axeman
    }
    enum EventType 
    { 
        fountain
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
        {EventType.fountain,"活力喷泉" }
    };
    private Dictionary<EventType, string> EventDescribeDictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"rpg游戏里常见的泉水，显然是用来恢复的" }
    };
    private Dictionary<EventType, string> EventButton1Dictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"饮用" }
    };
    private Dictionary<EventType, string> EventButton2Dictionary = new Dictionary<EventType, string>()
    {
        {EventType.fountain,"原地休息" }
    };

    void Start()
    {
        Cellinfo.SetActive(false);
        turnNextButton.onClick.AddListener(TurnStart);
        battleSceneButton.onClick.AddListener(EnterBattleScene);
        closePanelButton.onClick.AddListener(CloseInfo);
    }

    void Update()
    {
        
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
        Cellinfo.SetActive(false);
        EnemyInfo.SetActive(false);
    }

    public void DisplayEnemyInformation(int type)
    {
        EnemyInfo.SetActive(true);
        bsButton.SetActive(true);
        eButton1.SetActive(false);
        eButton2.SetActive(false);
        EnemyDescribe.text = EnemyDescribeDictionary[(EnemyType)type];
        EnemyName.text = EnemyNameDictionary[(EnemyType)type];
    }

    public void DisplayEventInformation(int type)
    {
        EnemyInfo.SetActive(true);
        bsButton.SetActive(false);
        eButton1.SetActive(true);
        eButton2.SetActive(true);
        EnemyName.text = EventNameDictionary[(EventType)type];
        EnemyDescribe.text = EventDescribeDictionary[(EventType)type];
        eButton1text.text = EventButton1Dictionary[(EventType)type];
        eButton2text.text = EventButton2Dictionary[(EventType)type];
    }
    public void UpdateSlider(float currentStamina, float maxStamina,int ID)
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
        }
    }

    public void TurnStart()
    {
        turnStart?.Invoke();
    }

    public void EnterBattleScene()
    {
        SceneLoader.Instance.LoadBattleScene();
    }
}
