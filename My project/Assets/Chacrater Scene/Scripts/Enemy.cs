using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;//初始生命值
    public int attack;//攻击力
    public int physicalResistivity;//物理抗性
    public int magicalResistivity;//魔法抗性
    public int speed;//速度
    public int currentHealth;
    public void beDamaged(int damage)
    {
        currentHealth -= damage;
    }
}
