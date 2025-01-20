using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterProperty : MonoBehaviour
{
    [SerializeField] private int maxHealth;//初始生命值
    [SerializeField] private int attack;//攻击力
    [SerializeField] private int physicalResistivity;//物理抗性
    [SerializeField] private int magicalResistivity;//魔法抗性
    [SerializeField] private int speed;//速度
}
