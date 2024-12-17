using System.Collections;
using System.Collections.Generic;
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

/*    Dictionary<Celltype, string>
    {
        {Celltype.grass,"²ÝÆº"}
        
    };*/
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void displayinfo(Vector2 position , int type)
    {
        Cellinfo.transform.position = position;
    }
}
