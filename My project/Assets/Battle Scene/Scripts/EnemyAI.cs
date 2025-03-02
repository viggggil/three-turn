using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int RandomAction;

    CharacterProperty _characterproperty;
    BattleManager _battlemanager;

    public int targetPosition;

    private void Awake()
    {
        _battlemanager = GetComponent<ActioninBattleManager>().battleManager;
        _characterproperty = GetComponent<CharacterProperty>();

        BattleManager.ReadyStageStart += GetTarget;
        BattleManager.ReadyStageStart += ConfirmAcition;
    }

    public void GetTarget()
    {
        do
        {
            targetPosition = UnityEngine.Random.Range(0, 6);
        } while (! Spawner.nodeDictionary[targetPosition].GetComponent<Nodes>().isPlayerHere);
    }

    public void ConfirmAcition()
    {
        RandomAction = UnityEngine.Random.Range(0, 2);

        if (_battlemanager.RoundCount == 1)
        {//Ö´ÐÐ¹¥»÷
            _characterproperty.OnTheAttack = true;
            gameObject.GetComponent<ActioninBattleManager>().GonnaAttack(SkillRanges(_characterproperty.Code));//·ÖÅä¹¥»÷
            Spawner.nodeDictionary[_characterproperty.Position].GetComponent<Nodes>().DisplaySword();
        }
        else
        {
            _characterproperty.OnTheDefense = true;
            gameObject.GetComponent<ActioninBattleManager>().GonnaDefense();
            Spawner.nodeDictionary[_characterproperty.Position].GetComponent<Nodes>().DisplayShield();
        }
    }

    public List<int> SkillRanges(int enemycode)
    {
        List<int> Atkrange = new List<int>();

        switch (enemycode)
        {
            case 27:
                Atkrange = gameObject.GetComponent<Enemy1>().skill(targetPosition);
                break;
            case 24:
                Atkrange = gameObject.GetComponent<Enemy2>().skill(targetPosition);
                break;
            case 23:
                Atkrange = gameObject.GetComponent<Enemy3>().skill(targetPosition);
                break;
            case 28:
                Atkrange = gameObject.GetComponent<Enemy4>().skill(targetPosition);
                break;
            case 19:
                Atkrange = gameObject.GetComponent<Enemy5>().skill(targetPosition);
                break;
            case 21:
                Atkrange = gameObject.GetComponent<Enemy6>().skill(targetPosition);
                break;
            case 22:
                Atkrange = gameObject.GetComponent<Enemy7>().skill(targetPosition);
                break;
            default:
                break;
        }

        return Atkrange;
    }

    private void OnDestroy()
    {
        BattleManager.ReadyStageStart -= ConfirmAcition;
    }

    
}
