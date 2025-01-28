using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Map : MonoBehaviour
{
    // Start is called before the first frame update
    private UIManager UIManager;
    [SerializeField] public int type;
    void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        UIManager.DisplayEnemyInformation(type);
    }
}
