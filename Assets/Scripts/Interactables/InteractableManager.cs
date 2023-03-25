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
        public Dictionary<string, ButtonType> ButtonTypes = new Dictionary<string, ButtonType>();
        
        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
            InstantiateActivatables();
            InstantiateButtons();


           
        }
        private void InstantiateActivatables()
        {
            Interacables.Add("Door", new Door());
            Interacables.Add("Fan", new Fan());
        }
        private void InstantiateButtons()
        {
            ButtonTypes.Add("Player", new PlayerButton());
            ButtonTypes.Add("Clone", new CloneButton());
            ButtonTypes.Add("Heavy", new HeavyButton());
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
