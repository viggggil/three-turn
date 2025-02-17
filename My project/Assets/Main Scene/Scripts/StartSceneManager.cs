using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public Text PlayerOneName;
    public Text PlayerTwoName;
    public Text PlayerOneDescribe;
    public Text PlayerTwoDescribe;

    public Image PlayerOneImage;
    public Image PlayerTwoImage;

    public Sprite[] Images;
    public string[] Names;
    public string[] Describes;

    private int PlayerOne=0, PlayerTwo = 0;
    private bool PlayerOneConfirm, PlayerTwoConfirm;
    public Text Confirm1, Confirm2;

    public SceneLoader SceneLoader;

    public GameObject StartPanel;
    public GameObject PickPanel;

    public void LastProfessionOne()
    {
        if (PlayerOne == 0) return;
        PlayerOne--;
        RefreshPlayerOne();
    }

    public void LastProfessionTwo()
    {
        if (PlayerTwo == 0) return;
        PlayerTwo--;
        RefreshPlayerTwo();
    }

    public void NextProfessionOne()
    {
        if (PlayerOne>=3) return;
        PlayerOne++;
        RefreshPlayerOne();
    }

    public void NextProfessionTwo()
    {
        if (PlayerTwo >= 3) return;
        PlayerTwo++;
        RefreshPlayerTwo();
    }
    public void RefreshPlayerOne()
    {
        PlayerOneImage.sprite = Images[PlayerOne];
        PlayerOneName.text = Names[PlayerOne];
        PlayerOneDescribe.text = Describes[PlayerOne];
        PlayerOneConfirm = false;
        Confirm1.text = "确认";
    }

    public void RefreshPlayerTwo()
    {
        PlayerTwoImage.sprite = Images[PlayerTwo];
        PlayerTwoName.text = Names[PlayerTwo];
        PlayerTwoDescribe.text = Describes[PlayerTwo];
        PlayerTwoConfirm = false;
        Confirm2.text = "确认";
    }

    public void ConfirmOne()
    {
        if (PlayerTwoConfirm)
        {
            SceneLoader.PlayerOneProfession = PlayerOne;
            SceneLoader.PlayerTwoProfession = PlayerTwo;
            SceneLoader.StartNewGame();
        }
        else
        {
            PlayerOneConfirm = true;
            Confirm1.text = "已确认";
        }
    }

    public void ConfirmTwo()
    {
        if (PlayerOneConfirm)
        {
            SceneLoader.PlayerOneProfession = PlayerOne;
            SceneLoader.PlayerTwoProfession = PlayerTwo;
            SceneLoader.StartNewGame();
        }
        else
        {
            PlayerTwoConfirm = true;
            Confirm2.text = "已确认";
        }
    }

    public void PickProfessions()
    {
        StartPanel.SetActive(false);
        PickPanel.SetActive(true);
    }

    private void Start()
    {
        SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        Confirm1.text = "确认";
        Confirm2.text = "确认";
    }
}
