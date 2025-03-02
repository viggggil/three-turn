using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cleric : MonoBehaviour
{
    CharacterProperty charaterProperty;

    private void Start()
    {
        charaterProperty = this.GetComponent<CharacterProperty>();
    }

    public void Skills(int skillCode, int position)
    {//技能在这里生效，这两个参数之后有逻辑传入
        switch (skillCode)
        {
            case 0:
                skill1(position);
                break;
            case 1:
                skill2(position);
                break;
            case 2:
                //skill3();
                break;
            default:
                break;
        }
    }

    public List<int> skill(int position)
    {
        List<int> list = new List<int>();
        list.Add(position);
        return list;
    }
    public void skill1(int position)
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty teammateProperty = node.GetComponentInChildren<CharacterProperty>();
        teammateProperty.HP += charaterProperty.ATK;
    }

    public void skill2(int position)
    {
        GameObject node;
        if(position < 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out node);
                if (node == null)
                    continue;
                CharacterProperty teammateProperty = node.GetComponentInChildren<CharacterProperty>();
                teammateProperty.HP += (int)(charaterProperty.ATK * 0.6f);
            }
        }
        else if(position < 6)
        {
            for (int i = 3; i < 6; i++)
            {
                Spawner.nodeDictionary.TryGetValue(i, out node);
                if (node == null)
                    continue;
                CharacterProperty teammateProperty = node.GetComponentInChildren<CharacterProperty>();
                teammateProperty.HP += (int)(charaterProperty.ATK * 0.6f);
            }
        }
    }

    public void skill3(int position)
    {
        GameObject node;
        Spawner.nodeDictionary.TryGetValue(position, out node);
        CharacterProperty teammateProperty = node.GetComponentInChildren<CharacterProperty>();
        for (int i = 0; i < teammateProperty.Buffs.Count; i++)
        {
            if (teammateProperty.Buffs[i].BuffType == BuffType.Debuff)
                teammateProperty.Buffs.Remove(teammateProperty.Buffs[i]);
        }
    }
}
