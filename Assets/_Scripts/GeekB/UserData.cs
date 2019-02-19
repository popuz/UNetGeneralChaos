using System;
using System.Collections.Generic;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class UserData
{
    public Vector3 posCharacter;
    public List<int> inventory = new List<int>();
    public List<int> equipment = new List<int>();
    public int level, statPoints;
    public float exp, nextLevelExp;
    public int curHealth;
    public int statDamage, statArmor, statMoveSpeed;
}