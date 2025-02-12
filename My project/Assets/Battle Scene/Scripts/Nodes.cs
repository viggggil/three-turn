using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
   [SerializeField] int nodeCode;

   private bool isUnderAtk;//跟角色的移动有关，如果遭受攻击则该格不能移到；
   public bool isPlayerNode;

   public void NodeOnClick()
   {

   }
}
