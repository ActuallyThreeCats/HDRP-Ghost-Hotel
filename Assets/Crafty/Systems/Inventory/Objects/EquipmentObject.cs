using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Crafty/Inventory System/Items/Equipment Object")]
public class EquipmentObject : ItemObject
{
    public float genericBonus;
    public int levelRequirement;

    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
