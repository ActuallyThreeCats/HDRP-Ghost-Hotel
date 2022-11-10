using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CheckInComputer : MonoBehaviour
{
    public List<Button> roomButtons = new List<Button>();
    public List<RoomInfo> rooms = new List<RoomInfo>();

    private void Start()
    {

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

    public void CheckInRoom(RoomInfo room)
    {
        
    }
}
