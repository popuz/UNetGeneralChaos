using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618

public class UnitStats : NetworkBehaviour
{   
    [SerializeField] private int _maxHealth = 100;
    [SyncVar] private int _curHealth;

    public Stat damage;
    public Stat armor;
    public Stat moveSpeed;    
    
    public int CurHealth => _curHealth;

    public override void OnStartServer() => SetHealthByRate(1);


    public virtual void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        if (damage > 0)
        {
            _curHealth -= damage;
            if (_curHealth <= 0) 
                _curHealth = 0;
        }
    }
        
    public void SetHealthByRate(float rate) => _curHealth = (int) (_maxHealth * rate);
}