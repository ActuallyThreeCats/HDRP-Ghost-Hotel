using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Consumable,
    Equipment,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    //public Image icon;
    public Sprite sprite;
    public ItemType type;
    public float sellValue, buyValue;
    public bool stackable;
    [TextArea(15, 20)]
    public string description;

}
