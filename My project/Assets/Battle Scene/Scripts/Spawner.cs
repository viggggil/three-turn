using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] GameObject Player3;
    [Header("Enemies")]
    [SerializeField] GameObject Enemy1;
    [SerializeField] GameObject Enemy2;
    [SerializeField] GameObject Enemy3;

    [Header("Transforms")]
    [SerializeField] Transform transform1;
    [SerializeField] Transform transform2;
    [SerializeField] Transform transform3;
    [SerializeField] Transform transform4;
    [SerializeField] Transform transform5;
    [SerializeField] Transform transform6;

    static public Dictionary<int, GameObject> nodeDictionary;

    //public List<GameObject> spawnList;
    public List<GameObject> nodeList;
    
    //public int spawnCount = 0;

    public int position1;
    public int position2;
    public int position3;//这三个表示随机出来的几个位置
    public int position4;
    public int position5;
    public int position6;
    private int RDPosition = 0;

    private void Awake()
    {
        nodeDictionary = new Dictionary<int, GameObject>();
    }

    public void LoadPlayers()
    {
        for (int i = 0; i < nodeList.Count; i++)
        { //向字典中加入内容
            if (nodeList[i] != null)
            {
                nodeDictionary.Add(i, nodeList[i]);
            }
        }

        //Player1 = spawnList[0];
        //Player2 = spawnList[1];
        //Player3 = spawnList[2];
        //transform1 = nodeList[0].transform;
        position1 = Random.Range(0, 5);
        RDPosition = Random.Range(0, 5);
        while (RDPosition == position1)
        {
            RDPosition = Random.Range(0, 5);
        }
        position2 = RDPosition;

        while (RDPosition == position1 || RDPosition == position2)
        {
            RDPosition = Random.Range(0, 5);
        }
        position3 = RDPosition;//确保三个随机数不一样

        PlayerTeamState.PlayerState.playerPosition[0] = position1;
        PlayerTeamState.PlayerState.playerPosition[1] = position2;
        PlayerTeamState.PlayerState.playerPosition[2] = position3;
        //去修改公共文件当中的值

        transform1 = nodeDictionary[position1].transform;
        transform2 = nodeDictionary[position2].transform;
        transform3 = nodeDictionary[position3].transform;

        Instantiate(Player1, transform1);
        Instantiate(Player2, transform2);
        Instantiate(Player3, transform3);

        
        //扫描成员、使其成为某个node下面的子对象，并将成员加入列表，方便后面比较速度决定行动顺序
      
    }

    public void LoadEnemies()
    {
        
        position4 = Random.Range(6, 11);
        RDPosition = Random.Range(6, 11);
        while (RDPosition == position4)
        {
            RDPosition = Random.Range(6, 11);
        }
        position5 = RDPosition;

        while (RDPosition == position4 || RDPosition == position5)
        {
            RDPosition = Random.Range(6, 11);
        }
        position6 = RDPosition;//确保三个随机数不一样

        transform4 = nodeDictionary[position4].transform;
        transform5 = nodeDictionary[position5].transform;
        transform6 = nodeDictionary[position6].transform;

        Instantiate(Enemy1, transform4);
        Instantiate(Enemy2, transform5);
        Instantiate(Enemy3, transform6);


        //扫描成员、使其成为某个node下面的子对象，并将成员加入列表，方便后面比较速度决定行动顺序

        //在此补充进入场景前载入敌人的逻辑
    }
}
