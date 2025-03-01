using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationManager : MonoBehaviour
{
    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void ActionCountPlus()
    {
        DataofAttackers.ActionCount++;
    }
}
