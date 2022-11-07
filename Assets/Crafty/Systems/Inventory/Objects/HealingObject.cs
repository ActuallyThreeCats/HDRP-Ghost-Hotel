using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Healing Object", menuName = "Crafty/Inventory System/Items/Healing Object")]

public class HealingObject : ItemObject
{
    public int healValue;

    private void Awake()
    {
        type = ItemType.Consumable;
    }
}
