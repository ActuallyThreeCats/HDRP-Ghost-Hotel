using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    bool canInteract = false;
    [SerializeField] GameObject interactableObject;
    [SerializeField] InputManager inputManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = true;
            interactableObject = other.gameObject;
        }
        else
        {
            canInteract = false;
            interactableObject = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactableObject = null;
            canInteract = false;
        }
    }

    public void Interact()
    {
        switch (interactableObject.GetComponent<Interactable>().interactable)
        {
            case Interactable.Interactables.CheckInMat:
                UseCheckInMat();
                break;
            default:
                break;
        }
    }

    private void UseCheckInMat()
    {
        if (interactableObject == null)
        {
            return;
        }

        if (CheckInManager.Instance.inUse)

        {
            CheckInManager.Instance.StartCheckInState(CheckInManager.Instance.guestsInQueue[0]);
            
            /*
                if (GuestManager.Instance.occupants.Count == VacancyManager.Instance.roomInfo.Count)

                {            
                    GuestManager.Instance.totalGuests.Remove(CheckInManager.Instance.guestsInQueue[0]);               
                    Destroy(CheckInManager.Instance.guestsInQueue[0].gameObject);                
                    CheckInManager.Instance.guestsInQueue.Remove(CheckInManager.Instance.guestsInQueue[0]);                
                    CheckInManager.Instance.inUse = false;               
                    CheckInManager.Instance.isTargeted = false;                
                }            
                else            
                {           
                    CheckInManager.Instance.guestsInQueue[0].SwitchState(CheckInManager.Instance.guestsInQueue[0].roomState);                
                    CheckInManager.Instance.guestsInQueue.Remove(CheckInManager.Instance.guestsInQueue[0]);
                    CheckInManager.Instance.inUse = false;             
                    CheckInManager.Instance.isTargeted = false;

                }

                }
            */
        }
    }
}
