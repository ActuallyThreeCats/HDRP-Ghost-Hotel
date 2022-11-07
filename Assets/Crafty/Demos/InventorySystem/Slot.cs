using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private int amount;
    public TextMeshProUGUI amountText;
    public Image icon;
    
    public ItemObject item;

    // Start is called before the first frame update
    void Start()
    {

        icon.color = new Color(1, 1, 1, 0);
        amountText.enabled = false;
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(ItemObject item)
    {
        this.item = item;
    }

    public void AddAmount(int amount)
    {
        this.amount += amount;
        amountText.text = this.amount.ToString();
    }
}
