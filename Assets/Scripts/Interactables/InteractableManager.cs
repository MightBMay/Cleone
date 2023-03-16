using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Interactables;

namespace MightBMaybe.Cleone.Interactables
{
    public class InteractableManager : MonoBehaviour
    {
        public static InteractableManager instance;
        public Dictionary<string, Activatable> Interacables = new Dictionary<string, Activatable>();
        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
            Interacables.Add("Door", new Door());
            Interacables.Add("Fan", new Fan());
        }

        public Activatable GetActivatable(string name)
        {
            Activatable activ;
            Interacables.TryGetValue(name, out activ);
            return activ;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }



}
