using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationManager : MonoBehaviour
{


    public void DestroyCharacter()
    {
        // 获取父物体
        GameObject parentObject = transform.parent.gameObject;


        // 销毁父物体，由于子物体是父物体的一部分，销毁父物体时子物体也会被销毁

        Destroy(parentObject);
    }

    public void DestroyUI()
    {
        Destroy(gameObject);
    }

    public void ActionCountPlus()
    {
        DataofAttackers.ActionCount++;
    }
}
