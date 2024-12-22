using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text infotext;
    public GameObject Cellinfo;
    
    
    enum Celltype
    {
        grass,
        woods,
        forest
    }
    private Dictionary<Celltype, string> celldic = new Dictionary<Celltype, string>()
    {{Celltype.grass,"≤›∆∫"},
     {Celltype.woods,"…≠¡÷"}
    };

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Displayinfo(Vector2 position , int type)
    {
        RectTransform rt = Cellinfo.GetComponent<RectTransform>();
        Camera uiCamera = null;
        Vector3 globalmouseposition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, position, uiCamera, out globalmouseposition);
        Cellinfo.transform.position = globalmouseposition ;
        infotext.text = celldic[(Celltype)type];
    }
}
