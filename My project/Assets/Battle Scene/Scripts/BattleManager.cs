using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
   Spawner spawner;

   static public int round;

   private bool isGameOver;

   private void Awake()
   {
      spawner = GetComponent<Spawner>();
      //基本对象的载入
      spawner.LoadPlayers();
      spawner.LoadEnemies();
      
   }

   private void Start()
   {
      //确认载入后回合开始
   }

   
}
