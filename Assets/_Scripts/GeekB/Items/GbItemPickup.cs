using UnityEngine;

public class GbItemPickup : GbInteractable
{
    public GbItem item;
    
    public override bool Interact(GameObject user) => PickUp(user);

    public bool PickUp(GameObject user)
    {
        var character = user.GetComponent<Character>();
        if (character != null && character.Inventory.Add(item))
        {
            Destroy(gameObject);
            return true;
        }
        else return false;
    }
}