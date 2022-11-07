using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InventoryObject inventory;
    [SerializeField] private SlotContainer slotContainer;
    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item)
        {
            //inventory.AddItem(item.item, 1);
            slotContainer.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }



    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
