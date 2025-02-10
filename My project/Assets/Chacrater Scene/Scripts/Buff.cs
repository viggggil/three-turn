using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Buff : MonoBehaviour
{
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public BuffType BuffType { get; private set; }
    public Action<CharaterProperty> ApplyEffect { get; private set; }
    public Action<CharaterProperty> RemoveEffect { get; private set; }
        

    public Buff(string name, int duration, BuffType buffType, Action<CharaterProperty> applyEffect, Action<CharaterProperty> removeEffect)
    {
        Name = name;
        Duration = duration;
        BuffType = buffType;
        ApplyEffect = applyEffect;
        RemoveEffect = removeEffect;
    }

    public void Apply(CharaterProperty character)
    {
        ApplyEffect(character);
        Duration--;
    }

    public bool IsExpired()
    {
        return Duration <= 0;
    }

    public static void AddBuff(CharaterProperty character, Buff buff)
    {
        character.AddBuff(buff);
    }
}

public enum BuffType
{
    Buff,
    Debuff
}
