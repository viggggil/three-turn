using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : MonoBehaviour
{
    void skill1(GameObject enemy,int attack)//攻击一个敌人
    {
        Enemy e = enemy.GetComponent<Enemy>();
        int damage = attack - e.physicalResistivity;
        e.beDamaged(damage);
    }

    void skill2(GameObject enemy1,GameObject enemy2,GameObject enemy3,int attack)//攻击前方三个敌人
    {
        Enemy e1 = enemy1.GetComponent<Enemy>();
        Enemy e2 = enemy2.GetComponent<Enemy>();
        Enemy e3 = enemy3.GetComponent<Enemy>();
        int damage1 = (int)(attack * 0.5f - e1.physicalResistivity);
        int damage2 = (int)(attack * 0.5f - e2.physicalResistivity);
        int damage3 = (int)(attack * 0.5f - e3.physicalResistivity);
        e1.beDamaged(damage1);
        e2.beDamaged(damage2);
        e3.beDamaged(damage3);
    }
}
