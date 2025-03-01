using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [Header("Players")]
    public List<GameObject> Player;
    [Header("Enemies")]
    public List<GameObject> Enemy;

    [Header("Transforms")]
    public Transform[] transforms;

    static public Dictionary<int, GameObject> nodeDictionary;
    static public Dictionary<int, GameObject> characterDictionary;

    //public List<GameObject> spawnList;
    public List<GameObject> nodeList;
    public List<GameObject> characterList;

    //public int spawnCount = 0;

    public int[] Positions;
    public int UpperLimitForKnight = 3;
    public int LowerLimitForKnight = 5;
    public int UpperLimitForOthers = 0;
    public int LowerLimitForOthers = 2;
    public int PlayerCount = 0;
    
    private int RDPosition = 0;

    private void Awake()
    {
        transforms = new Transform[6] ;
        Player = new List<GameObject>();
        Enemy = new List<GameObject>();
        Positions = new int[6] { 0, 0, 0, 0, 0, 0 };
        nodeDictionary = new Dictionary<int, GameObject>();
        characterDictionary = new Dictionary<int, GameObject>();
    }

    public void LoadPlayers()
    {
        


        for (int i = 0; i < nodeList.Count; i++)
        { //向node字典中加入内容
            if (nodeList[i] != null)
            {
                nodeDictionary.Add(i, nodeList[i]);
            }
        }

        for (int i = 0; i < characterList.Count; i++)
        { //向node字典中加入内容
            if (characterList[i] != null)
            {
                characterDictionary.Add(i, characterList[i]);
            }
        }

        Player.Add(characterDictionary[PlayerTeamState.PlayerState.Professions[0]]);
        Player.Add(characterDictionary[PlayerTeamState.PlayerState.Professions[0]]);
        if (PlayerCount == 3)
        {
            Player.Add(characterDictionary[PlayerTeamState.PlayerState.Professions[2]]);
        }

        //Player.Add(characterDictionary[0]);
        //Player.Add(characterDictionary[1]);
        //Player.Add(characterDictionary[2]);

        //根据编号生成两个或者三个角色的人物
        //首先确定每个人的位置
        for (int i = 0; i < 3; i++)
        {
            if (PlayerTeamState.PlayerState.isHere[i])
            {//如果第i个玩家是要进入场景内的
                PlayerCount++;
                int profession = 0;
                //profession = PlayerTeamState.PlayerState.characterProperties[i].Profession;
                if (!(profession == 0))//该角色不是骑士
                {
                    if (i == 0)
                    {
                        Positions[i] = Random.Range(0, 3);
                    }
                    else if (i == 1)
                    {
                        RDPosition = Random.Range(0, 3);
                        while (RDPosition == Positions[0])
                        {
                            RDPosition = Random.Range(0, 3);
                        }
                        Positions[i] = RDPosition;
                    }
                    else if (i == 2)
                    {
                        RDPosition = Random.Range(0, 3);
                        while (RDPosition == Positions[0] || RDPosition == Positions[1])
                        {
                            RDPosition = Random.Range(0, 3);
                        }
                        Positions[i] = RDPosition;
                    }
                    
                }
                else//是骑士
                {
                    if (i == 0)
                    {
                        Positions[i] = Random.Range(3, 6);
                    }
                    else if (i == 1)
                    {
                        RDPosition = Random.Range(3, 6);
                        while (RDPosition == Positions[0])
                        {
                            RDPosition = Random.Range(3, 6);
                        }
                        Positions[i] = RDPosition;
                    }
                    else if (i == 2)
                    {
                        RDPosition = Random.Range(3, 6);
                        while (RDPosition == Positions[0] || RDPosition == Positions[1])
                        {
                            RDPosition = Random.Range(3, 6);
                        }
                        Positions[i] = RDPosition;
                    }
                }
            }
        }



        //transform1 = nodeList[0].transform;
        //position1 = Random.Range(0, 5);

        //RDPosition = Random.Range(0, 5);
        //while (RDPosition == position1)
        //{
        //    RDPosition = Random.Range(0, 5);
        //}
        //position2 = RDPosition;

        //while (RDPosition == position1 || RDPosition == position2)
        //{
        //    RDPosition = Random.Range(0, 5);
        //}
        //position3 = RDPosition;//确保三个随机数不一样



        transforms[0] = nodeDictionary[Positions[0]].transform;
        transforms[1] = nodeDictionary[Positions[1]].transform;
        if (PlayerCount == 3)
        {
            transforms[2] = nodeDictionary[Positions[2]].transform;
        }

        Player[0].transform.localScale = new Vector3(2f,2f,2f);
        Player[1].transform.localScale = new Vector3(2f,2f,2f);
        if (PlayerCount == 3)
        {
            Player[2].transform.localScale = new Vector3(2f, 2f, 2f);
        }

        Instantiate(Player[0], transforms[0]);
        Instantiate(Player[1], transforms[1]);
        if (PlayerCount == 3)
        {
            Instantiate(Player[0], transforms[0]);            
        }



        //扫描成员、使其成为某个node下面的子对象，并将成员加入列表，方便后面比较速度决定行动顺序
        nodeDictionary[Positions[0]].GetComponent<Nodes>().isPlayerHere = true;
        nodeDictionary[Positions[1]].GetComponent<Nodes>().isPlayerHere = true;
        if (PlayerCount == 3)
        {
            nodeDictionary[Positions[2]].GetComponent<Nodes>().isPlayerHere = true;            
        }


        nodeDictionary[Positions[0]].GetComponentInChildren<CharacterProperty>().Position = Positions[0];
        nodeDictionary[Positions[1]].GetComponentInChildren<CharacterProperty>().Position = Positions[1];
        if (PlayerCount == 3)
        {
            nodeDictionary[Positions[2]].GetComponentInChildren<CharacterProperty>().Position = Positions[2];            
        }

    }


    public void LoadEnemies()
    {
        //临时指定，后面加上具体要什么样的敌人的逻辑
        Enemy.Add(characterDictionary[22]);
        Enemy.Add(characterDictionary[26]);
        Enemy.Add(characterDictionary[24]);


        Positions[3] = Random.Range(6, 11);
        RDPosition = Random.Range(6, 11);
        while (RDPosition == Positions[3])
        {
            RDPosition = Random.Range(6, 11);
        }
        Positions[4] = RDPosition;

        while (RDPosition == Positions[3] || RDPosition == Positions[4])
        {
            RDPosition = Random.Range(6, 11);
        }
        Positions[5] = RDPosition;//确保三个随机数不一样

        transforms[3] = nodeDictionary[Positions[3]].transform;
        transforms[4] = nodeDictionary[Positions[4]].transform;
        transforms[5] = nodeDictionary[Positions[5]].transform;

        Enemy[0].transform.localScale = new Vector3(2f, 2f, 2f);
        Enemy[1].transform.localScale = new Vector3(2f, 2f, 2f);
        Enemy[2].transform.localScale = new Vector3(2f, 2f, 2f);

        Instantiate(Enemy[0], transforms[3]);
        Instantiate(Enemy[1], transforms[4]);
        Instantiate(Enemy[2], transforms[5]);


        //扫描成员、使其成为某个node下面的子对象，并将成员加入列表，方便后面比较速度决定行动顺序
        nodeDictionary[Positions[3]].GetComponent<Nodes>().isEnemyHere = true;
        nodeDictionary[Positions[4]].GetComponent<Nodes>().isEnemyHere = true;
        nodeDictionary[Positions[5]].GetComponent<Nodes>().isEnemyHere = true;

    }
}
