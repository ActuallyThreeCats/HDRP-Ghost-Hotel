using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class CheckInManager : MonoBehaviour
{
    public static CheckInManager Instance;
    [SerializeField] private InputManager input;
    [SerializeField] private CinemachineVirtualCamera deskCam;
    [SerializeField] private GameObject waypoint;
    public string fullName;
    public GameObject ID;
    public TextMeshProUGUI signatureText;
    public TextMeshProUGUI nameOnIDText;
    public TextMeshProUGUI ageText;
    public bool inUse;
    public bool isTargeted;
    public bool isCheckingGuestIn;
    public List<GuestController> guestsInQueue = new List<GuestController>();

    private int firstPersonPriority;
    private int thirdPersonPriority;

    private void Awake()
    {
        Instance = this;
        ID.SetActive(false);
    }

    public void Checkout(GuestController guest)
    {
        GuestManager.Instance.occupants.Remove(guest);
    }

    public void StartCheckInState(GuestController guest)
    {

        isCheckingGuestIn = true;
        firstPersonPriority = input.firstPerson.Priority;
        thirdPersonPriority = input.thirdPerson.Priority;
        
        input.firstPerson.Priority = 1;
        input.thirdPerson.Priority = 1;
        deskCam.Priority = 2;
        ID.SetActive(true);
       
        signatureText.text = guest.guestName;
        nameOnIDText.text = guest.guestName;
        ageText.text = guest.GetGuestAge().ToString();
    }

    public void EndCheckInState()
    {
        isCheckingGuestIn = false;
        ID.SetActive(false);
        if(firstPersonPriority > thirdPersonPriority)
        {
            input.firstPerson.Priority = 2;
            input.thirdPerson.Priority = 1;
            deskCam.Priority = 1;
        }
        if(thirdPersonPriority > firstPersonPriority)
        {
            input.firstPerson.Priority = 1;
            input.thirdPerson.Priority = 2;
            deskCam.Priority = 1;
        }
    }
}
