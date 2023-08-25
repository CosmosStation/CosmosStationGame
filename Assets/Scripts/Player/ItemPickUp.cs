using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class ItemPickUp : InteractableObject
{
    public Item Item;

    public override void InteractStart(RaycastHit hit)
    {
        base.InteractStart(hit);
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }
}
