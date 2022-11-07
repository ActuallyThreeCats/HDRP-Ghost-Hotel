using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KeyDate", menuName = "Crafty/TimeSystem/KeyDate")]
public class KeyDateObject : ScriptableObject
{
    public int holdayID;
    public bool yearly;
    public int month;
    public int dayInMonthStart;
    public int dayInMonthEnd;
    public string eventName;
    public string eventDescription;
    public Sprite icon;


}
