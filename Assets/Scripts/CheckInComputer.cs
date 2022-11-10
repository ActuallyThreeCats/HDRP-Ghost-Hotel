using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CheckInComputer : MonoBehaviour
{
    public static CheckInComputer Instance;
    public List<Button> roomButtons = new List<Button>();
    public List<RoomInfo> rooms = new List<RoomInfo>();
    public GameObject roomInfoPanel;
    public TextMeshProUGUI roomNumberText;
    public CheckInManager checkIn;
    public Button confirm;

    public RoomInfo roomInfo;

    [Header("Reservation Status")]
    public TextMeshProUGUI reservationText;

    [Space]
    public TextMeshProUGUI firstNameInput;
    public TextMeshProUGUI lastNameInput;
    public TextMeshProUGUI ageInput;
    public TextMeshProUGUI signatureInput;
    public TextMeshProUGUI dueAmt;





    PlayerControls controls;
    PlayerControls.DefaultActions actions;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().controls;
        actions = controls.Default;
        actions.Cancel.performed += Cancel_performed;
        

    }

    private void Cancel_performed(InputAction.CallbackContext obj)
    {
        CheckInManager.Instance.EndCheckInState();
    }

    private void Update()
    {
        if (CheckInManager.Instance.isCheckingGuestIn)
        {
            UpdateRooms();
        }
    }
    private void UpdateRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].occupied)
            {
                rooms[i].checkInButton.interactable = false;
            }
            else
            {
                rooms[i].checkInButton.interactable = true;
            }
        }
    }

    public void CheckInRoom()
    {

        
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
            Debug.Log("Checking in attempt");
            roomInfo.Patron = CheckInManager.Instance.guestsInQueue[0].gameObject;
            roomInfo.SetDaysScheduled(CheckInManager.Instance.guestsInQueue[0].GetScheduledDays());
            CheckInManager.Instance.guestsInQueue[0].SwitchState(CheckInManager.Instance.guestsInQueue[0].roomState);
            CheckInManager.Instance.guestsInQueue.Remove(CheckInManager.Instance.guestsInQueue[0]);
            CheckInManager.Instance.inUse = false;
            CheckInManager.Instance.isTargeted = false;


        }

    }

    public void CheckRoomStatus(string roomName)
    {
        roomInfoPanel.SetActive(true);
       
        for (int i = 0; i < rooms.Count; i++)
        {
            Debug.Log("Running Loop");
            if(rooms[i].name == roomName)
            {
                if (rooms[i].occupied)
                {
                    rooms[i].roomState = RoomEnum.occupied;
                }
                else
                {
                    rooms[i].roomState = RoomEnum.goodMatch;
                }
                
                roomNumberText.text = "Room " + rooms[i].roomNumber.ToString();
                dueAmt.text = "Due: " + rooms[i].GetRoomCost().ToString();
                roomInfo = rooms[i];



                switch (rooms[i].roomState)
                {
                    case RoomEnum.occupied:
                        reservationText.text = "Occupied";

                        break;
                    case RoomEnum.reserved:
                        reservationText.text = "Reserved";

                        break;
                    case RoomEnum.ghostHaunting:
                        reservationText.text = "Ghost Haunting";

                        break;
                    case RoomEnum.unknownThreat:
                        reservationText.text = "Unknown Threat";

                        break;
                    case RoomEnum.goodMatch:
                        reservationText.text = "Good Match";

                        break;
                    case RoomEnum.badMatch:
                        reservationText.text = "Bad Match";

                        break;
                    default:
                        break;
                }

            }
        }

    }
}
