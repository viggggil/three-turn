using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPropertyCard : MonoBehaviour
{
    [SerializeField] private Text text;
    public void SetupCharacterPropert(CharacterProperty characterProperty)
    {
        switch (characterProperty.profession)
        {
            case 0:
                text.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}",
                "骑士", characterProperty.ATK.ToString(), characterProperty.PR.ToString(), characterProperty.MR.ToString(), characterProperty.SpeedThisRound.ToString());
                break;
            case 1:
                text.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}",
                "弓箭手",characterProperty.ATK.ToString(),characterProperty.PR.ToString(),characterProperty.MR.ToString(),characterProperty.SpeedThisRound.ToString());
                break;
            case 2:
                text.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}",
                "牧师", characterProperty.ATK.ToString(), characterProperty.PR.ToString(), characterProperty.MR.ToString(), characterProperty.SpeedThisRound.ToString());
                break; 
            case 3:
                text.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}",
                "法师", characterProperty.ATK.ToString(), characterProperty.PR.ToString(), characterProperty.MR.ToString(), characterProperty.SpeedThisRound.ToString());
                break;
            default: 
                break;
        }
    }
          
}
