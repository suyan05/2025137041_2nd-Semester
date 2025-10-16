using UnityEngine;

public class PickUpItem : InteractionObj
{
    public ItemData ItemData;
    public int amount = 1;

    public override void Interact()
    {
        base.Interact();

        if(InventoryManager.instance != null)
        {
            bool added = InventoryManager.instance.AddItem(ItemData, amount);

            if(added)
            {
                Destroy(gameObject);
            }
        }
    }
}
