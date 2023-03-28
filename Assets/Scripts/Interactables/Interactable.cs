using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MightBMaybe.Cleone.Interactables {

    public class Interactable : MonoBehaviour
    {
        public GameObject triggerObject;
        Activatable activatable;
        Trigger trigger;
        public string InteractableType;
        public List<GameObject> partList;

        private void Start()
        {

            trigger = triggerObject.GetComponent<EventTrigger>().trigger;

            Activatable type;
            partList = new List<GameObject>();
            partList = GetChildObjectsWithScript(gameObject, typeof(DoorInfo));
            type = InteractableManager.instance.GetActivatable(InteractableType);
            
            activatable = (Activatable)Activator.CreateInstance(type.GetType());
            activatable.trigger = trigger;

            activatable.AssignParts(partList);

          
        }
        private bool isActivated = false; 
        private void Update()
        {
            if (activatable.trigger.isTriggered && !isActivated)
            {
                activatable.Activate();
                isActivated = true;
            }
            else if (!activatable.trigger.isTriggered && isActivated)
            {
                activatable.Deactivate();
                isActivated = false;
            }
        }

        /// <summary>
        /// Gets a list of all child GameObjects with a specific script.
        /// </summary>
        /// <param name="parent">Parent object from which to search cildren.</param>
        /// <param name="scriptType">Script you are looking for Children to have.</param>
        /// <returns></returns>
        public List<GameObject> GetChildObjectsWithScript(GameObject parent, System.Type scriptType)
        {
            List<GameObject> result = new List<GameObject>();
            foreach (Transform child in parent.transform)
            {
                if (child.GetComponent(scriptType) != null)
                {
                    result.Add(child.gameObject);
                }
                result.AddRange(GetChildObjectsWithScript(child.gameObject, scriptType));
            }
            return result;
        }

    }
    public class Activatable
    {
        public Trigger trigger;
        public List<GameObject> ActiveParts;
        public Activatable()
        {
            
        }

        public virtual void Deactivate()
        {

        }
        public virtual void Activate()
        {
            
        }
        public virtual void AssignParts(List<GameObject> parts)
        {
            this.ActiveParts = parts;
        }
    } 
    public class Door : Activatable
    {
        
        public Door()
        {
            
        }

        public override void Deactivate()
        {
            foreach (GameObject g in ActiveParts)
            {
                g.GetComponent<DoorInfo>().MoveDoor();

            }
        }
        public override void Activate()
        {

            foreach(GameObject g in ActiveParts)
            {
                g.GetComponent<DoorInfo>().MoveDoor();
                
            }
        }

        
        
    }
    public class Fan : Activatable
    {
        
        public Fan()
        {
            
        }
        

        public override void Deactivate()
        {

        }
        public override void Activate()
        {
            
        }
    }
}
