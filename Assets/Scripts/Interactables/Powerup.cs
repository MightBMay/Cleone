using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Clones;
using MightBMaybe.Cleone.Player;
namespace MightBMaybe.Cleone.Interactables {

    public class Powerup : MonoBehaviour
    {
        // Define the enumeration for the five options
        CloneManager cm;
        [Tooltip("insert name of the type of clone:\nNonRigid: does not interact with player \nRigid: Interacts with player")]
        public string TypeName;
        [SerializeField] CloneTypes cloneType;
        private void Start()
        {
            cm = CloneManager.cloneManager;
            cm.cloneTypes.TryGetValue(TypeName, out cloneType);

            Color32 color = (cloneType != null) ? cloneType.cloneColour : new Color32(0, 0, 0, 1);
            GetComponent<MeshRenderer>().material.color = color;


            Debug.Log(cloneType.tagName);

        }


        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player") && cloneType != null)
            {

                PlayerStats.pStats.CStats.CurrentCloneType = cloneType;

            }
        }
    } 
}
