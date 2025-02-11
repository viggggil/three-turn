using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [Header("Players")]
   [SerializeField] GameObject Player1;
   [SerializeField] GameObject Player2;
   [SerializeField] GameObject Player3;

   [Header("Transforms")]
   public Transform transform1;
   public Transform transform2;
   public Transform transform3;

   public List<GameObject> spawnList;
   public int spawnCount = 0;

   public List<GameObject> nodeList;
    
   private int position1 = 0;//测试阶段，先令定值

   public void LoadPlayers()
   {
      Player1 = spawnList[0];
      //Player2 = spawnList[1];
      //Player3 = spawnList[2];
      transform1 = nodeList[0].transform;
      Instantiate(Player1, transform1);

      //在此补充进入场景前载入成员的逻辑
      //扫描成员、使其成为某个node下面的子对象，并将成员加入列表，方便后面比较速度决定行动顺序
      spawnList.Clear();
   }

   public void LoadEnemies()
   {

      //在此补充进入场景前载入敌人的逻辑
   }
}
