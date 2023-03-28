using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Clones;
using MightBMaybe.Cleone.Player;
namespace MightBMaybe.Cleone.Interactables {

    public class Powerup : MonoBehaviour
    {
        CloneManager cm;
        [Tooltip("insert name of the type of clone:\nNonRigid: does not interact with player \nRigid: Interacts with player\nHeavy: rigid clone that works with heavy buttons.")]
        public string TypeName;
        [SerializeField] CloneTypes cloneType;
        private void Start()
        {
            cm = CloneManager.cloneManager;
            cm.cloneTypes.TryGetValue(TypeName, out cloneType);

            Color32 color = (cloneType != null) ? cloneType.cloneColour : new Color32(0, 0, 0, 1);
            GetComponent<MeshRenderer>().material.color = color;




        }

        /// <summary>
        /// Collects powerup and assigns its properties to the player.
        /// </summary>

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player") && cloneType != null)
            {

                PlayerStats.pStats.CStats.CurrentCloneType = cloneType;
                GetComponentInParent<PowerUpCollecter>().Collect();

            }
        }
    } 
}
