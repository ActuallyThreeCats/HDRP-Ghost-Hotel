using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [Header("Player Wallet")]
    [SerializeField] private int playerWallet;
    [SerializeField] private int maxPlayerWallet = Int32.MaxValue;

    [Header("Player Bank")]
    [SerializeField] private int playerBank;
    [SerializeField] private int maxPlayerBank = Int32.MaxValue;

    [Header("Hotel Bank")]
    [SerializeField] private long hotelBank;
    [SerializeField] private long maxHotelBank = long.MaxValue;

    public event EventHandler<OnWalletChangedEventArgs> OnPlayerWalletChanged;
    public event EventHandler<OnWalletChangedEventArgs> OnPlayerBankChanged;
    public event EventHandler<OnWalletChangedEventArgs> OnHotelBankChanged;

    public class OnWalletChangedEventArgs : EventArgs
    {
        public long _amt;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void FireWalletEvent(long amt)
    {
        OnPlayerWalletChanged?.Invoke(this, new OnWalletChangedEventArgs{
            _amt = amt,
            
        });
    }    
    private void FirePlayerBankEvent(long amt)
    {
        OnPlayerBankChanged?.Invoke(this, new OnWalletChangedEventArgs{
            _amt = amt,
            
        });
    }    
    private void FireHotelBankEvent(long amt)
    {
        OnHotelBankChanged?.Invoke(this, new OnWalletChangedEventArgs{
            _amt = amt,
            
        });
    }

    public int GetPlayerWallet()
    {
        return playerWallet ;
    }

    public int GetPlayerBank()
    {
        return playerBank;
    }

    public long GetHotelBank()
    {
        return hotelBank;
    }

    public void SetPlayerWallet(int amt)
    {
        if(amt < maxPlayerWallet)
        {
            playerWallet = amt;
            FireWalletEvent(playerWallet);

        }
    }

    public void SetPlayerBank(int amt)
    {
        if(amt < maxPlayerBank)
        {
            playerBank = amt;
            FirePlayerBankEvent(playerBank);
        }
    }

    public void SetHotelBank(long amt)
    {
        if(amt < maxHotelBank)
        {
            hotelBank = amt;
            FireHotelBankEvent(hotelBank);

        }
    }


    public void AddToPlayerWallet(int amt)
    {
        if (amt + playerWallet < maxPlayerWallet)
        {

            playerWallet += amt;
            FireWalletEvent(playerWallet);


        }
    }

    public void AddToPlayerBank(int amt)
    {
        if(amt+ playerBank < maxPlayerBank)
        {
            playerBank += amt;
            FirePlayerBankEvent(playerBank);


        }
    }

    public void AddToHotelBank(long amt)
    {
        if (amt + hotelBank < maxHotelBank)
        {
            Debug.Log("Added to Hotel Bank");
            hotelBank += amt;
            Debug.Log(hotelBank + " " + amt);
            FireHotelBankEvent(hotelBank);


        }
    }

    public void SubtractFromPlayerWallet(int amt)
    {
        if(amt < playerWallet)
        {
            playerWallet -= amt;
            FireWalletEvent(playerWallet);

        }
    }

    public void SubtractFromPlayerBank(int amt)
    {
        if(amt < playerBank)
        {
            playerBank -= amt;
            FirePlayerBankEvent(playerBank);

        }
    }

    public void SubtractFromHotelBank(long amt)
    {
        if(amt < hotelBank)
        {
            hotelBank -= amt;
            FireHotelBankEvent(hotelBank);

        }
    }
}
