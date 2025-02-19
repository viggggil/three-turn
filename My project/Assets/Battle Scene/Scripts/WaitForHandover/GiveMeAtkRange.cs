using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMeAtkRange : MonoBehaviour
{
    CharacterProperty characterProperty;

    public int[] SampleAtkRange;

    private void Awake()
    {
        characterProperty = GetComponent<CharacterProperty>();
    }

    int ToFront()
    {
        return (characterProperty.Position + 6);
    }
}
