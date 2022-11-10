using UnityEngine;
using Crafty.Systems.StateMachine;

public class CheckInState : State
{
    public override void EnterState(GuestController guest)
    {
        Debug.Log("Entered CheckIn State");
        for (int i = 0; i < GuestManager.Instance.waypoints.Count; i++)
        {
            if(GuestManager.Instance.waypoints[i].name == "DeskQueue")
            {
                guest.SetWaypoint(GuestManager.Instance.waypoints[i].transform.position);
                guest.agent.SetDestination(guest.GetWaypoint());
                CheckInManager.Instance.isTargeted = true;
            }
        }
        Debug.Log("Finished CheckIn State");

    }

    public override void UpdateState(GuestController guest)
    {
        if (guest.agent.remainingDistance < 0.1f)
        {
            CheckInManager.Instance.inUse = true;
            CheckInManager.Instance.signatureText.text = guest.guestName;
            CheckInManager.Instance.nameOnIDText.text = guest.guestName;
            CheckInManager.Instance.ageText.text = guest.GetGuestAge().ToString();
        }
    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }
}
