using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private TextMeshProUGUI walletAmount;
    [SerializeField] private TextMeshProUGUI bankAmount;
    [SerializeField] private TextMeshProUGUI hotelAmount;

    // Start is called before the first frame update
    void Start()
    {
        moneyManager.OnPlayerWalletChanged += MoneyManager_OnPlayerWalletChanged;
        moneyManager.OnPlayerBankChanged += MoneyManager_OnPlayerBankChanged;
        moneyManager.OnHotelBankChanged += MoneyManager_OnHotelBankChanged;
    }

    private void MoneyManager_OnHotelBankChanged(object sender, MoneyManager.OnWalletChangedEventArgs e)
    {
        hotelAmount.text = e._amt.ToString();
    }

    private void MoneyManager_OnPlayerBankChanged(object sender, MoneyManager.OnWalletChangedEventArgs e)
    {
        bankAmount.text = e._amt.ToString();
    }

    private void MoneyManager_OnPlayerWalletChanged(object sender, MoneyManager.OnWalletChangedEventArgs e)
    {
        walletAmount.text = e._amt.ToString();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
