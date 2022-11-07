using System.Collections;
using UnityEngine;


namespace Crafty.Systems.StateMachine
{
    public abstract class State
    {
        public abstract void EnterState(GuestController guest);
        public abstract void UpdateState(GuestController guest);
        public abstract void OnCollisionEnter(GuestController guest);
    }
}

