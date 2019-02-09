using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StatsManager : NetworkBehaviour
{
    [SyncVar] public int damage, armor, moveSpeed;
    [SyncVar] public int level, statPoints;
    [SyncVar] public float exp, nextLevelExp;

    public Player player;

    [Command]
    public void CmdUpgradeStat(int stat)
    {
        if (player.progress.RemoveStatPoint())
        {
            switch (stat)
            {
                case (int) StatType.Damage:
                    player.character.Stats.damage.baseValue++;
                    break;
                case (int) StatType.Armor:
                    player.character.Stats.armor.baseValue++;
                    break;
                case (int) StatType.MoveSpeed:
                    player.character.Stats.moveSpeed.baseValue++;
                    break;
            }
        }
    }
}

public enum StatType
{
    Damage,
    Armor,
    MoveSpeed
}