using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotContainer : MonoBehaviour
{
    [SerializeField] private List<Slot> slots = new List<Slot>();
    [SerializeField] private InventoryObject inventory;
    [SerializeField] private int maxItems;
    [SerializeField] private GameObject slotPrefab;

    private void Start()
    {
        for (int i = 0; i < maxItems-1; i++)
        {
            GameObject newGameObject = Instantiate(slotPrefab, gameObject.transform);
            slots.Add(newGameObject.GetComponent<Slot>());
            
        }
        inventory.OnLoad += Inventory_OnLoad;
        
    }

    private void Inventory_OnLoad(object sender, System.EventArgs e)
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            PlaceItem(inventory.Container[i].item, inventory.Container[i].amount);
        }
    }

    public void AddItem(ItemObject item, int amount )
    {
        if(slots.Count < maxItems)
        {
            inventory.AddItem(item, amount);

            PlaceItem(item, amount);
        }

        
       
    }
    public void PlaceItem(ItemObject item, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item && slots[i].item.stackable)
            {
                slots[i].AddAmount(amount);
                return;
            }
            else if (slots[i].item == null)
            {
                slots[i].amountText.enabled = true;
                slots[i].item = item;
                slots[i].icon.sprite = item.sprite;
                slots[i].icon.color = new Color(1, 1, 1, 1);
                slots[i].AddAmount(amount);
                return;
            }
        }
    }
}
