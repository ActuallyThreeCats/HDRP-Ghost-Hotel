using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.UI;

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
    public GraphicRaycaster computerCanvas;

    private int firstPersonPriority;
    private int thirdPersonPriority;

    private void Awake()
    {
        //ID.gameObject.GetComponentInParent<GraphicRaycaster>().enabled = false;

        Instance = this;
        ID.SetActive(false);
        computerCanvas.enabled = false;
    }

    public void Checkout(GuestController guest)
    {
        GuestManager.Instance.occupants.Remove(guest);
    }

    public void StartCheckInState(GuestController guest)
    {
        //ID.gameObject.GetComponentInParent<GraphicRaycaster>().enabled = true;
        computerCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCheckingGuestIn = true;
        firstPersonPriority = input.firstPerson.Priority;
        thirdPersonPriority = input.thirdPerson.Priority;
        

        deskCam.Priority = 3;
        ID.SetActive(true);
       
        signatureText.text = guest.guestName;
        nameOnIDText.text = guest.guestName;
        ageText.text = guest.GetGuestAge().ToString();
    }

    public void EndCheckInState()
    {
        computerCanvas.enabled = false;
        //ID.gameObject.GetComponentInParent<GraphicRaycaster>().enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCheckingGuestIn = false;
        ID.SetActive(false);
        if(firstPersonPriority > thirdPersonPriority)
        {

            deskCam.Priority = 0;
        }
        if(thirdPersonPriority > firstPersonPriority)
        {

            deskCam.Priority = 0;
        }
    }
}
