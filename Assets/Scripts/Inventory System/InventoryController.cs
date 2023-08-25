using UnityEngine;
using Player;

public class InventoryController : MonoBehaviour
{
    [Header("PlayerController")] public FirstPersonController PersonController;
    [Header("InventoryManager")] public InventoryManager InventoryManager;

    [Header("Inventory")] [SerializeField] private GameObject _inventory;

    public void OpenInventory()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        PersonController.LockCamera();

        if (!_inventory.activeSelf)
        {
            _inventory.SetActive(true);
            InventoryManager.ListItems();
        }
    }
}
