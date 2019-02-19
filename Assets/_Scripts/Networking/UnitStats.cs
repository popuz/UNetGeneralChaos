using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618

public class UnitStats : NetworkBehaviour
{
    [SerializeField] protected int _maxHealth = 100;
    [SyncVar] private int _curHealth;

    public virtual int curHealth
    {
        get => _curHealth;
        protected set => _curHealth = value;
    }

    public Stat damage;
    public Stat armor;
    public Stat moveSpeed;


    public override void OnStartServer() => SetHealthByRate(1);

    public virtual void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        if (damage > 0)
        {
            curHealth -= damage;
            if (curHealth <= 0)
                curHealth = 0;
        }
    }

    public void SetHealthByRate(float rate) => curHealth = (int) (_maxHealth * rate);
}