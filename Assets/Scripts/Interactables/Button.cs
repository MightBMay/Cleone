using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MightBMaybe.Cleone.Interactables
{
    public class Button : MonoBehaviour
    {
        ButtonType bt;
        public EventTrigger et;
        [Tooltip("Type the type of button (ie what activates the button)\n\nPlayer:    Player, Clones \nClone:   Clones  ")]
        public string buttonType;
        [HideInInspector]
        public Dictionary<string, ButtonType> ButtonTypes = new Dictionary<string, ButtonType>();
        public bool isActivated;



        // Start is called before the first frame update
        void Start()
        {
            et = GetComponent<EventTrigger>();
            ButtonTypes.Add("Player", new PlayerButton());
            ButtonTypes.Add("Clone", new CloneButton());
            //ButtonTypes.Add("HeavyClone", 3);
            //ButtonTypes.Add("MultiClone", 4);
            AssignButtonType(buttonType);

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void AssignButtonType(string val)
        {
            ButtonTypes.TryGetValue(val, out bt);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(et.trigger.isTriggered == true) { return; }
            bool canInteract = false;
            foreach (string s in bt.GetTags())
            {
                if (collision.collider.CompareTag(s)) { canInteract = true; }
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
                if (collision.collider.CompareTag(s)) { canInteract = true; }
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
        public List<string> ActivatableTags;
        public virtual List<string> GetTags()
        {
            return ActivatableTags;
        }
    }
    public class PlayerButton : ButtonType
    {
        public PlayerButton()
        {
            ActivatableTags = new List<string>() { "Player", "Clone" };
        }
    }
    public class CloneButton : ButtonType
    {
        public CloneButton()
        {
            ActivatableTags = new List<string>() { "HeavyClone", "Clone" };
        }
    }
}