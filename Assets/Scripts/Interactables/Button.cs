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


        /// <summary>
        /// Assigns button type to this button.
        /// </summary>
        /// <param name="val">Name of button type.</param>
        public void AssignButtonType(string val)
        {
            InteractableManager.instance.ButtonTypes.TryGetValue(val, out bt);
        }
        /// <summary>
        /// Activates button when collision is entered with an object that shares its tag with this buttons ActivatableTags.
        /// </summary>
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
        /// <summary>
        /// Deactivates button when collision is exited with an object that shares its tag with this buttons ActivatableTags.
        /// </summary>

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
    /// <summary>
    /// base button class containing an array of tags the button can collide with.
    /// </summary>
    public class ButtonType
    {
        public string[] ActivatableTags;
        public virtual string[] GetTags()
        {
            return ActivatableTags;
        }
    }
    /// <summary>
    /// Player Buttons can be triggered by: Players, Clones
    /// </summary>
    public class PlayerButton : ButtonType
    {
        public PlayerButton()
        {
            ActivatableTags = new string[] { "Player", "Clone" };
        }
    }
    /// <summary>
    /// Clone Buttons can be triggered by: Clones, Heavy Clones
    /// </summary>
    public class CloneButton : ButtonType
    {
        public CloneButton()
        {
            ActivatableTags = new string[] { "HeavyClone", "Clone" };
        }
    }
    /// <summary>
    /// Heavy Buttons can be triggered by: Heavy Clones
    /// </summary>
    public class HeavyButton : ButtonType
    {
        public HeavyButton()
        {
            ActivatableTags = new string[] { "HeavyClone" };
        }
    }

}