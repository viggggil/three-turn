using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject Spawner;

    Spawner spawner;

    static public int round;

    private bool isGameOver;

    private void Awake()
    {
        Spawner = GameObject.FindWithTag("Spawner");
        spawner = Spawner.GetComponent<Spawner>();//找到Spawner并获得它的脚本
        //基本对象的载入
        
       
    }

    private void Start()
    {
        //spawner.LoadPlayers();
        //spawner.LoadEnemies();
       //确认载入后回合开始
    }

   
}
