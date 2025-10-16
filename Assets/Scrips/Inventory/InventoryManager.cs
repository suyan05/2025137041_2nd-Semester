using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Inventory Settings")]
    public int inventorySize = 20;
    public GameObject inventorUI;
    public Transform itemSlotParent;
    public GameObject itemSletPrefab;

    [Header("Input")]
    public KeyCode inventoryKey = KeyCode.I;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    private bool isInventoryOpen = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CreateInventorySlots();
        inventorUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }
    }

    private void CreateInventorySlots()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject newSlot = Instantiate(itemSletPrefab, itemSlotParent);
            InventorySlot slot = newSlot.GetComponent<InventorySlot>();
            if (slot != null)
            {
                inventorySlots.Add(slot);
            }
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventorUI.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    public bool AddItem(ItemData item, int amount = 1)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.item == item && slot.amount < item.id)
            {
                int spaceLeft = item.id - slot.amount;
                int amountToAdd = Mathf.Min(amount, spaceLeft);
                slot.AddAmount(amountToAdd);

                amount -= amountToAdd;

                if (amount <= 0)
                {
                    return true;
                }
            }
        }

        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.item == null)
            {
                slot.SetItem(item, amount);
                return true;
            }
        }

        Debug.Log("No empty slot available");
        return false;
    }

    public void RemoveItem(ItemData item, int amount = 1)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.item == item)
            {
                slot.RemoveAmount(amount);
                return;
            }
        }
    }

    public int GetItemAmount(ItemData item)
    {
        int totalAmount = 0;
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.item == item)
            {
                totalAmount += slot.amount;
            }
        }
        return totalAmount;
    }
}
