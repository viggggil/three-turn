using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    void skill1(GameObject enemy, int attack)//¹¥»÷Ò»¸öµÐÈË
    {
        Enemy e = enemy.GetComponent<Enemy>();
        int damage = attack - e.physicalResistivity;
        e.beDamaged(damage);
    }
}
