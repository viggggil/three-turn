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
    enum Celltype
    {
        grass,
        woods,
        forest,
        flowerfield,
        mountain
    }
    private Dictionary<Celltype, string> celldic = new Dictionary<Celltype, string>()
    {{Celltype.grass,"草坪"},
     {Celltype.woods,"森林"},
      {Celltype.flowerfield,"花地" },
       { Celltype.mountain,"山地"}
    };

    void Start()
    {
        Cellinfo.SetActive(false);
        turnNextButton.onClick.AddListener(TurnStart);
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
        infoText.text = celldic[(Celltype)type];
    }

    public void CloseInfo()
    {
        Cellinfo.SetActive(false);
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
}
