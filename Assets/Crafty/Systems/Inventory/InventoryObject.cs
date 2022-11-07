/* https://youtu.be/_IqTeruf3-s 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Crafty/Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    private string savePath;
    [SerializeField] private string saveName;
    private string saveNameFile;
    private string assetPath;
    public event EventHandler OnLoad;
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();


    private void OnEnable()
    {
        saveNameFile = "/" + saveName;
        assetPath = "Assets/Crafty/Demos/InventorySystem/Resources/ItemDatabase.asset";
        savePath = "/" + saveName + "/inventory.txt";

#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Crafty/Demos/InventorySystem/Resources/ItemDatabase.asset");
#endif

    }

    public void AddItem(ItemObject item, int amount)
    {
        Debug.Log(this + " AddItem");
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == item && Container[i].item.stackable)
            {
                Container[i].AddAmount(amount);
                
                return;
                
            }
            
        }
            Container.Add(new InventorySlot(database.getID[item], item, amount));
            
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        Directory.CreateDirectory(string.Concat(Application.persistentDataPath, saveNameFile));
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
       

    }
    public void Load()
    {
        
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
            OnLoad?.Invoke(this, EventArgs.Empty);
        }

    }


    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.getItem[Container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {
       
    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySlot(int ID, ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
        this.ID = ID;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}