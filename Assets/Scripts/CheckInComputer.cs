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

    [Space]
    [Header("Buttons")]
    public Button idName;
    public Button idAge;
    public Button inputName;
    public Button inputAge;
    public Button signature;

    private TextMeshProUGUI signatureText;

    private enum CheckInButton
    {
        Null,
        Name,
        Age
    }
    private CheckInButton checkInButton;


    PlayerControls controls;
    PlayerControls.DefaultActions actions;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        checkInButton = CheckInButton.Null;
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().controls;
        actions = controls.Default;
        actions.Cancel.performed += Cancel_performed;
        signatureText = signature.GetComponentInChildren<TextMeshProUGUI>();

        
        idName.onClick.AddListener(delegate 
        {
            InputName();
        });

        inputName.onClick.AddListener(delegate 
        {
            InputName();
        });

        idAge.onClick.AddListener(delegate
        {
            InputAge();
        });

        inputAge.onClick.AddListener(delegate
        {
            InputAge();
        });

        signature.onClick.AddListener(delegate
        {
            InputSignature();
        });


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
        if(CheckInManager.Instance.guestsInQueue.Count > 0)
        {
            if(signatureText.text == CheckInManager.Instance.guestsInQueue[0].guestName)
            {
                confirm.interactable = true;
            }
            else
            {
                confirm.interactable = false;
            }

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
        if(signature != null)
        {
            if (GuestManager.Instance.occupants.Count == VacancyManager.Instance.roomInfo.Count)

            {
                GuestManager.Instance.totalGuests.Remove(CheckInManager.Instance.guestsInQueue[0]);
                CheckInManager.Instance.guestsInQueue.Remove(CheckInManager.Instance.guestsInQueue[0]);
                Destroy(CheckInManager.Instance.guestsInQueue[0].gameObject);
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
                CheckInManager.Instance.guestsInQueue.TrimExcess();
                CheckInManager.Instance.inUse = false;
                CheckInManager.Instance.isTargeted = false;


                inputAge.GetComponentInChildren<TextMeshProUGUI>().text = null;
                inputName.GetComponentsInChildren<TextMeshProUGUI>()[0].text = null;
                inputName.GetComponentsInChildren<TextMeshProUGUI>()[1].text = null;
                signature.GetComponentInChildren<TextMeshProUGUI>().text = null;
                dueAmt.text = "Due :";
                roomInfoPanel.SetActive(false);

            }


        }
        

    }

    public void CheckRoomStatus(string roomName)
    {
        roomInfoPanel.SetActive(true);
       
        for (int i = 0; i < rooms.Count; i++)
        {
            //Debug.Log("Running Loop");
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

    private void InputName()
    {
        if (checkInButton == CheckInButton.Name)
        {
            string[] nameSplit = CheckInManager.Instance.guestsInQueue[0].guestName.Split(" ");
            firstNameInput.text = nameSplit[0];
            lastNameInput.text = nameSplit[1];
        }
        else
        {
            checkInButton = CheckInButton.Name;


        }
    }

    private void InputAge()
    {
        if (checkInButton == CheckInButton.Age)
        {
            ageInput.text = CheckInManager.Instance.guestsInQueue[0].GetGuestAge().ToString();
        }
        else
        {
            checkInButton = CheckInButton.Age;
        }
    }

    private void InputSignature()
    {
        if(ageInput.text != null)
        {
            if(firstNameInput.text != null)
            {
                signatureInput.text = firstNameInput.text + " " + lastNameInput.text;
            }
        }
    }
}
