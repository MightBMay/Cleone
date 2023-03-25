using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MightBMaybe.Cleone.Interactables
{
    public class Button : MonoBehaviour
    {
        ButtonType bt;
        public EventTrigger et;
        [Tooltip("Type the type of button (ie what activates the button)\n\nPlayer:    Player, Clones \nClone:   Clones \nHeavy: heavy clones ")]
        public string buttonType;
        public bool isActivated;



        // Start is called before the first frame update
        void Start()
        {
            et = GetComponent<EventTrigger>();
            AssignButtonType(buttonType);

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void AssignButtonType(string val)
        {
            InteractableManager.instance.ButtonTypes.TryGetValue(val, out bt);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(et.trigger.isTriggered == true) { return; }
            bool canInteract = false;
            foreach (string s in bt.GetTags())
            {
                if (collision.collider.CompareTag(s)) { canInteract = true; break; }
            }
            if (canInteract)
            {
                et.trigger.isTriggered = true;
                
                //make activation sequence.
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (et.trigger.isTriggered == false) { return; }
            bool canInteract = false;
            foreach (string s in bt.GetTags())
            {
                if (collision.collider.CompareTag(s)) { canInteract = true; break; }
            }
            if (canInteract)
            {
                et.trigger.isTriggered = false;
                //make deactivation sequence.
            }
        }
    }
    public class ButtonType
    {
        public string[] ActivatableTags;
        public virtual string[] GetTags()
        {
            return ActivatableTags;
        }
    }
    public class PlayerButton : ButtonType
    {
        public PlayerButton()
        {
            ActivatableTags = new string[] { "Player", "Clone" };
        }
    }
    public class CloneButton : ButtonType
    {
        public CloneButton()
        {
            ActivatableTags = new string[] { "HeavyClone", "Clone" };
        }
    }
    public class HeavyButton : ButtonType
    {
        public HeavyButton()
        {
            ActivatableTags = new string[] { "HeavyClone" };
        }
    }

}