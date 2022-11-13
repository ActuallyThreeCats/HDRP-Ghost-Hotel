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
    public TextMeshProUGUI daysRemaining;

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
    public void UpdateRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].occupied)
            {
                ColorBlock cb = rooms[i].checkInButton.colors;
                cb.normalColor = new Color(200, 0, 0);
                rooms[i].checkInButton.colors = cb;

            }
            else
            {
                rooms[i].roomState = RoomEnum.goodMatch;
            }
            if(rooms[i].roomState == RoomEnum.goodMatch)
            {
                ColorBlock cb = rooms[i].checkInButton.colors;
                cb.normalColor = new Color(0, 150, 0);
                rooms[i].checkInButton.colors = cb;
            }
        }
    }

    public void CheckInRoom()
    {
        if(signature != null)
        {

                //Debug.Log("Checking in attempt");
                roomInfo.Patron = CheckInManager.Instance.guestsInQueue[0].gameObject;
                roomInfo.SetDaysScheduled(CheckInManager.Instance.guestsInQueue[0].GetScheduledDays());
                roomInfo.ResetDaysRemaining();
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
                UpdateRooms();
            CheckInManager.Instance.ID.SetActive(false);
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

                    confirm.interactable = false;
                    
                }
                else
                {
                    rooms[i].roomState = RoomEnum.goodMatch;
                    roomNumberText.text = "Room " + rooms[i].roomNumber.ToString();
                    dueAmt.text = "Due: " + rooms[i].GetRoomCost().ToString();
                    roomInfo = rooms[i];
                    daysRemaining.enabled = false;
                }
                




                switch (rooms[i].roomState)
                {
                    case RoomEnum.occupied:
                        reservationText.text = "Occupied";
                        reservationText.GetComponentInParent<Image>().color = Color.red;
                        inputAge.GetComponentInChildren<TextMeshProUGUI>().text = rooms[i].Patron.GetComponent<GuestController>().GetGuestAge().ToString();
                        string[] nameSplit = rooms[i].Patron.GetComponent<GuestController>().guestName.Split(" ");
                        inputName.GetComponentsInChildren<TextMeshProUGUI>()[0].text = nameSplit[0];
                        inputName.GetComponentsInChildren<TextMeshProUGUI>()[1].text = nameSplit[1];
                        signature.GetComponentInChildren<TextMeshProUGUI>().text = rooms[i].Patron.GetComponent<GuestController>().guestName;
                        roomNumberText.text = "Room " + rooms[i].roomNumber.ToString();
                        dueAmt.text = "Paid: " + rooms[i].GetRoomCost().ToString();
                        daysRemaining.enabled = true;
                        daysRemaining.text = "Days Left: " + rooms[i].GetDaysRemaining().ToString();
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
                        reservationText.GetComponentInParent<Image>().color = Color.green;
                        inputAge.GetComponentInChildren<TextMeshProUGUI>().text = null;
                        inputName.GetComponentsInChildren<TextMeshProUGUI>()[0].text = null;
                        inputName.GetComponentsInChildren<TextMeshProUGUI>()[1].text = null;
                        signature.GetComponentInChildren<TextMeshProUGUI>().text = null;
                        daysRemaining.enabled = false;
                        dueAmt.text = "Due :";
                        break;
                    case RoomEnum.badMatch:
                        reservationText.text = "Bad Match";

                        break;
                    default:
                        break;
                }

            }

            if (!rooms[i].occupied)
            {
                rooms[i].roomState = RoomEnum.goodMatch;
            }

        }

    }

    private void InputName()
    {
        if (!roomInfo.occupied)
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
    }

    private void InputAge()
    {
        if (!roomInfo.occupied)
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
    }

    private void InputSignature()
    {
        if (!roomInfo.occupied)
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
}
