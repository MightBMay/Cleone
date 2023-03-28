using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Interactables;

namespace MightBMaybe.Cleone.Interactables
{
    public class InteractableManager : MonoBehaviour
    {
        public static InteractableManager instance;
        /// <summary>
        /// list of all interactable types.
        /// </summary>
        public Dictionary<string, Activatable> Interacables = new Dictionary<string, Activatable>();
        /// <summary>
        /// list of all button types.
        /// </summary>
        public Dictionary<string, ButtonType> ButtonTypes = new Dictionary<string, ButtonType>();
        
        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
            InstantiateActivatables();
            InstantiateButtons();


           
        }
        /// <summary>
        /// Fills Interactables with each interactable type to use in other scripts.
        /// </summary>
        private void InstantiateActivatables()
        {
            Interacables.Add("Door", new Door());
            Interacables.Add("Fan", new Fan());
        }
        /// <summary>
        /// Fills ButtonTypes with each button type to use in other scripts.
        /// </summary>
        private void InstantiateButtons()
        {
            ButtonTypes.Add("Player", new PlayerButton());
            ButtonTypes.Add("Clone", new CloneButton());
            ButtonTypes.Add("Heavy", new HeavyButton());
        }
        /// <summary>
        /// Gets an activatable type from the Interactables dictionary.
        /// </summary>
        /// <param name="name">Name of interactable you are trying to get.</param>
        /// <returns>Activatable of type with name "Name" </returns>
        public Activatable GetActivatable(string name)
        {
            Activatable activ;
            Interacables.TryGetValue(name, out activ);
            return activ;
        }

    }



}
