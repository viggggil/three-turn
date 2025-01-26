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
    public Button turnNextButton;
    public UnityEvent turnStart;
    public GameObject EnemyInfo;
    public Text EnemyName;
    public Text EnemyDescribe;
    public Button battleSceneButton;
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
    private Dictionary<EnemyType, string> EnemyNameDictionary = new Dictionary<EnemyType, string>()
    {
        {EnemyType.axeman,"还没取好名字的怪" }
    };

    void Start()
    {
        Cellinfo.SetActive(false);
        turnNextButton.onClick.AddListener(TurnStart);
        battleSceneButton.onClick.AddListener(EnterBattleScene);
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
        EnemyName.text = EnemyNameDictionary[(EnemyType)type];

    }
    public void UpdateSlider(float currentStamina, float maxStamina)
    {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
        _staminaValue = string.Format("{0}/{1}",currentStamina ,maxStamina );
        staminaValue.text = _staminaValue;
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
