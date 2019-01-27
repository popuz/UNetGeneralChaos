using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class GbItem : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon = null;
    public GbItemPickup pickupPrefab;
    
    public virtual void Use () 
    {
        Debug.Log( "Using " + name);
    }
}