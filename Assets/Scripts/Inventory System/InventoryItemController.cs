using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public GameObject player;

    // ������� ����� � ��������� � ��������
    public void AddItem(Item newItem, GameObject playerGameObject)
    {
        item = newItem;
        player = playerGameObject;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Key:
                // To Do ����� �����-�� ��������� ������
                Debug.Log("Trying to use key from inventory");
                break;
        }

    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }
}
