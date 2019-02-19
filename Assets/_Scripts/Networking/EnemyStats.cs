
public class EnemyStats : UnitStats
{    
    public override void OnStartServer () {
        curHealth = _maxHealth;
    }
}
