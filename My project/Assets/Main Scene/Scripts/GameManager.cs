using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] cells;
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
    }
    void Update()
    {
        
    }

 
}
