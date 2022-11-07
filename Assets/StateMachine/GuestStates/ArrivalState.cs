using UnityEngine;
using Crafty.Systems.StateMachine;

public class ArrivalState : State
{
   public override void EnterState(GuestController guest)
    {
        guest.SetMoney(Random.Range(300, 5000));
        GenerateGuestID(guest);
        GuestManager.Instance.totalGuests.Add(guest);
        CheckInManager.Instance.guestsInQueue.Add(guest);

        Debug.Log("Spawned guest with $"+ guest.GetMoney() + " with ID of: " + guest.GetGuestID());
    }

    public override void UpdateState(GuestController guest)
    {
        if (!CheckInManager.Instance.isTargeted)
        {
          
            guest.SwitchState(guest.checkInState);
        }else
        {
            guest.SwitchState(guest.queueingState);
        }
    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }

    public void GenerateGuestID(GuestController guest)
    {
        guest.SetGuestID(Random.Range(1, 2000000));

    }
}
