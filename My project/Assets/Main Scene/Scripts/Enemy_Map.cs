using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Map : MonoBehaviour,IMove_
{
    // Start is called before the first frame update
    private UIManager UIManager;
    private GameManager GameManager;
    private PlayerTeamState PlayerTeamState;
    public int SerialNumber;
    [SerializeField] public int type;
    public GameObject Enemy;
    void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerTeamState = GameObject.Find("PlayerTeamState").GetComponent<PlayerTeamState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        bool[] arr = GameManager.TestDistance_(this.gameObject);
        bool flag = true;
        if (arr[0] == false && arr[1] == false && arr[2] == false) flag = false;
        else
        {
            PlayerTeamState.PlayerState.EnemyType = type;
            PlayerTeamState.PlayerState.isHere = arr;
        }
        UIManager.DisplayEnemyInformation(type,flag);
    }

    public void Move(Vector2 direction)
    {
        transform.DOMove(direction, 0.3f);
        if (direction.x > Enemy.transform.position.x)
        {
            Enemy.GetComponent<RectTransform>().localScale = new Vector3(-1.25f, 1.25f, 1.25f);
        }
        if (direction.x <Enemy.transform.position.x)
        {
            Enemy.GetComponent<RectTransform>().localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
        Animator animator = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        animator.SetBool("Move", true);
        Invoke("Move_", 0.3f);
    }

    public void Move_()
    {
        Animator animator = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        animator.SetBool("Move", false);
    }
}
