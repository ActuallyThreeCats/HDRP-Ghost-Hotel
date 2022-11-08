using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PricesManager : MonoBehaviour
{
    public static PricesManager Instance;
    [SerializeField] private int vendingMachinePrice;

    private void Awake()
    {
        Instance = this;
    }
    public void SetVendingMachinePrice(int amt)
    {
        vendingMachinePrice = amt;
    }

    public int GetVendingMachinePrice()
    {
        return vendingMachinePrice;
    }
}
