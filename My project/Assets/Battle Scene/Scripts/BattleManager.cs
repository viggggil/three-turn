using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
   static public int round;

   private void Awake()
   {
      //基本对象的载入
      LoadPlayers();
      LoadEnemies();
      
   }

   private void Start()
   {
      //确认载入后回合开始
   }

   private void LoadPlayers()
   {
      
      //在此补充进入场景前载入成员的逻辑
      //扫描成员、使其成为某个node下面的子对象，并将成员加入列表，方便后面比较速度决定行动顺序
   }

   private void LoadEnemies()
   {

      //在此补充进入场景前载入敌人的逻辑
   }
}
