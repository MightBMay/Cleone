using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MightBMaybe.Cleone.Interactables
{
    public class EventTrigger : MonoBehaviour
    {
        public Trigger trigger = new Trigger();
        
    }
    public class Trigger
    {
        public bool isTriggered;
        public Trigger()
        {
            isTriggered = false;
        }
        
        
    }
}
