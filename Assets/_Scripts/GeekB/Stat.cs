﻿using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int _baseValue;

    public int GetValue() =>_baseValue;    
}
