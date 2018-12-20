using UnityEngine;
using UNetGeneralChaos;

public class TrapBehaviour : MonoBehaviour
{
    [SerializeField]
    private TrapTargetType _trapTargetType;

    private Trap trap;

    private void Awake()
    {
        trap = new Trap();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ICharacter>();
        trap.HandleCharacterEnetered(player, _trapTargetType);
    }
}

public class Trap
{
    public void HandleCharacterEnetered(ICharacter unitMover, TrapTargetType trapTargetType)
    {
        if (unitMover.IsPlayer)
        {
            if (trapTargetType == TrapTargetType.Player)
                unitMover.Health--;
        }
        else
        {
            if (trapTargetType == TrapTargetType.Npc)
                unitMover.Health--;
        }
    }
}

public enum TrapTargetType { Player, Npc }
