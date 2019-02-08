using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public delegate void StatChanged(int value);
    public event StatChanged onStatChanged;

    [SerializeField] private int _baseValue;
    
    private List<int> modifiers = new List<int>();
    
    
    public int GetValue()
    {
        int finalValue = _baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;        
    }
    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
            onStatChanged?.Invoke(GetValue());
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
            onStatChanged?.Invoke(GetValue());
        }
    }

}