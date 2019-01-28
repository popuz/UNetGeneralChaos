using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(UnitStats))]
public class GbCombat : NetworkBehaviour
{
    [SerializeField] private float attackSpeed = 1f;
    private float attackCooldown = 0f;
    private UnitStats myStats;

    public delegate void CombatDenegate();
    [SyncEvent] public event CombatDenegate EventOnAttack;

    private void Start() => myStats = GetComponent<UnitStats>();

    private void Update() => attackCooldown -= attackCooldown > 0 ? Time.deltaTime : 0f;

    public bool Attack(UnitStats targetStats)
    {        
        if (!(attackCooldown <= 0)) return false;
                
        Debug.Log(name + " attacks " + targetStats.gameObject.name);
        targetStats.TakeDamage(myStats.Damage.GetValue());
        EventOnAttack?.Invoke();
        attackCooldown = 1f / attackSpeed;
        return true;

    }
}