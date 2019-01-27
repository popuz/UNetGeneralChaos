using UnityEngine;

[CreateAssetMenu(fileName = "New Item Collection", menuName = "Inventory/Item Collection" ) ]
public class GbItemCollection : ScriptableObject
{
    public GbItem[] items = new GbItem[0];
}