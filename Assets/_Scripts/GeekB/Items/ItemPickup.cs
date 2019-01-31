using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override bool Interact(GameObject user) => PickUp(user);

    public bool PickUp(GameObject user)
    {
        Character character = user.GetComponent<Character>();

        if (character != null && character.player.inventory.AddItem(item))
        {
            Destroy(gameObject);
            return true;
        }

        else return false;
    }
    
    public float lifetime;

    private void Update()
    {
        if (isServer) {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0) Destroy(gameObject);
        }
    }
}